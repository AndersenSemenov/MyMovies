using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    class TagCodes: DataParser<int, Tag> // key --- tagID, value --- tag
    {
        public TagCodes(): base(',', @"D:\data\ml-latest (1)\ml-latest\TagCodes_MovieLens.csv") { }

        protected override void ParseData()
        {
            foreach (var line in inputFileStrings.GetConsumingEnumerable()) ///// 
            {
                string[] words = line.Split(spliter);
                if (words[0] != "tagId")
                {
                    dict.AddOrUpdate(Convert.ToInt32(words[0]), new Tag(words[1]), ((x, y) => y));
                }
            }
        }
    }
}
