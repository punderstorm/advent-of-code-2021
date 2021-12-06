using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawDirections = System.IO.File.ReadAllLines(@$"input.txt")
                .ToList();

            Part1(rawDirections);

            Part2(rawDirections);
        }

        static CourseDirection ParseDirection(string direction)
        {
            var match = Regex.Match(direction, $@"(forward|up|down) (\d*)");
            return new CourseDirection {
                    Direction = (Direction)Enum.Parse(typeof(Direction), match.Groups[1].Value),
                    Distance = int.Parse(match.Groups[2].Value)
                };
        }

        static void Part1(List<string> rawDirections)
        {
            var horizontal = 0;
            var depth = 0;

            rawDirections
                .Select(d => ParseDirection(d))
                .ToList()
                .ForEach(d => {
                    switch(d.Direction)
                    {
                        case Direction.forward:
                            horizontal += d.Distance;
                            break;
                        case Direction.down:
                            depth += d.Distance;
                            break;
                        case Direction.up:
                            depth -= d.Distance;
                            break;
                    }
                });

            Console.WriteLine($@"Part 1 Horizontal position X Depth = {horizontal * depth}");
        }

        static void Part2(List<string> rawDirections)
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            rawDirections
                .Select(d => ParseDirection(d))
                .ToList()
                .ForEach(d => {
                    switch(d.Direction)
                    {
                        case Direction.forward:
                            horizontal += d.Distance;
                            depth += aim * d.Distance;
                            break;
                        case Direction.down:
                            aim += d.Distance;
                            break;
                        case Direction.up:
                            aim -= d.Distance;
                            break;
                    }
                });

            Console.WriteLine($@"Part 2 Horizontal position X Depth = {horizontal * depth}");
        }
    }

    public enum Direction
    {
        forward = 1,
        down = 2,
        up = 3
    }

    public class CourseDirection
    {
        public Direction Direction { get; set; }
        public int Distance { get; set; }
    }
}
