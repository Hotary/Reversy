using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ReversyEngine;

namespace Tests
{
    class TestCoreConstructor
    {

        [TestCaseSource(nameof(Cases))]
        public void Test(string colorPlayer1, string colorPlayer2, int x, int y, string pattern)
        {
            var core = MakeCore(colorPlayer1, colorPlayer2, x, y, pattern);
            Assert.IsNotNull(core);
            Assert.AreEqual(core.Chips[Player.Player1].Color, colorPlayer1);
            Assert.AreEqual(core.Chips[Player.Player2].Color, colorPlayer2);
            Assert.AreEqual(core.Field.Size.X, x);
            Assert.AreEqual(core.Field.Size.Y, y);

            // Test field :)
            int[,] p = new int[x, y];  // First dimension - X; second dimension - Y;

            string[] splitted = pattern.Split('|');
            for (int _y = 0; _y < y; _y++)
            {
                var trimmed = splitted[_y].Trim();
                for (int _x = 0; _x < x; _x++)
                    p[_x, _y] = trimmed[_x] - '0';
            }

            for (int _x = 0; _x < x; _x++)
                for (int _y = 0; _y < y; _y++)
                {
                    var player = (Player)p[_x, _y];
                    if (player == Player.Player1 || player == Player.Player2)
                    {
                        Assert.AreEqual(core.Field[_x, _y].Chip.Player, player);
                    }
                    else 
                    {
                        Assert.IsNull(core.Field[_x, _y].Chip);
                    }
                }
        }

        static object[] Cases => PrepareTestCase();

        public static object[] PrepareTestCase()
        {
            var f = DataSource.Fields;
            var data = new object[]
            {
                new object[]{"#000000", "#FFFFFF", 8, 8, f[0] },
                new object[]{"#000000", "#FFFFFF", 8, 8, f[1] },
                new object[]{"#000000", "#FFFFFF", 8, 8, f[2] }
            };
            return data;
        }

        public class Params : ReversyEngine.CoreInit
        {
            public string ColorPlayer1 { get; set; }

            public string ColorPlayer2 { get; set; }

            public ReversyEngine.SizeField Size { get; set; }

            public string StartPattern { get; set; }
        }

        static private Core MakeCore(string colorPlayer1, string colorPlayer2, int x, int y, string pattern)
        {
            var _params = new Params()
            {
                ColorPlayer1 = colorPlayer1,
                ColorPlayer2 = colorPlayer2,
                Size = new SizeField() { X = x, Y = y },
                StartPattern = pattern
            };

            return new Core(_params)
            {
                Finder = new ReversyEngine.LineFinder()
            };
        }
    }
}
