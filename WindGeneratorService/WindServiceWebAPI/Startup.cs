using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoLocalServerDALImplementation;
using EntityFrameworkCoreContextRepository.Context;
using EntityFrameworkCoreContextRepository.DALImplementation;
using EntityFrameworkCoreContextRepository.DALImplementation.User;
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
using RepositoryModel.RepoModels.Implementations.Role;
using RepositoryModel.RepoModels.Implementations.User;

namespace WindServiceWebAPI
{
    public class Startup
    {
        readonly string CorsPolicy = "CorsPolicy";
        public static long userRoleId = 0;
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
            services.AddAuthorization(options =>
            {
            });

                services.AddScoped<IRepositoryDAL, EntityFrameworkCoreDAL>(isp =>
            {
                return new EntityFrameworkCoreDAL(conString);
            });
            services.AddScoped<DtoServiceDAL.Abstractions.ADtoDAL, DtoLocalServiceDAL>(isp =>
            {
                //  repoService = isp.GetService<IRepositoryDAL>();
                return new DtoLocalServiceDAL(conString);
            });
          //  services.AddTransient<TokenManagerMiddleware>();
       //     services.AddTransient<ITokenManager, TokenManager>();
         //   services.AddScoped<IDtoUserDAL, DtoUserDAL>();
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
            //app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

            //  app.UseHttpsRedirection();








            app.UseAuthentication();

        //    app.UseMiddleware<TokenManagerMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            try
            {
                DbContextOptions<WindServiceMainDbContext> options = new DbContextOptions<WindServiceMainDbContext>();
                var builder = new DbContextOptionsBuilder<WindServiceMainDbContext>(options);
                builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                var dbContext = new WindServiceMainDbContext(builder.Options);
                dbContext.Database.Migrate();
                SuperAdminInitialization(dbContext);
                //  MonitoringSuiteInitialization(dbContext);
                //     VirtualObjectSetInitialization(dbContext);
              //  GlobalSettingsInitialization(dbContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #region SuperAdminInitialization

        public void SuperAdminInitialization(WindServiceMainDbContext dbContext)
        {
            if (dbContext != null)
            {
                #region added super-admin-role in database

                var role = dbContext.Roles.FirstOrDefault(r => r.SystemString.Contains("{system-admin-role}"));
                if (role == null)
                {
                    var repoRole = new RepoRole
                    {
                        SystemString = "{system-admin-role}",
                        Name = "SuperAdmin",
                        Description = "Top level administrator. {read only}",
                        Active = true,                      
                    };

                    dbContext.Roles.Add(repoRole);
                    dbContext.SaveChanges();

                    var toRetRole = dbContext.Roles.FirstOrDefault(r => r.Name.Contains("SuperAdmin"));
                    #endregion
                
                #region added super-admin-user in database
                        byte[] passwordHash;
                        byte[] passwordSalt;

                    RepositoryUserDAL.CreatePasswordHashStatic("WindServiceAdmin!1", out passwordHash, out passwordSalt);

                    var repoUser = new RepoUser
                    {
                        UserName = "super-admin@windservice.com",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        AssignRoleId = toRetRole.Id,
                        EmailConfirmed = true,
                    };

                    dbContext.Users.Add(repoUser);
                    dbContext.SaveChanges();
                    #endregion

                #region added regular-user-role in database

                    var user_role = dbContext.Roles.FirstOrDefault(r => r.SystemString.Contains("{system-user-role}"));
                    if (user_role == null)
                    {
                        var repoUserRole = new RepoRole
                        {
                            SystemString = "{system-user-role}",
                            Name = "User",
                            Description = "Top level user. {read only}",
                            Active = true,
                        };

                        dbContext.Roles.Add(repoUserRole);
                        dbContext.SaveChanges();            
                       
                    }

                    var toRetUserRole = dbContext.Roles.FirstOrDefault(r => r.Name.Contains("User"));

                    userRoleId = toRetRole.Id;
                    #endregion
                }
            }
        }
        #endregion
    }
}
