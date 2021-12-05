using System;
using System.Collections.Generic;
using System.Linq;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var depths = System.IO.File.ReadAllLines(@$"input.txt")
                .Select(n => int.Parse(n))
                .ToList();
            
            FindIncreased(depths);

            FindIncreasedSlidingWindow(depths);
        }

        static void FindIncreased(List<int> depths)
        {
            var increased = depths
                .Where((depth, index) => { return index > 0 && depth > depths[index - 1]; })
                .Count();
                
            Console.WriteLine($@"Number of depths that increased from the one before: {increased}");
        }

        static void FindIncreasedSlidingWindow(List<int> depths)
        {
            var slidingWindows = depths
                .SkipLast(2)
                .Select((depth, index) => {
                    return depths[index] + depths[index + 1] + depths[index + 2];
                })
                .ToList();
            
            var increased = slidingWindows
                .Where((depth, index) => { return index > 0 && depth > slidingWindows[index - 1]; })
                .Count();
                
            Console.WriteLine($@"Number of sliding windows that increased from the one before: {increased}");
        }
    }
}
