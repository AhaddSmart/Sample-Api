﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Common.DomainEvent", b =>
                {
                    b.Property<DateTimeOffset>("DateOccurred")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("tinyint(1)");

                    b.ToTable("DomainEvent");
                });

            modelBuilder.Entity("Domain.Entities.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("text")
                        .HasColumnType("longtext")
                        .HasColumnName("Text");

                    b.HasKey("Id");

                    b.ToTable("Abouts");
                });

            modelBuilder.Entity("Domain.Entities.Banner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("fileRepoId")
                        .HasColumnType("int")
                        .HasColumnName("FileRepoId");

                    b.Property<DateTime?>("from")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("From");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("to")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("To");

                    b.HasKey("Id");

                    b.HasIndex("fileRepoId");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("Domain.Entities.BusinessType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Code");

                    b.Property<bool>("isActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsActive");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Name");

                    b.Property<int?>("parentTypeId")
                        .HasColumnType("int")
                        .HasColumnName("ParentTypeId");

                    b.HasKey("Id");

                    b.HasIndex("parentTypeId");

                    b.ToTable("BusinessTypes");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Code");

                    b.Property<bool>("isActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsActive");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Name");

                    b.Property<int?>("parentCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("parentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Entities.Enquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("businessTypeId")
                        .HasColumnType("int")
                        .HasColumnName("BusinessTypeId");

                    b.Property<string>("detail")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Detail");

                    b.Property<string>("loadingPortCity")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("LoadingPortCity");

                    b.Property<string>("loadingPortCountry")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("LoadingPortCountry");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Title");

                    b.Property<string>("unloadingPortCity")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("UnloadingPortCity");

                    b.Property<string>("unloadingPortCountry")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("UnloadingPortCountry");

                    b.HasKey("Id");

                    b.HasIndex("businessTypeId");

                    b.ToTable("Enquiries");
                });

            modelBuilder.Entity("Domain.Entities.ExceptionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClassName")
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.Property<int?>("LineNumber")
                        .HasColumnType("int");

                    b.Property<int?>("LogId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ExceptionLogs");
                });

            modelBuilder.Entity("Domain.Entities.FileRepo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("TableRef")
                        .HasColumnType("int")
                        .HasColumnName("TableRef");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("FileName");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("FilePath");

                    b.Property<int>("filePosition")
                        .HasColumnType("int")
                        .HasColumnName("FilePosition");

                    b.Property<int>("tableRefID")
                        .HasColumnType("int")
                        .HasColumnName("TableRefID");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.ToTable("FileRepos");
                });

            modelBuilder.Entity("Domain.Entities.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("logLevel")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("LogLevel");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Message");

                    b.Property<DateTime>("requestTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("RequestTime");

                    b.Property<DateTime?>("responseTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ResponseTime");

                    b.HasKey("Id");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("Domain.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("fileRepoId")
                        .HasColumnType("int")
                        .HasColumnName("FileRepoId");

                    b.Property<string>("newsContent")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("NewsContent");

                    b.Property<DateTime>("newsDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("NewsDate");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("validFrom")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ValidFrom");

                    b.Property<DateTime?>("validTill")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ValidTill");

                    b.HasKey("Id");

                    b.HasIndex("fileRepoId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Domain.Entities.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("fileRepoId")
                        .HasColumnType("int")
                        .HasColumnName("FileRepoId");

                    b.Property<DateTime?>("from")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("From");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("to")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("To");

                    b.Property<int?>("vendorId")
                        .HasColumnType("int")
                        .HasColumnName("VendorId");

                    b.HasKey("Id");

                    b.HasIndex("fileRepoId");

                    b.HasIndex("vendorId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("Domain.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Address");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("City");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Country");

                    b.Property<string>("designation")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Designation");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Email");

                    b.Property<string>("emails")
                        .HasColumnType("longtext")
                        .HasColumnName("Emails");

                    b.Property<string>("lic_no")
                        .HasColumnType("longtext")
                        .HasColumnName("LIC_No");

                    b.Property<int?>("logoId")
                        .HasColumnType("int")
                        .HasColumnName("LogoId");

                    b.Property<string>("mobileNo")
                        .HasColumnType("longtext")
                        .HasColumnName("MobileNo");

                    b.Property<string>("mobileNos")
                        .HasColumnType("longtext")
                        .HasColumnName("MobileNos");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Name");

                    b.Property<string>("personName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PersonName");

                    b.Property<string>("taxNo")
                        .HasColumnType("longtext")
                        .HasColumnName("TaxNo");

                    b.Property<string>("website")
                        .HasColumnType("longtext")
                        .HasColumnName("Website");

                    b.HasKey("Id");

                    b.HasIndex("logoId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes", (string)null);
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.Key", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("DataProtected")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsX509Certificate")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Use")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Use");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("Duende.IdentityServer.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("ConsumedTime");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Banner", b =>
                {
                    b.HasOne("Domain.Entities.FileRepo", "fileRepo")
                        .WithMany()
                        .HasForeignKey("fileRepoId");

                    b.Navigation("fileRepo");
                });

            modelBuilder.Entity("Domain.Entities.BusinessType", b =>
                {
                    b.HasOne("Domain.Entities.BusinessType", "ParentType")
                        .WithMany()
                        .HasForeignKey("parentTypeId");

                    b.Navigation("ParentType");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.HasOne("Domain.Entities.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("parentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Domain.Entities.Enquiry", b =>
                {
                    b.HasOne("Domain.Entities.BusinessType", "businessType")
                        .WithMany()
                        .HasForeignKey("businessTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("businessType");
                });

            modelBuilder.Entity("Domain.Entities.News", b =>
                {
                    b.HasOne("Domain.Entities.FileRepo", "fileRepo")
                        .WithMany()
                        .HasForeignKey("fileRepoId");

                    b.Navigation("fileRepo");
                });

            modelBuilder.Entity("Domain.Entities.Offer", b =>
                {
                    b.HasOne("Domain.Entities.FileRepo", "fileRepo")
                        .WithMany()
                        .HasForeignKey("fileRepoId");

                    b.HasOne("Domain.Entities.Vendor", "vendor")
                        .WithMany()
                        .HasForeignKey("vendorId");

                    b.Navigation("fileRepo");

                    b.Navigation("vendor");
                });

            modelBuilder.Entity("Domain.Entities.Vendor", b =>
                {
                    b.HasOne("Domain.Entities.FileRepo", "logoRepo")
                        .WithMany()
                        .HasForeignKey("logoId");

                    b.Navigation("logoRepo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
