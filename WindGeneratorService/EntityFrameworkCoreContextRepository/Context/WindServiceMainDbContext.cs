using Microsoft.EntityFrameworkCore;
using RepositoryModel.RepoModels.Implementations.Role;
using RepositoryModel.RepoModels.Implementations.User;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice;
using RepositoryModel.RepoModels.Implementations.WindGeneratorDevice_History;
using RepositoryModel.RepoModels.Implementations.WindGeneratorType;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreContextRepository.Context
{
    public class WindServiceMainDbContext : DbContext
    {
        public WindServiceMainDbContext()
        {

        }

        public WindServiceMainDbContext(DbContextOptions<WindServiceMainDbContext> options)
          : base(options)
        {

        }

        public DbSet<RepoUser> Users { get; set; }
        public DbSet<RepoRole> Roles { get; set; }
        public DbSet<RepoWindGeneratorDevice> WindGeneratorDevices { get; set; }
        public DbSet<RepoWindGeneratorDevice_History> WindGeneratorDevice_Histories { get; set; }

        public DbSet<RepoWindGeneratorType> WindGeneratorTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region RepoUser
                modelBuilder.Entity<RepoUser>()
                      .HasMany(c => c.ListOfWindGeneratorDevice)
                      .WithOne(e => e.ParentUser)
                      .HasForeignKey(e => e.ParentUserId);
            #endregion

            #region RepoRole
            modelBuilder.Entity<RepoRole>()
               .HasMany(c => c.ListOfUsers)
               .WithOne(e => e.AssignRole)
               .HasForeignKey(e => e.AssignRoleId);
            #endregion

            #region RepoWindGeneratorDevice
            modelBuilder.Entity<RepoWindGeneratorDevice>()
               .HasMany(c => c.ListOfWindGeneratorDevice_History)
               .WithOne(e => e.ParentWindGeneratorDevice)
               .HasForeignKey(e => e.ParentWindGeneratorDeviceId);

          
            #endregion

            #region RepoWindGeneratorTypes
            modelBuilder.Entity<RepoWindGeneratorType>()
              .HasMany(c => c.ListOfGenerators)
              .WithOne(e => e.ParentWindGeneratorType)
              .HasForeignKey(e => e.ParentWindGeneratorTypeId);
            #endregion

        }

    }
}
