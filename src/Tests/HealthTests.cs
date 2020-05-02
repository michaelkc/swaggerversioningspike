using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Seges.CvrServices.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Seges.CvrServices.Tests
{
    internal class HealthTests
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new NunitWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task HealthEndpointReturnsHealthy()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "/health");

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response:");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(responseContent);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
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