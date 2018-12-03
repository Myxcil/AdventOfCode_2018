using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode.Day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("inputData.txt");

            // PART ONE 
            /*
            int frequency = 0;
            for (int i = 0; i < lines.Length; ++i)
            {
                if (int.TryParse(lines[i], out int iValue))
                {
                    frequency += iValue;
                }
            }
            Console.WriteLine("Frequency = " + frequency);
            */

            // part #2
            int frequency = 0;
            Dictionary<int, bool> freqTable = new Dictionary<int, bool>();

            int index = -1;
            for(; ;)
            {
                if (++index == lines.Length)
                    index = 0;

                if (int.TryParse(lines[index], out int iValue))
                {
                    frequency += iValue;
                    if (freqTable.ContainsKey(frequency))
                    {
                        break;
                    }
                    freqTable.Add(frequency, true);
                }
            }
            Console.WriteLine("Frequency = " + frequency);
            Console.ReadLine();
        }
    }
}
