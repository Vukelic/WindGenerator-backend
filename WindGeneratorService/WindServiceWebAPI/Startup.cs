using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLocalServerDALImplementation;
using DtoLocalServerDALImplementation.DALImplementation.User;
using DtoServiceDAL.Interfaces.User;
using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation;
using EntityFrameworkCoreContextRepository.DALImplementation.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RepoServiceDAL.Interfaces;
using RepositoryModel.RepoModels.Implementations.Role;
using RepositoryModel.RepoModels.Implementations.User;
using WindServiceWebAPI.Interfaces;
using WindServiceWebAPI.Models.AppTokenSetting;
using WindServiceWebAPI.Models.Common;

namespace WindServiceWebAPI
{
    public class Startup
    {
        readonly string CorsPolicy = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DB_connection

            var conString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<WindServiceMainDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region Cors Policy
            //   services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder => builder
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(origin => true) // allow any origin
                 .AllowCredentials() // allow credentials                 
                    );
            });

            #endregion

            #region Controllers

            services.AddControllers()
                   .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            #endregion

            #region JWT Token Authentication 

            var authSettings = Configuration.GetSection(nameof(AppTokenSettings));
            services.Configure<AppTokenSettings>(authSettings);

            services.Configure<AppTokenSettings>(Configuration.GetSection("AppTokenSettings"));
            var token = Configuration.GetSection("AppTokenSettings").Get<AppTokenSettings>();
            var key = Encoding.ASCII.GetBytes(token.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion


            #region Dependency injection           

            services.AddScoped<IRepositoryDAL, EntityFrameworkCoreDAL>(isp =>
              {
            return new EntityFrameworkCoreDAL(conString);
             });
            services.AddScoped<DtoServiceDAL.Abstractions.ADtoDAL, DtoLocalServiceDAL>(isp =>
            {
                return new DtoLocalServiceDAL(conString);
            });
            services.AddTransient<TokenManagerMiddleware>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddScoped<IDtoUserDAL, DtoUserDAL>();
            services.AddHttpContextAccessor();

            #endregion
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors(CorsPolicy);

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMiddleware<TokenManagerMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            try
            {
                DbContextOptions<WindServiceMainDbContext> options = new DbContextOptions<WindServiceMainDbContext>();
                var builder = new DbContextOptionsBuilder<WindServiceMainDbContext>(options);
                builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                var dbContext = new WindServiceMainDbContext(builder.Options);
                dbContext.Database.Migrate();
             

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

       
    }
}
