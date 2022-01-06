﻿// <auto-generated />
using System;
using EntityFrameworkCoreContextRepository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkCoreContextRepository.Migrations
{
    [DbContext(typeof(WindServiceMainDbContext))]
    [Migration("20220104133306_Update-wind-generator-device")]
    partial class Updatewindgeneratordevice
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.Role.RepoRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AdditionalJsonData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoftDeleteReasonInt")
                        .HasColumnType("int");

                    b.Property<string>("SoftDeleteReasonJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("SystemString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.User.RepoUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalJsonData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AppFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("AssignRoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ExpireTokenDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FailedAttempt")
                        .HasColumnType("int");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RssId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoftDeleteReasonInt")
                        .HasColumnType("int");

                    b.Property<string>("SoftDeleteReasonJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartTrackingInterval")
                        .HasColumnType("datetime2");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Susspend")
                        .HasColumnType("bit");

                    b.Property<string>("SystemString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Workplace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.WindGeneratorDevice.RepoWindGeneratorDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalJsonData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GeographicalLatitude")
                        .HasColumnType("float");

                    b.Property<string>("GeographicalLatitudeStr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GeographicalLongitude")
                        .HasColumnType("float");

                    b.Property<string>("GeographicalLongitudeStr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoftDeleteReasonInt")
                        .HasColumnType("int");

                    b.Property<string>("SoftDeleteReasonJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("SystemString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ValueDec")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ValueStr")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WindGeneratorDevices");
                });

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.WindGeneratorDevice_History.RepoWindGeneratorDevice_History", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalJsonData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GeographicalLatitude")
                        .HasColumnType("float");

                    b.Property<string>("GeographicalLatitudeStr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GeographicalLongitude")
                        .HasColumnType("float");

                    b.Property<string>("GeographicalLongitudeStr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ParentWindGeneratorDeviceId")
                        .HasColumnType("bigint");

                    b.Property<int>("SoftDeleteReasonInt")
                        .HasColumnType("int");

                    b.Property<string>("SoftDeleteReasonJson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("SystemString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ValueDec")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ValueStr")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentWindGeneratorDeviceId");

                    b.ToTable("WindGeneratorDevice_Histories");
                });

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.User.RepoUser", b =>
                {
                    b.HasOne("RepositoryModel.RepoModels.Implementations.Role.RepoRole", "AssignRole")
                        .WithMany("ListOfUsers")
                        .HasForeignKey("AssignRoleId");

                    b.Navigation("AssignRole");
                });

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.WindGeneratorDevice_History.RepoWindGeneratorDevice_History", b =>
                {
                    b.HasOne("RepositoryModel.RepoModels.Implementations.WindGeneratorDevice.RepoWindGeneratorDevice", "ParentWindGeneratorDevice")
                        .WithMany("ListOfWindGeneratorDevice_History")
                        .HasForeignKey("ParentWindGeneratorDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentWindGeneratorDevice");
                });

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.Role.RepoRole", b =>
                {
                    b.Navigation("ListOfUsers");
                });

            modelBuilder.Entity("RepositoryModel.RepoModels.Implementations.WindGeneratorDevice.RepoWindGeneratorDevice", b =>
                {
                    b.Navigation("ListOfWindGeneratorDevice_History");
                });
#pragma warning restore 612, 618
        }
    }
}
