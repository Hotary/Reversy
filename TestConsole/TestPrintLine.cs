using NUnit.Framework;
using System;
using ConsoleReversy;


namespace TestConsole
{

    public class TestPrintLine
    {
        readonly string line = "####################################################################################################\r\n";

        [Test]
        public void Test()
        {
            Program.WindowWidth = 100;
            var buffer = new BufferedOutput();
            Console.SetOut(buffer);
            Program.PrintLine();
            Assert.AreEqual(buffer.Pop(), line);
        }
    }
}