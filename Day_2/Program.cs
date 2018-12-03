using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunPartOne();
            RunPartTwo();
        }

        private static void RunPartOne()
        {
            string[] lines = File.ReadAllLines("inputData.txt");

            int numOfTwos = 0, numOfThrees = 0;
            for (int i = 0; i < lines.Length; ++i)
            {
                bool twoCount, threeCount;
                CountMultiChars(lines[i], out twoCount, out threeCount);
                if (twoCount)
                {
                    ++numOfTwos;
                }
                if (threeCount)
                {
                    ++numOfThrees;
                }
            }

            int checkSum = numOfThrees * numOfTwos;
            Console.WriteLine("Checksum=" + checkSum);
            Console.ReadLine();
        }

        private static void CountMultiChars(string line, out bool twoCount, out bool threeCount)
        {
            Dictionary<char, int> charTable = new Dictionary<char, int>();
            for (int i = 0; i < line.Length; ++i)
            {
                int count;
                if (!charTable.TryGetValue(line[i], out count))
                {
                    charTable.Add(line[i], 1);
                }
                else
                {
                    charTable[line[i]] = count + 1;
                }
            }

            twoCount = false;
            threeCount = false;

            foreach(var kv in charTable)
            {
                if (kv.Value == 2)
                {
                    twoCount = true;
                }
                else if (kv.Value == 3)
                {
                    threeCount = true;
                }
            }
        }

        private static void RunPartTwo()
        {
            string[] lines = File.ReadAllLines("inputData.txt");

            Dictionary<int, List<int>> dupTable = new Dictionary<int, List<int>>();
            for (int i = 0; i < lines.Length - 1; ++i)
            {
                for (int j = i + 1; j < lines.Length; ++j)
                {
                    if (OffByOne(lines[i], lines[j]))
                    {
                        if (!dupTable.TryGetValue(i, out List<int> list))
                        {
                            list = new List<int>();
                            dupTable.Add(i, list);
                        }
                        list.Add(j);
                    }
                }
            }
            foreach(var kv in dupTable)
            {
                if (kv.Value.Count == 1)
                {
                    string lineA = lines[kv.Key];
                    string lineB = lines[kv.Value[0]];
                    StringBuilder sb = new StringBuilder();
                    for(int i=0; i < lineA.Length; ++i)
                    {
                        if (lineA[i] == lineB[i])
                        {
                            sb.Append(lineA[i]);
                        }
                    }
                    Console.WriteLine("Result=" + sb.ToString());
                }
            }
            Console.ReadLine();
        }

        private static bool OffByOne(string lineA, string lineB)
        {
            bool mismatchingChar = false;
            for(int i=0; i < lineA.Length; ++i)
            {
                if (lineA[i] != lineB[i])
                {
                    if (mismatchingChar)
                        return false;

                    mismatchingChar = true;
                }
            }
            return mismatchingChar;
        }
    }
}
