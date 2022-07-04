using ConsoleReversy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsole
{
    public class TestPrintField
    {
        readonly string line = "      A  B  C  D  E  F  G  H     \r\n" +
                               "  00  #  #  #  #  #  #  #  #     \r\n" +
                               "                                 \r\n" +
                               "  01  #  #  #  #  #  #  #  #     \r\n" +
                               "                                 \r\n" +
                               "  02  #  #  #  #  #  #  #  #     \r\n" +
                               "                                 \r\n" +
                               "  03  #  #  #  o  o  #  #  #     \r\n" +
                               "                                 \r\n" +
                               "  04  #  #  #  o  o  #  #  #     \r\n" +
                               "                                 \r\n" +
                               "  05  #  #  #  #  #  #  #  #     \r\n" +
                               "                                 \r\n" +
                               "  06  #  #  #  #  #  #  #  #     \r\n" +
                               "                                 \r\n" +
                               "  07  #  #  #  #  #  #  #  #     \r\n" +
                               "                                 \r\n";

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
            Program.WindowWidth = 34;
            var buffer = new BufferedOutput();
            Console.SetOut(buffer);
            Program._core = new ReversyEngine.Core(ColorPlayer1, ColorPlayer2, Size, StartPattern);
            Program.PrintField();
            Assert.AreEqual(buffer.Pop(), line);
        }
    }
}
