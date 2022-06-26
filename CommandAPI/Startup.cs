

namespace CommandAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandAPI.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Serialization;

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
            services.AddDbContext<CommandRepoContext>(options => options.UseNpgsql(Configuration.GetConnectionString("CommandDbConnection")));

            services.AddControllers().AddNewtonsoftJson(s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddScoped<TestServ>(x=>new TestServ(x.GetRequiredService<A>(),Configuration["develop"]));

            services.AddScoped<ICommandRepo,PostgreCommandRepo>();

            services.AddTransient<A>();

            services.AddMvc();

            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<CommandRepoContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 4;
            } );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            logger.LogInformation("It has started");
            app.UseExceptionHandler("/error.html");

            //if(env.IsDevelopment())
            //{
                //app.UseDeveloperExceptionPage();
            //}
            //else
            //{
                app.UseStatusCodePagesWithReExecute("/error.html");
            //}

            

            app.Use(async (c,n) => {
                if(c.Request.Path.Value.Contains("invalid"))
                {
                    throw new Exception();
                }
                await n();
            });

            //app.Run(async c=> await c.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName));

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            
            app.UseFileServer();

            
        }
    }
}
