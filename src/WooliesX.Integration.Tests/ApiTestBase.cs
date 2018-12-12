using System;
using Microsoft.AspNetCore.TestHost;

namespace WooliesX.Integration.Tests
{
    public class ApiTestBase
    {
        protected TestServer Api => TestHost.Api;
        protected IServiceProvider Services => TestHost.Services;
    }
}