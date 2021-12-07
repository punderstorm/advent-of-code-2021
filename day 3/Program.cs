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
                var bitUsage = rawBinary
                    .Select(b => b & bit)
                    .GroupBy(x => x)
                    .ToDictionary(g => g.Key, g => g.Count());
                
                // most used is gamma, least used is epsilon
                gamma += bitUsage
                    .Aggregate((x, y) => x.Value > y.Value ? x : y)
                    .Key;
                epsilon += bitUsage
                    .Aggregate((x, y) => x.Value < y.Value ? x : y)
                    .Key;
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
                        .Select(b => b & bit)
                        .GroupBy(x => x)
                        .ToDictionary(g => g.Key, g => g.Count())
                        .Aggregate((x, y) => {
                            if (x.Value == y.Value)
                            {
                                if (x.Key == Math.Max(x.Key, y.Key))
                                {
                                    return x;
                                }
                                else
                                {
                                    return y;
                                }
                            }
                            else if (x.Value > y.Value)
                            {
                                return x;
                            }
                            else
                            {
                                return y;
                            }
                        })
                        .Key > 0 ? "1" : "0");
                    o2 += currentO2Bit;
                    //Console.WriteLine($@"Current O2 bit = {currentO2Bit}");
                    //Console.WriteLine($@"O2 rating = {o2}");
                    o2Numbers = o2Numbers.Where(x => Convert.ToString(x, 2).PadLeft(numBits, '0').StartsWith(o2)).ToList();
                }
                
                if (co2Numbers.Count() > 1)
                {
                    var currentCO2Bit = (co2Numbers
                        .Select(b => b & bit)
                        .GroupBy(x => x)
                        .ToDictionary(g => g.Key, g => g.Count())
                        .Aggregate((x, y) => {
                            if (x.Value == y.Value)
                            {
                                if (x.Key == Math.Min(x.Key, y.Key))
                                {
                                    return x;
                                }
                                else
                                {
                                    return y;
                                }
                            }
                            else if (x.Value < y.Value)
                            {
                                return x;
                            }
                            else
                            {
                                return y;
                            }
                        })
                        .Key > 0 ? "1" : "0");
                    co2 += currentCO2Bit;
                    //Console.WriteLine($@"Current CO2 bit = {currentCO2Bit}");
                    //Console.WriteLine($@"CO2 rating = {co2}");
                    co2Numbers = co2Numbers.Where(x => Convert.ToString(x, 2).PadLeft(numBits, '0').StartsWith(co2)).ToList();
                }

                //Console.WriteLine($@"");
            }

            //Console.WriteLine($@"Last O2 rating = {o2Numbers[0]} ({Convert.ToString(o2Numbers[0], 2)})");
            //Console.WriteLine($@"Last CO2 rating = {co2Numbers[0]} ({Convert.ToString(co2Numbers[0], 2)})");
            Console.WriteLine($@"O2 rating * CO2 rating = {o2Numbers[0] * co2Numbers[0]}");
        }
    }
}
