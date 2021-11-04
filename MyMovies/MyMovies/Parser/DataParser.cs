﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    abstract class DataParser<T1, T2>
    {
        protected BlockingCollection<string> inputFileStrings;
        protected ConcurrentDictionary<T1, T2> output;
        protected char[] spliters;
        protected string pathToTheData;

        public abstract void ReadandGetData();

        public abstract Task ParseData();

        public DataParser(char[] split, string path)
        {
            inputFileStrings = new BlockingCollection<string>();
            output = new ConcurrentDictionary<T1, T2>();
            spliters = split; 
            pathToTheData = path;
        }
    }
}