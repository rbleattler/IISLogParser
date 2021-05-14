using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IISLogParser
{
    public static class Utils
    {
        public static List<string> ReadAllLines(string file)
        {
            List<string> lines = new List<string>();
            using (FileStream fileStream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    while (streamReader.Peek() > -1)
                    {
                        lines.Add(streamReader.ReadLine());
                    }
                }
            }
            return lines;
        }
    }
}
