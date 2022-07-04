using System;
using System.Collections.Generic;
using System.Text;
using ReversyEngine;
using NUnit.Framework;

namespace Tests
{
    class TestCellAction
    {
        [Test]
        public void Test()
        {
            bool testFlag = false;

            // CellHelper помогательный класс, чтобы бtз модификации основного кода обойти protected internal set для action;
            var cellHelper = new CellHelper(0, 0, (c) =>
            {
                testFlag = true;
            });

            // Производим downcast т.к. тест для Cell :)
            var cell = cellHelper as Cell;
            cell.Action();
            Assert.IsTrue(testFlag);
        }
    }
}
