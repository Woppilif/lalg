using NUnit.Framework;
using System.IO;
using System;

namespace lalgTest
{
    public class ControllersTest
    {
        private const string Expected = "Hello World!";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Assert.Pass();
            using(var sw = new StringWriter())
            {
                Console.SetOut(sw);
                //LAlg.Startup.Configuration;
                //LAlg.Models.ErrorViewModel();

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
}