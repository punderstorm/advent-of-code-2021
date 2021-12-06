﻿using System;
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

            //Part2(rawBinary);
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

        static void Part2(List<string> rawBinary)
        {
        }
    }
}