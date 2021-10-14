using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiIdentityJwt.Services;

namespace WebApiIdentityJwt
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
            // RateLimit -- needed to store rate limit counters and ip rules
            // RateLimit -- Configure the Rate Limit in the api
            services.AddMemoryCache();
            services.ConfigureRateLimiting();

            // Implement Cache -- Package:- Marvin.Cache.Headers
            services.ConfigureHttpCacheHeaders();

            // DataBase Connection
            services.AddDbContext<Data.AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConn")));

            // Dependency Injection
            services.AddTransient<AuthManagerService>();

            // Identity & Jwt
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            // Enable Cors in api
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api Identity Jwt", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Api Identity Jwt v1"));

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            // Global Error Handling Must below development Exception Page
            app.ConfigureExceptionHandler();

            //RateLimit -- Use the Middleware
            app.UseIpRateLimiting();

            app.UseRouting();

            // Authentication & Authorization Jwt Token
            app.UseAuthentication();
            app.UseAuthorization();

            // Cache
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
