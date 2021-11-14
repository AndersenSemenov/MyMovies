using System;
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
        public ConcurrentDictionary<T1, T2> dict;
        protected char spliter;
        protected string pathToTheData;

        public virtual void ReadandGetData()
        {
            var task = Downloader.LoadContentAsync(pathToTheData, inputFileStrings);
            ParseData();
        }

        protected abstract void ParseData();

        public DataParser(char split, string path)
        {
            inputFileStrings = new BlockingCollection<string>();
            dict = new ConcurrentDictionary<T1, T2>();
            spliter = split; 
            pathToTheData = path;
        }
    }
}
