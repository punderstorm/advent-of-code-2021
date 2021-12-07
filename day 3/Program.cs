using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawData = System.IO.File.ReadAllLines(@$"input.txt")
                .ToList();
            var rawBinary = rawData
                .Select(b => Convert.ToInt64(b, 2))
                .ToList();

            var numBits = rawData.First().Length;

            Part1(numBits, rawBinary);

            Part2(numBits, rawBinary);
        }

        static void Part1(int numBits, List<long> rawBinary)
        {
            long gamma = 0;
            long epsilon = 0;

            foreach(var i in Enumerable.Range(0, numBits))
            {
                // bit shift to the appropriate bit
                var bit = (long)1 << i;
                
                // find usage of 0 and 1 by using bitwise AND against the bitshift, and get counts of each
                var onIsMost = (rawBinary
                    .Where(x => (x & bit) > 0)
                    .Count() >= rawBinary.Count() / 2m);
                
                // most used is gamma, least used is epsilon
                gamma += (onIsMost ? bit : 0);
                epsilon += (!onIsMost ? bit : 0);
            }

            Console.WriteLine($@"Gamma rate * Epsilon rate = {gamma * epsilon}");
        }

        static void Part2(int numBits, List<long> rawBinary)
        {
            string o2 = "";
            string co2 = "";
            List<long> o2Numbers = rawBinary.ToList();
            List<long> co2Numbers = rawBinary.ToList();

            foreach(var i in Enumerable.Range(0, numBits).Reverse())
            {
                // bit shift to the appropriate bit
                var bit = (long)1 << i;
                
                if (o2Numbers.Count() > 1)
                {
                    var currentO2Bit = (o2Numbers
                        .Where(x => (x & bit) > 0)
                        .Count() >= o2Numbers.Count() / 2m ? "1" : "0");
                    o2 += currentO2Bit;
                    o2Numbers = o2Numbers.Where(x => Convert.ToString(x, 2).PadLeft(numBits, '0').StartsWith(o2)).ToList();
                }
                
                if (co2Numbers.Count() > 1)
                {
                    var currentCO2Bit = (co2Numbers
                        .Where(x => (x & bit) > 0)
                        .Count() < co2Numbers.Count() / 2m ? "1" : "0");
                    co2 += currentCO2Bit;
                    co2Numbers = co2Numbers.Where(x => Convert.ToString(x, 2).PadLeft(numBits, '0').StartsWith(co2)).ToList();
                }
            }

            Console.WriteLine($@"O2 rating * CO2 rating = {o2Numbers[0] * co2Numbers[0]}");
        }
    }
}
