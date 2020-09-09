using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using webapi.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using webapi.Services.EmailService;
using Microsoft.AspNetCore.Http.Features;
using webapi.Helpers;
using AutoMapper;
using webapi.Services;
using webapi.Middleware;

namespace webapi
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

            services.AddDbContext<DataContext>(opts =>
            {
                opts.UseNpgsql(
                Configuration["ConnectionStrings:DefaultConnection"]);
            });

            //identity
            services.AddDbContext<IdentityDataContext>(opts =>
            {
                opts.UseNpgsql(
                Configuration["ConnectionStrings:IdentityConnection"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDataContext>()
            .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(24));

            //swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo { Title = "coupman API", Version = "v1" });
            });



            //smtp email
            services.AddSingleton(Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>());
            services.AddScoped<IEmailSender, SendGridSender>();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //app setting
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // services.AddSingleton(Configuration
            //      .GetSection("AppSettings")
            //      .Get<AppSettings>());

            //automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAccountService, AccountService>();

            services.AddCors();
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder => builder.SetIsOriginAllowed(isOriginAllowed => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "coupman API");
            });

            //demo only
            IdentitySeedData.SeedDatabase(app).Wait();
            SeedData.EnsurePopulated(app).Wait();
            //end demo only
        }
    }
}
