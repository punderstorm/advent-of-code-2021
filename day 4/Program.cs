using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawData = System.IO.File.ReadAllLines(@$"input.txt")
                .Select(x => Regex.Replace(x.Trim(), "\\s+", " "))
                .ToList();

            var numbersToDraw = rawData[0].Split(',').Select(n => int.Parse(n)).ToList();

            var allBoards = CreateBoards(rawData.GetRange(2, rawData.Count() - 2));

            // part 1
            FindFirstToWin(numbersToDraw, allBoards);

            // part 2
            FindLastToWin(numbersToDraw, allBoards);
        }

        static void FindFirstToWin(List<int> numbersToDraw, List<BingoBoard> allBoards)
        {
            BingoBoard hasBingo;
            for (var i = 0; i < numbersToDraw.Count(); i++)
            {
                allBoards.ForEach(b => b.UpdateBoard(numbersToDraw[i]));

                if (i >= 5)
                {
                    hasBingo = allBoards.Where(b => b.HasBingo()).FirstOrDefault();
                    if (hasBingo != null)
                    {
                        Console.WriteLine($@"Board number {hasBingo.Index} has bingo.  Sum of unmatched numbers * number found ({numbersToDraw[i]}) = {hasBingo.GetUnmatchedSum() * numbersToDraw[i]}");
                        break;
                    }
                }
            }
        }

        static void FindLastToWin(List<int> numbersToDraw, List<BingoBoard> allBoards)
        {
            for (var i = 0; i < numbersToDraw.Count(); i++)
            {
                allBoards.ForEach(b => b.UpdateBoard(numbersToDraw[i]));

                if (i >= 5)
                {
                    if (allBoards.Count() == 1 && allBoards.Where(b => b.HasBingo()).Count() == 1)
                    {
                        Console.WriteLine($@"Board number {allBoards[0].Index} has bingo.  Sum of unmatched numbers * number found ({numbersToDraw[i]}) = {allBoards[0].GetUnmatchedSum() * numbersToDraw[i]}");
                        break;
                    }
                    else
                    {
                        allBoards = allBoards.Where(b => !b.HasBingo()).ToList();
                    }
                }
            }
        }

        static List<BingoBoard> CreateBoards(List<string> rawData)
        {
            var allBoards = new List<BingoBoard>();
            BingoBoard board = null;
            for (var i = 0; i < rawData.Count(); i++)
            {
                if (rawData[i] == "")
                {
                    allBoards.Add(board);
                    board = null;
                    continue;
                }

                if (board == null)
                {
                    board = new BingoBoard(allBoards.Count() + 1);
                }

                board.Numbers.AddRange(rawData[i].Split(' ').Select(n => new Cell(int.Parse(n))).ToList());
            }

            return allBoards;
        }

        class Cell
        {
            public int Number {get;set;}

            public bool Matched {get;set;}

            public Cell (int number)
            {
                Number = number;
            }
        }

        class BingoBoard
        {
            public List<Cell> Numbers {get;set;}
            public int Index {get;set;}

            public BingoBoard(int index)
            {
                Numbers = new List<Cell>();
                Index = index;
            }

            public int GetUnmatchedSum()
            {
                return Numbers.Where(c => !c.Matched).Sum(c => c.Number);
            }

            public void UpdateBoard(int number)
            {
                var matchedCell = Numbers.Where(c => c.Number == number).FirstOrDefault();
                if (matchedCell != null) { matchedCell.Matched = true; }
            }

            public bool HasBingo()
            {
                foreach (var index in Enumerable.Range(0,5))
                {
                    if
                    (
                        (Numbers[5*index].Matched && Numbers[5*index+1].Matched && Numbers[5*index+2].Matched && Numbers[5*index+3].Matched && Numbers[5*index+4].Matched) ||
                        (Numbers[index].Matched && Numbers[index+5].Matched && Numbers[index+10].Matched && Numbers[index+15].Matched && Numbers[index+20].Matched)
                    )
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
