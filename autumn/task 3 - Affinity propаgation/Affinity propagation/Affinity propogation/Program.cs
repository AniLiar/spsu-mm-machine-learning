using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Affinity_propagation {
    class Program {
        private const double preference = -1;
        private const int nodesN = 196591;

        private static int epochN = 0;

        private static Edge[][] edgesIn = new Edge[nodesN][];
        private static Edge[][] edgesOut = new Edge[nodesN][];

        private static int[] exemplars = new int[nodesN];
        private static Dictionary<int, int> clusterWidth = new Dictionary<int, int>();

        private static Random rand = new Random();

        private const String dataSetName = "Gowalla_edges.txt";
        private const String dataDimName = "Gowalla_edges_dims.txt";

        private static double getGaussian(double mean, double stdDev) {
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = mean + stdDev * randStdNormal;
            return randNormal;
        }

        private static void initGraph(Edge[][] graphOut, Edge[][] graphIn, double preference, String FileName) {
            try {
                using (StreamReader sr = new StreamReader(FileName)) {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null) {
                        int dim = int.Parse(line);

                        graphOut[i] = new Edge[dim + 1];
                        graphOut[i][0] = new Edge(i, preference, 0, 0, null);

                        graphIn[i] = new Edge[dim + 1];
                        graphIn[i][0] = new Edge(i, preference, 0, 0, graphOut[i][0]);

                        graphOut[i][0].invEdge = graphOut[i][0];
                        i++;
                    }
                }
                Console.WriteLine("Dims loaded");
            } catch (Exception e) {
                Console.WriteLine("The file {0} could not be read:", FileName);
                Console.WriteLine(e.Message);
                Console.WriteLine("Or another problem :)");
            }
        }

        private static void getEpochN() {
            do {
                Console.WriteLine("Enter the number of iterations:");
            } while (!Int32.TryParse(Console.ReadLine(), out epochN));
        }

        private static void loadDataset(String FileName, Edge[][] edgesOut, Edge[][] edgesIn) {
            try {
                using (StreamReader sr = new StreamReader(FileName)) {
                    int[] lastIndIn = new int[nodesN];
                    int[] lastIndOut = new int[nodesN];

                    for (int i = 0; i < lastIndIn.Length; i++)
                        lastIndIn[i] = 0;

                    for (int i = 0; i < lastIndOut.Length; i++)
                        lastIndOut[i] = 0;
                    
                    string line;
                    while ((line = sr.ReadLine()) != null) { 

                        string[] nodes = line.Split('\t');
                        int from = int.Parse(nodes[0]);
                        int to = int.Parse(nodes[1]);                       

                        lastIndOut[from]++;
                        lastIndIn[to]++;

                        double s = 1 + getGaussian(0, 0.005);
                        edgesOut[from][lastIndOut[from]] = 
                            new Edge(to, s, 0, 0, null);

                        edgesIn[to][lastIndIn[to]] = 
                            new Edge(from, s, 0, 0, edgesOut[from][lastIndOut[from]]);

                        edgesOut[from][lastIndOut[from]].invEdge = edgesIn[to][lastIndIn[to]];
                    }
                }
                Console.WriteLine("Dataset loaded");
            } catch (Exception e) {
                Console.WriteLine("The file {0} could not be read:", FileName);
                Console.WriteLine(e.Message);
                Console.WriteLine("Or another problem :)");
            }
        }

        private static void updateR(double damping) {
            for (int i = 0; i < nodesN; i++) {
                double max = double.MinValue;
                double max2 = double.MinValue;
                int maxInd = -1;

                for (int j = 0; j < edgesOut[i].Length; j++) {
                    double sumAS = edgesOut[i][j].A + edgesOut[i][j].S;
                    if (sumAS > max) {
                        max2 = max;
                        max = sumAS;
                        maxInd = edgesOut[i][j].id;
                    } else if (sumAS > max2)
                        max2 = sumAS;
                }

                for (int j = 0; j < edgesOut[i].Length; j++) {
                    double Rij = maxInd == edgesOut[i][j].id ? max2 : max;
                    edgesOut[i][j].R *= (1 - damping);
                    edgesOut[i][j].R += (edgesOut[i][j].S - Rij) * damping;
                    edgesOut[i][j].invEdge.R = edgesOut[i][j].R;
                }
            }
        }

        private static double calcAij(int i, int k) {
            double sumMax = 0;
            for (int j = 0; j < edgesIn[k].Length; j++) {
                if (edgesIn[k][j].id != k && edgesIn[k][j].id != i || edgesIn[k][j].id == k) {
                    sumMax += Math.Max(0, edgesIn[k][j].R);
                }
            }
            return 0 > sumMax ? 0 : sumMax;
        }

        private static double calcAii(int i) {
            double sumMax = 0;
            for (int j = 1; j < edgesIn[i].Length; j++) {
                sumMax += Math.Max(0, edgesIn[i][j].R);
            }
            return sumMax;
        }

        private static void updateA(double damping) {
            for (int i = 0; i < nodesN; i++) {                
                for (int j = 1; j < edgesOut[i].Length; j++) {
                    edgesOut[i][j].A *= (1 - damping);
                    edgesOut[i][j].A += calcAij(i, edgesOut[i][j].id) * damping;
                    edgesOut[i][j].invEdge.A = edgesOut[i][j].A;
                }
                edgesOut[i][0].A *= (1 - damping);
                edgesOut[i][0].A += calcAii(i) * damping;
                edgesOut[i][0].invEdge.A = edgesOut[i][0].A;
            }
        }

        private static void appointExemplar() {
            for (int i = 0; i < edgesOut.Length; i++) {
                double max = double.MinValue;
                int maxInd = -1;
                for (int j = 0; j < edgesOut[i].Length; j++) {
                    double sumAR = edgesOut[i][j].A + edgesOut[i][j].R;
                    if (max < sumAR) {
                        max = sumAR;
                        maxInd = edgesOut[i][j].id;
                    }
                }
                exemplars[i] = maxInd;

                if (clusterWidth.ContainsKey(exemplars[i]))
                    clusterWidth[exemplars[i]]++;
                else
                    clusterWidth.Add(exemplars[i], 1);
            }
        } 

        private static void getClusterWidth() {
            for (int i = 0; i < exemplars.Length; i++) {
                if (clusterWidth.ContainsKey(exemplars[i]))
                    clusterWidth[exemplars[i]]++;
                else
                    clusterWidth.Add(exemplars[i], 1);
            }
        }

        private static int getClustersN() {
            return clusterWidth.Count;
        }

        private static void APFit(double damping) {
            Console.WriteLine("Affinity propagation runs");
            for (int epoch = 0; epoch < epochN; epoch++) {
                DateTime timeStart = DateTime.Now;
                updateR(damping);
                updateA(damping);
                appointExemplar();
                Console.WriteLine("Epoch {0} is completed in {1} sec.", epoch, DateTime.Now - timeStart);
            }
        }

        private static void saveExemplars() {
            string FileName = "Gowalla_Exemplars_" + DateTime.Now.ToString("yyyy MM dd HH mm ss") + ".txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    for (int i = 0; i < exemplars.Length; i++)
                        sw.WriteLine(exemplars[i]);
                }
                Console.WriteLine("Examplars data saved");
            }
            catch (Exception e)
            {
                Console.WriteLine("The file {0} could not be write:", FileName);
                Console.WriteLine(e.Message);
                Console.WriteLine("Or another problem :)");
            }
        }

        static void Main(string[] args) {
            double damping = 0.5;
            initGraph(edgesOut, edgesIn, preference, dataDimName);
            getEpochN();
            loadDataset(dataSetName, edgesOut, edgesIn);
            APFit(damping);
            Console.WriteLine("Count of clusters {0}", getClustersN());
            saveExemplars();
            Console.ReadLine();
        }
    }
}