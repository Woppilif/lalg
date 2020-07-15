using NUnit.Framework;
using LAlg;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LAlg.Tests
{
    [TestFixture()]
    public class StartupTests
    {
        private TestServer _server;
        private HttpClient _client;
        [SetUp]
        public void Init()
        {
            _server = new TestServer(new WebHostBuilder()
                       .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test()]
        public void StartupTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ConfigureServicesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ConfigureTest()
        {
            Assert.Fail();
        }
    }
}