﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Vacancy.Data.DbContexts;

#nullable disable

namespace Vacancy.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240708163445_UserRole")]
    partial class UserRole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Vacancy.Domain.Entities.Applicants.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<string>("PassportPath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("passport_path");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("applicants");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Applications.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicantId")
                        .HasColumnType("integer")
                        .HasColumnName("applicant_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("VakancyId")
                        .HasColumnType("integer")
                        .HasColumnName("vakancy_id");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("VakancyId");

                    b.ToTable("applications");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Assetss.Assets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Extention")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("extention");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("assets", "assets");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .HasColumnType("integer")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("permission_name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("permissions", "auth");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<bool>("isDefault")
                        .HasColumnType("boolean")
                        .HasColumnName("is_default");

                    b.HasKey("Id");

                    b.ToTable("roles", "auth");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int?>("GivenById")
                        .HasColumnType("integer")
                        .HasColumnName("givenby_id");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer")
                        .HasColumnName("permission_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("GivenById");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.TokenModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("access_token");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiredDateOfRefreshToken")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expired_date_of_refresh_token");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("boolean")
                        .HasColumnName("remember_me");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("token_model");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .HasColumnType("text")
                        .HasColumnName("company_name");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("avatar_image");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("boolean")
                        .HasColumnName("is_approved");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Region")
                        .HasColumnType("text")
                        .HasColumnName("region");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users", "auth");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Vacancies.Vakancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("company");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<DateTime>("JobStartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("job_start_date");

                    b.Property<string>("QualificationRequirements")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("qualification_requirements");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("region");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric")
                        .HasColumnName("salary");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("WorkingDays")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("working_days");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("vacancy");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Applicants.Applicant", b =>
                {
                    b.HasOne("Vacancy.Domain.Entities.Authentications.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Applications.Application", b =>
                {
                    b.HasOne("Vacancy.Domain.Entities.Applicants.Applicant", "Applicant")
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vacancy.Domain.Entities.Vacancies.Vakancy", "Vakancy")
                        .WithMany("Applications")
                        .HasForeignKey("VakancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Vakancy");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.RolePermission", b =>
                {
                    b.HasOne("Vacancy.Domain.Entities.Authentications.User", "GivenBy")
                        .WithMany()
                        .HasForeignKey("GivenById");

                    b.HasOne("Vacancy.Domain.Entities.Authentications.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vacancy.Domain.Entities.Authentications.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GivenBy");

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.User", b =>
                {
                    b.HasOne("Vacancy.Domain.Entities.Authentications.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Vacancies.Vakancy", b =>
                {
                    b.HasOne("Vacancy.Domain.Entities.Authentications.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Applicants.Applicant", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Authentications.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Vacancy.Domain.Entities.Vacancies.Vakancy", b =>
                {
                    b.Navigation("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}
