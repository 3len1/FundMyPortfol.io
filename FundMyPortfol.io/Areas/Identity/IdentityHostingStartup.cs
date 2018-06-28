﻿using System;
using FundMyPortfol.io.Areas.Identity.Data;
using FundMyPortfol.io.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FundMyPortfol.io.Areas.Identity.IdentityHostingStartup))]
namespace FundMyPortfol.io.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FundMyPortfolioContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("FundMyPortfolioContextConnection")));

                services.AddDefaultIdentity<FundMyPortfolioUser>()
                    .AddEntityFrameworkStores<FundMyPortfolioContext>();
            });
        }
    }
}