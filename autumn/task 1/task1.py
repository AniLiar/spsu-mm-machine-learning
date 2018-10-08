# -*- coding: utf-8 -*-
"""
Created on Wed Sep 26 16:36:20 2018

@author: Anna Leonova
"""
import pandas as pd
import numpy as np
from datetime import datetime as dt

def R_2(y_pred, y_true):
    loss = y_true - y_pred
    return 1 - np.sum(loss**2) / np.sum((y_true - np.mean(y_true))**2) 

def RMSE(y_pred, y_true):
    n = len(y_true)
    loss = y_true - y_pred
    return np.sqrt(np.sum(loss**2) * 1. / n)
    

def createEmptyReport(featuresNames):
    metricsNames = ['R2-train','R2-test', 'RMSE-train', 'RMSE-test']
    return pd.DataFrame([], index = metricsNames + featuresNames)
    
def saveReport(df):
    fileName = getReportName()
    df.to_csv(fileName, sep=';', encoding='utf-8')

def getReportName():
    return 'Report ' + dt.now().strftime("%Y-%m-%d %H-%M-%S") + '.csv'

def getFeaturesNames(df):
    return list(df.columns.values[:-1])
    
###############################
# m denotes the number of examples here, not the number of features
# w denotes features' weight
def gradientDescent(x, y, w, coeff_step, n, numIterations, eps, printLog=False):
    step = coeff_step    
    xTrans = x.transpose()
    for i in range(1, numIterations):
        step = coeff_step * 1.0 / np.sqrt(i)
        hypothesis = np.dot(x, w)
        loss = hypothesis - y
        cost = np.sum(loss ** 2) * 1. /  n
        if i % 100 == 0 and printLog:        
            print "Iteration %d | Cost: %f" % (i, cost) 
        gradient = 2. * np.dot(xTrans, loss) / n
        # update
        prevW = w
        w = w - step * gradient
        if np.linalg.norm(prevW - w) <= eps:
            if printLog:
                print 'exit, convergence condition fulfilled'            
            return w
    return w
    
def getParamGradientDescent(param):
    numIter = param[0]
    stepCoeff  = param[1]
    eps = param[2]
    return numIter, stepCoeff, eps    
    
def normalizeFeatures(df):
    #normalized_df=(df-df.mean())/df.std()
    df.iloc[:,:-1] = df.iloc[:,:-1] / df.iloc[:,:-1].std()
    return df
    
def shuffle(df):
    return df.sample(frac=1).reset_index(drop=True)

def crossValidation(df, n_chunks, paramGradientDescent):
    featuresNames = getFeaturesNames(df)
    report = createEmptyReport(featuresNames)  
    
    n_rows = len(df.index)
    size_chunk = n_rows / n_chunks
    numIterations, stepCoeff, eps = getParamGradientDescent(paramGradientDescent)

    for i in range(0, (n_chunks)):
        start = i * size_chunk
        end = n_rows if (i + 1 == n_chunks) else ((i + 1) * size_chunk)
        
        x_train = (df.drop(df.index[start:end])).iloc[:,:-1]
        y_train = (df.drop(df.index[start:end])).iloc[:,-1]
    
        x_test  = df.iloc[start:end,:-1]
        y_test  = df.iloc[start:end,-1]    

        n, m = np.shape(x_train) 
        w = np.zeros(m)
        w = gradientDescent(x_train, y_train, w, stepCoeff, n, numIterations, eps)    

        y_pred_tr = np.dot(x_train, w)
        R_2_train = R_2(y_pred_tr, y_train)
        RMSE_train = RMSE(y_pred_tr, y_train)
        
        y_pred_ts = np.dot(x_test, w)
        R_2_test = R_2(y_pred_ts, y_test)
        RMSE_test = RMSE(y_pred_ts, y_test)

        colName = 'T%d'% (i + 1) 
        report[colName] = pd.Series([R_2_train, R_2_test, RMSE_test, RMSE_train] + list(w), index = report.index) 
        
    E = pd.Series(report.mean(axis=1), index = report.index)
    STD = pd.Series(report.std(axis=1), index = report.index)
    report['E'] = E
    report['STD'] = STD
    return report   

def main():
    df = pd.read_csv('dataset-facebook-comment.csv', index_col='Index')
    
    normalized_df = normalizeFeatures(df)
    normalized_df = normalized_df.dropna(axis='columns')
    shuffled_df = shuffle(normalized_df)

    #CONST#
    n_chunks = 5
    numIterations = 1000
    stepCoeff = 0.036
    eps = 0.002
    #######
    
    paramGradientDescent = [numIterations, stepCoeff, eps]
    report = crossValidation(shuffled_df, n_chunks, paramGradientDescent)
    print report
    saveReport(report)
    
if __name__ == '__main__':
    main()       