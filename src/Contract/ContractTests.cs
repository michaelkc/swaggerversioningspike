using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Seges.CvrServices.Hosting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Seges.CvrServices.Contract.Infrastructure;

namespace Seges.CvrServices.Contract
{
    //[TestFixtureSource(typeof(VersionsTestFixtureSource))]
    internal class ContractTests : IVersionedTestFixture
    {
        public string CurrentVersion { get; }

        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        // When specifying TestFixtureSource
        //public ContractTests(string currentVersion)
        //{
        //    CurrentVersion = currentVersion;
        //}

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new NunitWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [TestCaseSource(typeof(VersionsTestCaseSource), "TestCases", new object[] { new[] { "V1"} })]
        //[VersionsSupportedByTest("V1","V2")]
        public async Task RegressionTestForV1NotV2(string version)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, $"/weatherforecast/{version}/");

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response:");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(responseContent);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            StringAssert.Contains("temperatureF", responseContent);
        }

        [TestCaseSource(typeof(VersionsTestCaseSource), "TestCases", new object[] { new[] { "V2" } })]
        public async Task RegressionTestForV2NotV1(string version)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, $"/weatherforecast/{version}/");

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response:");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(responseContent);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            StringAssert.Contains("temperatureF", responseContent);
        }

        [TestCaseSource(typeof(VersionsTestCaseSource),"TestCases",new object[]{new[]{"V1","V2"}})]
        public async Task RegressionTestForAllVersions(string version)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, $"/weatherforecast/{version}/");

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response:");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(responseContent);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            StringAssert.Contains("temperatureF", responseContent);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
        
        public class NunitWebApplicationFactory : WebApplicationFactory<Startup> { }

    }
}