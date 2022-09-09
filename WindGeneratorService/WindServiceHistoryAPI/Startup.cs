using DtoLocalServerDALImplementation;
using DtoLocalServerDALImplementation.DALImplementation.User;
using DtoServiceDAL.Interfaces.User;
using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RepoServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceHistoryAPI
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

           


            #region Dependency injection           

            services.AddScoped<IRepositoryDAL, EntityFrameworkCoreDAL>(isp =>
            {
                return new EntityFrameworkCoreDAL(conString);
            });
            services.AddScoped<DtoServiceDAL.Abstractions.ADtoDAL, DtoLocalServiceDAL>(isp =>
            {
                return new DtoLocalServiceDAL(conString);
            });
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
