namespace Affinity_propagation
{
    public class Edge
    {
        public int id { get; set; }
        public double S { get; set; }
        public double R { get; set; }
        public double A { get; set; }
        public Edge invEdge { get; set; } //inverse edge

        public Edge(int id, double similarity, double responsibility, double availability,                           
                            Edge edge)
        {
            this.id = id;
            this.S = similarity;
            this.R = responsibility;
            this.A = availability;
            this.invEdge = edge;            
        }
    }
}