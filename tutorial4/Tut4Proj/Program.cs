using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Tut4Proj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1. create Local IIS/Kestrel Server on random port

            //2. Our app will be deployed and served through localhost:port

            CreateHostBuilder(args).Build().Run(); // obejct configured below
        }

        //running our app:
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) // builder configuration 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
