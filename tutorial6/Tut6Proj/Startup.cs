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
            services.AddTransient<IDbService, SqlServerDbService>(); // remember to invoke
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService dbService) // our custom db service inteface injected
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<LoggingMiddleware>();

            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("The index was not porvided in teh header"); // 401 + message
                    return;
                }
                else
                {
                    // check if student is in db
                    string index = context.Request.Headers["Index"].ToString();

                    //...
                    // new SqlConnection()
                    // dbService.CheckIdex - to make in Services
                    // select * from students where IndexNumber = @Index;
                    //401 context.Response.WriteAssync("Invalid student index")..
                    //return;
                }
                await next(); // if everyting is ok (student is in Db) -> call next middleware (works like linked-list)
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
