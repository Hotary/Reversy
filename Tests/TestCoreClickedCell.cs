using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ReversyEngine;

namespace Tests
{
    class TestCoreClickedCell
    {
        [TestCaseSource(nameof(Cases))]
        public void Test(Core core, int xClick, int yClick, string patternAfterClick)
        {
            Assert.IsNotNull(core);
            core.ClickedCell(core.Field[xClick, yClick]);

            int[,] pattern = new int[core.CurrentSize.X, core.CurrentSize.Y];  // First dimension - X; second dimension - Y;

            string[] splitted = patternAfterClick.Split('|');
            for (int y = 0; y < core.CurrentSize.Y; y++)
            {
                var trimmed = splitted[y].Trim();
                for (int x = 0; x < core.CurrentSize.X; x++)
                    pattern[x, y] = trimmed[x] - '0';
            }

            for (int x = 0; x < core.CurrentSize.X; x++)
                for (int y = 0; y < core.CurrentSize.Y; y++)
                {
                    var player = (Player)pattern[x, y];
                    if (player == Player.Player1 || player == Player.Player2)
                    {
                        if (core.Field[x, y].Chip is null)
                            Assert.Fail("Fail on cell [{0}, {1}]. Was empty, waited {2}", x, y, player);
                        if (core.Field[x, y].Chip.Player != player)
                            Assert.Fail("Fail on cell [{0}, {1}]. Was {2}, waited {3}", x, y, core.Field[x, y].Chip.Player, player);
                    }
                    else
                    {
                        Assert.IsNull(core.Field[x, y].Chip);
                    }
                }



            for (int x = 0; x < core.CurrentSize.X; x++)
                for (int y = 0; y < core.CurrentSize.Y; y++)
                {
                    var player = (Player)pattern[x, y];
                    if (player == Player.Player1 || player == Player.Player2)
                    {
                        if (core.Field[x, y].Chip is null)
                            Assert.Fail("Fail on cell [{0}, {1}]. Was empty, waited {2}", x, y, player);
                        if (core.Field[x, y].Chip.Player != player)
                            Assert.Fail("Fail on cell [{0}, {1}]. Was {2}, waited {3}", x, y, core.Field[x, y].Chip.Player, player);
                    }
                    else
                    {
                        Assert.IsNull(core.Field[x, y].Chip);
                    }
                }
        }

        static object[] Cases => PrepareTestCase();

        public static object[] PrepareTestCase()
        {
            var f = DataSource.Fields;
            var data = new object[]
            {
                new object[]{MakeCore(8, 8, f[0]), 5, 3, clickedFields[0]},
                new object[]{MakeCore(8, 8, f[0]), 4, 2, clickedFields[1]},
                new object[]{MakeCore(8, 8, f[0]), 3, 5, clickedFields[2]},
                new object[]{MakeCore(8, 8, f[0]), 2, 4, clickedFields[3]},
                new object[]{MakeCore(8, 8, f[0]), 0, 0, f[0] },
                new object[]{MakeCore(8, 8, f[0]), 2, 3, f[0] },
                new object[]{MakeCore(8, 8, f[0]), 3, 2, f[0] },
                new object[]{MakeCore(8, 8, f[1]), 0, 0, f[1] },
                new object[]{MakeCore(8, 8, f[2]), 0, 0, f[2] },
                new object[]{MakeCore(8, 8, f[3]), 0, 0, f[3] },
                new object[]{MakeCore(8, 8, f[3]), 6, 5, f[3] },
                new object[]{MakeCore(8, 8, f[3]), 7, 5, clickedFields[4] },
                new object[]{MakeCore(8, 8, f[4]), 0, 0, f[4] },
                new object[]{MakeCore(8, 8, f[4]), 1, 4, f[4] },
                new object[]{MakeCore(8, 8, f[4]), 7, 4, clickedFields[5] }
            };
            return data;
        }


        public class Params : ReversyEngine.CoreInit
        {
            public string ColorPlayer1 => null;

            public string ColorPlayer2 => null;

            public ReversyEngine.SizeField Size { get; set; }

            public string StartPattern { get; set; }
        }

        static private Core MakeCore(int x, int y, string pattern)
        {
            var _params = new Params()
            {
                Size = new SizeField() { X = x, Y = y },
                StartPattern = pattern
            };

            return new Core(_params)
            {
                Finder = new ReversyEngine.LineFinder()
            };
        }

        static private List<string> clickedFields = new List<string>
        {
            @"00000000|
              00000000|
              00000000|
              00011100|
              00021000|
              00000000|
              00000000|
              00000000",

            @"00000000|
              00000000|
              00001000|
              00011000|
              00021000|
              00000000|
              00000000|
              00000000",

            @"00000000|
              00000000|
              00000000|
              00012000|
              00011000|
              00010000|
              00000000|
              00000000",

            @"00000000|
              00000000|
              00000000|
              00012000|
              00111000|
              00000000|
              00000000|
              00000000",

            @"00000000|
              00000100|
              00000100|
              00012120|
              00011210|
              00010001|
              00000000|
              00000000",

            @"00000000|
              00000100|
              00200100|
              00111110|
              00111111|
              01010000|
              00000000|
              00000000"
        };
    }
}
