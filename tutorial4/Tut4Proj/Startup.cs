using System;
using Tut4Proj.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Tut4Proj
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
            services.AddScoped<IStudentsDb, SqlServerDb>(); // HAS TO BE SPECIFIED!
            //        ^ sth between Transient and Singleton
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            // all methods below are MiddleWare - order is important!
            app.UseHttpsRedirection();  // short-circuit and immediatelly return response
            // ^ redirects our request to https request if asked in http
            app.UseRouting(); // Route["api.students"] 

            // // our custom middleWare (adds Secret to every request or sth like that)
            // app.Run(async con =>
            // {
            //     con.Response.Headers.Add("Secret", "12345aa");
            // });

            app.UseAuthorization(); // immidiatelly return 401 if not authorised
            // checking if someone has access to sth

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
