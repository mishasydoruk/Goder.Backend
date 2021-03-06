using AutoMapper;
using Goder.BL.Hubs;
using Goder.BL.MappingProfiles;
using Goder.BL.Providers;
using Goder.BL.Services;
using Goder.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Wrapper.Models;
using RabbitMQ.Wrapper.Services;
using System.Linq;
using System.Reflection;

namespace Goder.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<CodeRunnerProvider>();
            services.AddScoped<SolutionService>();
            services.AddScoped<ProblemService>();

            services.AddSignalR();

            services.AddDbContext<GoderContext>(options => options.UseMySql(Configuration.GetConnectionString("GoderDBConnection")));

            services.AddControllers()
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var rabbitMqOptions = Configuration.GetSection("RabbitMq").Get<RabbitMQOptions>();
            services.AddScoped<MessageFactory>(_ => new MessageFactory(rabbitMqOptions));
            services.AddScoped<QueueService>();

            var corsOrigins = Configuration.GetSection("AllowedCorsOrigins").GetChildren().Select(c => c.Value).ToArray();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(corsOrigins);
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Authority = Configuration["FirebaseAuthentication:Issuer"];
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["FirebaseAuthentication:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["FirebaseAuthentication:Audience"],
                        ValidateLifetime = true
                    };
                });

            services.AddScoped<RegistrationService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ProblemProfile>();
                cfg.AddProfile<TestProfile>();
                cfg.AddProfile<SolutionProfile>();
            }, Assembly.GetExecutingAssembly());

            services.AddHealthChecks()
                .AddDbContextCheck<GoderContext>("DbContextHealthCheck", HealthStatus.Healthy, tags: new[] { "db_ok" });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<CodeRunnerHub>("/codeRunner");
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthAuth", new HealthCheckOptions
                {
                    Predicate = _ => _.Tags.Contains("db_ok")
                }).RequireAuthorization();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => _.Tags.Contains("db_ok")
                });
            });
        }
    }
}
