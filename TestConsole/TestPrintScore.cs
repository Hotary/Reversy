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

        [Test]
        public void Test()
        {
            Program.WindowWidth = 100;
            var buffer = new BufferedOutput();
            Console.SetOut(buffer);
            Program._core = new ReversyEngine.Core(new Params());
            Program._core.CountChips[ReversyEngine.Player.Player1] = 9;
            Program._core.CountChips[ReversyEngine.Player.Player2] = 16;
            Program.PrintScore();
            Assert.AreEqual(buffer.Pop(), line);
        }
    }
}
