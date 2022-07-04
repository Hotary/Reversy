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

        [Test]
        public void Test()
        {
            Program.WindowWidth = 34;
            var buffer = new BufferedOutput();
            Console.SetOut(buffer);
            Program._core = new ReversyEngine.Core(new Params());
            Program.PrintField();
            Assert.AreEqual(buffer.Pop(), line);
        }
    }
}
