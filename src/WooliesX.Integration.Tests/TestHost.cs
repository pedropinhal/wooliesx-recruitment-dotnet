using System;
using WooliesX.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace WooliesX.Integration.Tests
{
    public static class TestHost
    {
        private static readonly TestServer _api;

        static TestHost()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
                
            _api = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
        }

        public static TestServer Api => _api;

        public static IServiceProvider Services => Api.Host.Services;
    }
}