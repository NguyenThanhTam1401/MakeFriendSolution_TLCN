using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeFriendSolution.Application;
using MakeFriendSolution.EF;
using MakeFriendSolution.HubConfig;
using MakeFriendSolution.Middlewares;
using MakeFriendSolution.Models;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MakeFriendSolution
{
    public class Startup
    {
        public static string DomainName;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Startup.DomainName = Configuration["DomainName"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins("https://localhost:4200", "http://localhost:4200", "https://192.168.0.200:4200", "http://192.168.0.200:4200", "http://192.168.0.150:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSignalR();
            
            services.AddDbContext<MakeFriendDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("MakeFriendConnection")));

            //Declare DI
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IMailService, MailService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionService, SessionService>();
            services.AddTransient<IUserApplication, UserApplication>();
            services.AddSingleton<IMailchimpService, MailchimpService>();
            services.AddTransient<IImageApplication, ImageApplication>();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            //add Session
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1800);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddTransient<AccessMiddleware>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseRouting();

            //Adding
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();
            app.UseMiddleware<AccessMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChartHub>("/chatHub");
            });

        }
    }
}