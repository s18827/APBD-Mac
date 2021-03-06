using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tut6Proj.Services;
using Microsoft.AspNetCore.Http;
using Tut6Proj.MiddleWare;

namespace Tut6Proj
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
            services.AddScoped<IDbService, SqlServerDbService>(); // remember to invoke
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService dbService) // our custom db service inteface injected
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<LoggingMiddleware>(); // Task 2

            app.Use(async (context, next) => // Task 1
            {
                 // check if request contains index number
                if (!context.Request.Headers.ContainsKey("IndexNumber"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("The index was not provided in the header");
                    return; // 401 + message
                }
                // check if student exists
                string index = context.Request.Headers["IndexNumber"].ToString();
                var st = dbService.GetStudentByIndex(index);
                if (st == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Student not found in the database - Incorrect index number");
                    return;
                }
                await next(); // if everyting is ok -> call next middleware (works like linked-list)
            });

            // // Task Additional

            // // conditional middleware (authorisation activtes when some condition is met like here: "secret" in path)
            // app.UseWhen(context  => context.Request.Path.ToString().Contains("secret"), app => app.Use (async (context, next) =>
            // {
            //     if (!context.Request.Headers.ContainsKey("IndexNumber"))
            //     {
            //         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //         await context.Response.WriteAsync("The index was not provided in the header");
            //         return; // 401 + message
            //     }

            //     string index = context.Request.Headers["IndexNumber"].ToString();
            //     var st = dbService.GetStudentByIndex(index);
            //     if (st == null)
            //     {
            //         context.Response.StatusCode = StatusCodes.Status404NotFound;
            //         await context.Response.WriteAsync("Student not found in the database - Incorrect index number");
            //         return;
            //     }
            //     await next();
            //     // in this case as "secret" appears in the path we should handle it somehow or else we cannot properly use our requests
            // }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
