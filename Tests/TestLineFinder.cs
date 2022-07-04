using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ReversyEngine;
using System.Linq;

namespace Tests
{
    class TestLineFinder
    {
        [TestCaseSource(nameof(Cases))]
        public void Test(Player testPlayer, int xPoint, int yPoint, string strPattern, int xLength, int yLength, List<Line?> testLines = null)
        {
            var finder = new LineFinder();

            var field = new Field(new SizeField() { X = xLength, Y = yLength }, null, null);
            var chips = new Dictionary<Player, Chip>()
                {
                    { Player.Player1, new Chip(Player.Player1, null) },
                    { Player.Player2, new Chip(Player.Player2, null) }
                };

            int[,] pattern = new int[xLength, yLength];  // First dimension - X; second dimension - Y;

            string[] splitted = strPattern.Split('|');
            for (int y = 0; y < yLength; y++)
            {
                var trimmed = splitted[y].Trim();
                for (int x = 0; x < xLength; x++)
                    pattern[x, y] = trimmed[x] - '0';
            }

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                {
                    var player = (Player)pattern[x, y];
                    if (player == Player.Player1 || player == Player.Player2)
                        field[x, y].Chip = chips[player];
                }

            var lines = finder.Search(field, testPlayer, new Position(xPoint, yPoint));

            if (testLines is null)
            {
                if (lines is null)
                    return;
                else
                    Assert.Fail("Failed, wait not founded lines, but lines was found :( !");
            }
            else
            {
                if (lines is null)
                    Assert.Fail("Failed, wait founded lines, but lines was not found :( !");

                foreach (var line in lines)
                {
                    if (line is null)
                        break;
                    var foundedLine = testLines.Where(l => l.Value.Start == line.Value.Start && l.Value.End == line.Value.End).FirstOrDefault();
                    if (foundedLine is null)
                        Assert.Fail("Failed, was line [{0}, {1}] => [{2}, {3}], but it not waited!", line.Value.Start.X, line.Value.Start.Y, line.Value.End.X, line.Value.End.Y);
                    testLines.Remove(foundedLine);
                }

                if (testLines.Count > 0) 
                {
                    var line = testLines.First().Value;
                    Assert.Fail("Failed, waited line [{0}, {1}] => [{2}, {3}]", line.Start.X, line.Start.Y, line.End.X, line.End.Y);
                }


            }
        }

        static object[] Cases => PrepareTestCase();

        public static object[] PrepareTestCase()
        {
            var f = DataSource.Fields;
            var data = new object[]
            {
                new object[]{ Player.Player1, 5, 3, f[0], 8, 8, new List<Line?>() 
                {
                    new Line(){Start = new Position(5,3), End = new Position(3,3)}
                }},
                new object[]{ Player.Player2, 5, 3, f[0], 8, 8, null },
                new object[]{ Player.Player2, 2, 3, f[0], 8, 8, new List<Line?>()
                {
                    new Line(){Start = new Position(2,3), End = new Position(4,3)}
                }},
                new object[]{ Player.Player1, 2, 3, f[0], 8, 8, null },
                new object[]{ Player.Player1, 0, 0, f[0], 8, 8, null},
                new object[]{ Player.Player1, 7, 0, f[0], 8, 8, null},
                new object[]{ Player.Player1, 0, 7, f[0], 8, 8, null},
                new object[]{ Player.Player1, 7, 7, f[0], 8, 8, null},
                new object[]{ Player.Player1, 7, 5, f[3], 8, 8, new List<Line?>()
                {
                    new Line(){Start = new Position(7,5), End = new Position(5,3)},
                }},
                new object[]{ Player.Player2, 7, 5, f[3], 8, 8, null},
                new object[]{ Player.Player1, 7, 4, f[4], 8, 8, new List<Line?>()
                {
                    new Line(){Start = new Position(7,4), End = new Position(5,2)},
                    new Line(){Start = new Position(7,4), End = new Position(2,4)},
                }},
                new object[]{ Player.Player2, 7, 4, f[4], 8, 8, null },
            };
            return data;
        }
    }
}
