using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using urfurniture.ActionFilters;
using urfurniture.DAL.Data;
using urfurniture.DAL.Implementation;
using urfurniture.DAL.Repository;
using urfurniture.Extentions;
using urfurniture.Models;
using urfurniture.Utility;

namespace urfurniture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
            //StripeConfiguration.ApiKey = "sk_test_51IBdfxEJrX3unl1DTNRIZlUXeJLULKDMgR3LKcitvO38akKTd82olTW4ide45vo9bHMqeoaVjg2xMGssG6roVIBx00TNmMwM0k";
        }
         
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtModel>(Configuration.GetSection("JWT"));
            services.Configure<EmailSetting>(Configuration.GetSection("MailSettings"));
            services.Configure<Models.Path>(Configuration.GetSection("Path"));
            services.AddDbContext<urfurnitureContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("urfurnitureDbConnection")));

            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IManageAccount, ManageAccountService>();
            services.AddScoped<IManageProduct, ManageProductService>();
            services.AddScoped<IMail, MailService>();
            services.AddScoped<IManageCart, ManageCartService>();
            services.AddScoped<IManageOrder, ManageOrderService>();
            services.AddScoped<IManageUser, ManageUser>();
            services.AddScoped<ITokenService,urfurniture.DAL.Implementation.TokenService>();
            services.AddCors(o => o.AddPolicy("urfurniture", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

            //services.AddControllersWithViews();


            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = Configuration["JWT:Issuer"],
            ValidAudience = Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
        };
    });
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            // Register the Swagger Generator service. This service is responsible for genrating Swagger Documents.

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "urfurniture API",
                    Version = "v1",
                    Description = "Description for the API goes here.",
                    Contact = new OpenApiContact
                    {
                        Name = "urfurniture",
                        Email = string.Empty,
                        Url = new Uri("http://urfurniture.co.uk/"),
                    },
                });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            app.UseCors("urfurniture");
            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<urfurnitureContext>();
            //    context.Database.Migrate();
            //}
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WoodenStore API V1");
                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Upload")),
                RequestPath = "/Upload"
            });
            app.UseRouting();
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Upload")),
                RequestPath = new PathString("/Upload")
            });


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
