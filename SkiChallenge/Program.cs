using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiChallenge
{
    class Program
    {

        private static List<SkiControl> route;
        private static Stack<Tuple<int, int>> maximums;
        private static List<int> overall;

        static void Main(string[] args)
        {
            overall = new List<int>();
            
            //read file
            var map = ReadFile.Read("map.txt");
            var clonedMap = ReadFile.Read("map.txt");
            //find max and storage stack greater than long path
            var maxPosition = GetMaxPosition(ref clonedMap);
            //surf north east west south and return data
            while ((maxPosition != null && map[maxPosition.Item1][maxPosition.Item2] > overall.Count))
            {
                route = new List<SkiControl>();
                GetMapRoute(map, 0, maxPosition, route);
                maxPosition = GetMaxPosition(ref clonedMap);
                Console.WriteLine($"Path: {overall.Count} stepeer {overall.FirstOrDefault()-overall.LastOrDefault()} current {map[maxPosition.Item1][maxPosition.Item2]}");
            }
            overall.ForEach(x=>Console.WriteLine(x));
        }

        private static Tuple<int, int> GetMaxPosition(ref List<List<int>> clonedMap)
        {
            int max = 0;
            Tuple<int, int> maxPosition = null;
            for (int yCount = 0; yCount < clonedMap.Count; yCount++)
            {
                var yline = clonedMap.ElementAt(yCount);
                var ylineMax = yline.Max();
                if (ylineMax > max)
                {
                    max = yline.Max();
                    maxPosition = new Tuple<int, int>(yCount, yline.IndexOf(max));
                }

            }
            if (maxPosition != null)
            {
                clonedMap[maxPosition.Item1][maxPosition.Item2] = -1;
            }

            return maxPosition;
        }

        private static List<SkiControl> GetMapRoute(List<List<int>> map, int deep, Tuple<int, int> position, List<SkiControl> route)
        {

            var current = map[position.Item1][position.Item2];
            if (route.Count == 0)
            {
                route.Add(new SkiControl() { Cell = current, Deep = deep, Steeper = 0 });
            }
            else if (route.LastOrDefault().Cell > current)
            {
                route.Add(new SkiControl() { Cell = current, Deep = deep, Steeper = route.FirstOrDefault().Cell - current });
            }
            else
            {

               
                var temp = route.Select(x => x.Cell).ToList();
                if(temp.Count > overall.Count || (temp.Count == overall.Count && temp.FirstOrDefault() - temp.LastOrDefault() > overall.FirstOrDefault() - overall.LastOrDefault()))
                {
                    overall = temp;
                }
                route.RemoveRange(deep, route.Count - deep);
                if (route.Count > 0)
                {
                    route.Add(new SkiControl() { Cell = current, Deep = deep, Steeper = route.FirstOrDefault().Cell - current });
                }
                else
                {
                    return route;
                }

            }
            //north
            if (position.Item1 - 1 >= 0 && map[position.Item1 - 1][position.Item2] < current)
            {
                route = GetMapRoute(map, deep + 1, new Tuple<int, int>(position.Item1 - 1, position.Item2), route);
            }
            //west
            if (position.Item2 - 1 >= 0 && map[position.Item1][position.Item2 - 1] < current)
            {
                route = GetMapRoute(map, deep + 1, new Tuple<int, int>(position.Item1, position.Item2 - 1), route);
            }
            //south
            if (position.Item1 + 1 < map.Count && map[position.Item1 + 1][position.Item2] < current)
            {
                route = GetMapRoute(map, deep + 1, new Tuple<int, int>(position.Item1 + 1, position.Item2), route);
            }
            //east
            if (position.Item2 + 1 < map.Count && map[position.Item1][position.Item2 + 1] < current)
            {
                route = GetMapRoute(map, deep + 1, new Tuple<int, int>(position.Item1, position.Item2 + 1), route);
            }

            return route;

        }
    }
}
