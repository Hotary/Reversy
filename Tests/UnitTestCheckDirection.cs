using NUnit.Framework;
using ReversyEngine;
using System.Collections.Generic;
using System.Linq;

using System.Diagnostics;

namespace Tests
{
    public class UnitTestCheckDirection
    {
        [TestCaseSource(nameof(DirCases))]
        public void Test(Field field, Player player, int x, int y, (int, int) dir, bool exist)
        {
            var lineFinder = new LineFinder();
            var line = lineFinder.CheckDirection(field, player, new Position(x, y), dir.Item1, dir.Item2);
            if (exist)
            {
                Assert.IsNotNull(line);
            }
            else
            {
                Assert.IsNull(line);
            }
        }

        static object[] DirCases => PrepareTestCase(); 

        public static object[] PrepareTestCase()
        {
            var fields = new Field[DataSource.Fields.Count];
            var p1 = Player.Player1;
            var p2 = Player.Player2;
            var xLength = 8;
            var yLength = 8;

            var chips = new Dictionary<Player, Chip>()
                {
                    { p1, new Chip(p1, null) },
                    { p2, new Chip(p2, null) }
                };
            var i = 0;
            foreach (var f in DataSource.Fields)
            {
                var field = new Field(new SizeField() { X = xLength, Y = yLength }, null, null);

                int[,] pattern = new int[xLength, yLength];  // First dimension - X; second dimension - Y;

                string[] splitted = f.Split('|');
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
                        if (player == p1 || player == p2)
                            field[x, y].Chip = chips[player];
                    }
                fields[i++] = field;
            }

            var data = new object[]
            {
                new object[]{fields[0], p1, 5, 3, Get(Dir.L),  true },
                new object[]{fields[0], p1, 5, 3, Get(Dir.U),  false},
                new object[]{fields[0], p1, 5, 3, Get(Dir.D),  false},
                new object[]{fields[0], p1, 5, 3, Get(Dir.R),  false},
                new object[]{fields[0], p1, 5, 3, Get(Dir.UL), false},
                new object[]{fields[0], p1, 5, 3, Get(Dir.UR), false},
                new object[]{fields[0], p1, 5, 3, Get(Dir.DL), false},
                new object[]{fields[0], p1, 5, 3, Get(Dir.DR), false},

                new object[]{fields[0], p2, 2, 3, Get(Dir.L),  false},
                new object[]{fields[0], p2, 2, 3, Get(Dir.U),  false},
                new object[]{fields[0], p2, 2, 3, Get(Dir.D),  false},
                new object[]{fields[0], p2, 2, 3, Get(Dir.R),  true },
                new object[]{fields[0], p2, 2, 3, Get(Dir.UL), false},
                new object[]{fields[0], p2, 2, 3, Get(Dir.UR), false},
                new object[]{fields[0], p2, 2, 3, Get(Dir.DL), false},
                new object[]{fields[0], p2, 2, 3, Get(Dir.DR), false}
            };
            return data;
        }

        private static (int, int) Get(Dir dir) => dirs[dir];

        private static Dictionary<Dir, (int, int)> dirs = new Dictionary<Dir, (int, int)>
        {
            {Dir.U, (0, -1)},
            {Dir.D, (0, 1)},
            {Dir.L, (-1, 0) },
            {Dir.R, (1, 0) },
            {Dir.UL, (-1, -1)},
            {Dir.UR, (1, -1)},
            {Dir.DL, (-1, 1)},
            {Dir.DR, (1, 1)},
        };

        private enum Dir
        {
            U, D, L, R,
            UL, UR, DL, DR
        }
    }
}