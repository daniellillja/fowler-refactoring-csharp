using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtractMethod = FowlerRefactoring.Code.Ch6.After.ExtractMethod;

namespace FowlerRefactoring.Tests.Ch6
{
    public abstract class ExtractMethodTests
    {
        protected ExtractMethod _sut;

        public void GivenSut()
        {
            _sut = new ExtractMethod("Bob");
        }

        [TestClass]
        public class WhenPrintingAmount : ExtractMethodTests
        {
            private string _consoleOutput;

            [TestInitialize]
            public void Init()
            {
                GivenSut();
                WhenPrintAmountCalled();
            }

            private void WhenPrintAmountCalled()
            {
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    _sut.PrintOwing(100);
                    _consoleOutput = sw.ToString();
                }
            }

            [TestMethod]
            public void ShouldPrintNameAndAmount()
            {
                Assert.IsTrue(_consoleOutput.Contains("name:Bob" + Environment.NewLine));
                Assert.IsTrue(_consoleOutput.Contains("amount:100" + Environment.NewLine));
            }

        }

    }


}
