using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day_3
{
    class Program
    {
        class Claim
        {
            public readonly int id;
            public readonly int x;
            public readonly int y;
            public readonly int width;
            public readonly int height;
            
            public Claim(string line)
            {
                int at = line.IndexOf('@');
                id = int.Parse(line.Substring(1, at - 1));

                line = line.Substring(at + 1);
                int comma = line.IndexOf(',');
                int colon = line.IndexOf(':');
                x = int.Parse(line.Substring(0,comma));
                y = int.Parse(line.Substring(comma + 1, colon - comma - 1));

                line = line.Substring(colon + 1);
                int times = line.IndexOf('x');
                width = int.Parse(line.Substring(0, times));
                height = int.Parse(line.Substring(times + 1));
            }
        }

        static void Main(string[] args)
        {
            RunPartOne();
        }

        private static void RunPartOne()
        { 
            string[] lines = File.ReadAllLines("inputData.txt");

            List<Claim> claims = new List<Claim>(lines.Length);

            int width = 0, height = 0;
            for(int i=0; i < lines.Length; ++i)
            {
                Claim newClaim = new Claim(lines[i]);
                claims.Add(newClaim);

                width = Math.Max(width, newClaim.x + newClaim.width);
                height = Math.Max(height, newClaim.y + newClaim.height);
            }

            int overlap = 0;
            int[,] grid = new int[width, height];
            for(int i=0; i < claims.Count; ++i)
            {
                overlap += FillGrid(claims[i], grid);
            }
            Console.WriteLine("Overlaps=" + overlap);

            for(int i=0; i < claims.Count; ++i)
            {
                if (!HasOverlap(claims[i],grid))
                {
                    Console.WriteLine("Fitting Id=" + claims[i].id);
                    break;
                }
            }

            Console.ReadLine();
        }

        private static int FillGrid(Claim claim, int[,] grid)
        {
            int overlap = 0;
            for(int y=0; y < claim.height; ++y)
            {
                int coordY = claim.y + y;
                for(int x=0; x < claim.width; ++x)
                {
                    int coordX = claim.x + x;
                    if (grid[coordX,coordY] == 1)
                    {
                        ++overlap;
                    }
                    grid[coordX, coordY]++;
                }
            }
            return overlap;
        }

        private static bool HasOverlap(Claim claim, int[,] grid)
        {
            for (int y = 0; y < claim.height; ++y)
            {
                int coordY = claim.y + y;
                for (int x = 0; x < claim.width; ++x)
                {
                    int coordX = claim.x + x;
                    if (grid[coordX, coordY] != 1)
                        return true;
                }
            }
            return false;
        }
    }
}
