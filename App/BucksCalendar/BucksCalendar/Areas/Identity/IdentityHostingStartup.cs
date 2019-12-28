using System;
using BucksCalendar.Areas.Identity.Data;
using BucksCalendar.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BucksCalendar.Areas.Identity.IdentityHostingStartup))]
namespace BucksCalendar.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        private string _connectionString = null;
        
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                var connectionStringBuilder = new SqlConnectionStringBuilder(context.Configuration.GetConnectionString("BucksCalendarIdentityDbContextConnection"));
                connectionStringBuilder.Password = context.Configuration["CalendarDbPassword"];
                _connectionString = connectionStringBuilder.ConnectionString;
                
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(_connectionString));

                services.AddDefaultIdentity<CalendarUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<DatabaseContext>();
            });
        }
    }
}