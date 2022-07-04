using ConsoleReversy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsole
{
    class TestPrintScore
    {
        readonly string line = "### Player1: 009 ################################################################## Player2: 016 ###\r\n";

        public string ColorPlayer1 => "#FFFFFF";

        public string ColorPlayer2 => "#000000";

        public ReversyEngine.SizeField Size => new ReversyEngine.SizeField() { X = 8, Y = 8 };

        public string StartPattern => @"00000000|
                                        00000000|
                                        00000000|
                                        00012000|
                                        00021000|
                                        00000000|
                                        00000000|
                                        00000000";

        [Test]
        public void Test()
        {
            Program.WindowWidth = 100;
            var buffer = new BufferedOutput();
            Console.SetOut(buffer);
            Program._core = new ReversyEngine.Core(ColorPlayer1, ColorPlayer2, Size, StartPattern);
            Program._core.CountChips[ReversyEngine.Player.Player1] = 9;
            Program._core.CountChips[ReversyEngine.Player.Player2] = 16;
            Program.PrintScore();
            Assert.AreEqual(buffer.Pop(), line);
        }
    }
}
