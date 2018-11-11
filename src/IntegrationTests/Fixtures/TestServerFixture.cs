using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Host;
using Host.Infrastructure.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IntegrationTests.Fixtures
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _server;
        private bool _disposed;

        public TestServerFixture()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            _server = new TestServer(builder);
            Client = _server.CreateClient();
            Client.BaseAddress = new Uri(ConfigurationHelper.CreateConfiguration()["baseAddress"]);
        }

        public HttpClient Client { get; private set; }

        public async Task<HttpResponseMessage> Post(string path, object data)
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), path);
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Put(string path, object data)
        {
            var request = new HttpRequestMessage(new HttpMethod("PUT"), path);
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Get(string path)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), path);
            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Delete(string path)
        {
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), path);
            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<T> Deserialize<T>(HttpResponseMessage message)
        {
            var json = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public DomainDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(ConfigurationHelper.CreateConfiguration()["sql"]);
            return new DomainDbContext(builder.Options);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Client.Dispose();
                _server.Dispose();
            }

            _disposed = true;
        }
    }
}
