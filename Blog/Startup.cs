using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Blog.Data;
using Blog.Data.Security;
using Blog.Features.Blog;
using Blog.Features.Log;
using Blog.Features.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Blog
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                };
            });

            services.AddMvc();

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            // Dependency injection
            //   scoped:    created once per request.
            //   transient: created each time they are requested.
            //   singleton: created first time they are requested and stays the same for subsequence requests.
            services.AddSingleton<IJwt, Jwt>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IReferrerLogRepository, ReferrerLogRepository>();
            services.AddSingleton<IPasswordHasher<BlogUser>, PasswordHasher>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Entity Framework
            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IReferrerLogRepository referrerLogger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Message");
            }

            app.Use(async (context, next) =>
            {
                // log referrer if request does not come from current host
                string referrer = context.Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referrer) && !referrer.ToLowerInvariant().Contains(context.Request.Host.Value.ToLowerInvariant()))
                {
                    var referrerLogRepository = context.RequestServices.GetRequiredService<IReferrerLogRepository>();
                    referrerLogRepository.Log(referrer);
                }

                await next();

                // let angular routing handle "not found" requests except "/api/*"
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.ToString().StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
                {
                    context.Request.Path = "/index.html"; // Go to WebRoot ("Angular/dist") - see Webroot config in Program.cs
                    await next();
                }
            })
            .UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
