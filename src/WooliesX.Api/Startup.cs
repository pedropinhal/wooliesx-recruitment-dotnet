﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using MediatR;
using WooliesX.Domain.Models;
using WooliesX.Domain.Ports;
using WooliesX.Data;

namespace WooliesX.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opt => opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc);

            services.AddMediatR(typeof(Domain.DomainModule).Assembly);

            var config = new ConfigurationSettings();
            Configuration.GetSection("WooliesXUser").Bind(config);
            services.AddSingleton<ConfigurationSettings>(config);
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IShopperHistoryRepository, ShopperHistoryRepository>();
            services.AddTransient<IHttpClient, Data.HttpClient>();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
