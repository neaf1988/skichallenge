using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SkiChallenge
{
    public class ReadFile
    {
        /// <summary>
        /// Read the file map
        /// </summary>
        /// <param name="filePath">txt file of the map file</param>
        /// <returns></returns>
        public static List<List<int>> Read(string filePath)
        {
            var map = new List<List<int>>();
            if (File.Exists(filePath))
            {
                // Read a text file line by line.
                string[] lines = File.ReadAllLines(filePath);
                string[] linesMap = new string[lines.Length];
                Array.Copy(lines,1, linesMap, 0, lines.Length-1);
                foreach (string line in linesMap)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        map.Add(line.Split(' ').Select(x => Convert.ToInt32(x)).ToList());
                    }
                    
                }
                    
            }
            else
            {
                throw new Exception("The path file doesn't exists");
            }
            return map;
        }
    }
}
