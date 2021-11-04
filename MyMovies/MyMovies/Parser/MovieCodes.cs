using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class MovieCodes: DataParser<string, string> // key --- movieID, value --- movieName
    {
        public MovieCodes(): base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\MovieCodes_IMDB.tsv") { }

        public override void ReadandGetData()
        {
            var task1 = Downloader.LoadContentAsync(pathToTheData, inputFileStrings);
            var task2 = ParseData();
            task2.Wait();
        }

        public override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    if (words[4] == "en") //words[4] == "ru" 
                    {
                       output.AddOrUpdate(words[0], words[2], ((x, y) => y));
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
