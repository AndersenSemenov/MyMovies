using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    public static class Downloader
    {
        public static async Task LoadContentAsync(string path, BlockingCollection<string> output)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var reader = new StreamReader(stream);
                var firstLine = reader.ReadLine();
                string line = null;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    output.Add(line);
                }
            }
            output.CompleteAdding();
        }
    }
}
