using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// PACKAGE ADDED: Microsoft.EntityFrameworkCore 3.1.4
// PACKAGE ADDED: Microsoft.EntityFrameworkCore.SqlServer 3.1.4
// PACKAGE ADDED: Microsoft.EntityFrameworkCore.Tools 3.1.4
// PACKAGE ADDED: Microsoft.AspNetCore.Mvc.NewtonsoftJson 3.1.4
namespace ExTest2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
