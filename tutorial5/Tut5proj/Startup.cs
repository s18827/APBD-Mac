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
using Tut5proj.Services;

namespace Tut5proj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // public void ConfigureServices(IServiceCollection services)
        // {
        //     // AddTransient - always new instance returned (not best performance)
        //     // AddSingleton - always one instance returned
        //     services.AddScoped<IStudentsDb, SqlServerDb>(); // for StudentController
        //     //        ^ sth between Transient and Singleton
        //     services.AddControllers();
        // }
        public void ConfigureServices(IServiceCollection services)
        // very useful for managing working on different db services
        // just choose different Service which implements Interface (here: IStudentsServiceDb)
        {
            services.AddScoped<IStudentsServiceDb, SqlServerStudentsDbService>(); // for EnrollmentController
            services.AddControllers();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
