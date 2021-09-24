﻿// <auto-generated />
using System;
using Igtampe.Neco.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Igtampe.Neco.Data.Migrations
{
    /// <summary></summary>
    [DbContext(typeof(NecoContext))]
    [Migration("20210924190624_ChangeRoadWidthToThickness")]
    partial class ChangeRoadWidthToThickness
    {
        /// <summary></summary>
        /// <param name="modelBuilder"></param>
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Igtampe.Neco.Common.Bank", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccount", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("BankID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("Closed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DetailsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OwnerID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<Guid?>("TypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.HasIndex("DetailsID");

                    b.HasIndex("OwnerID");

                    b.HasIndex("TypeID");

                    b.ToTable("BankAccount");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccountDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Balance")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.ToTable("BankAccountDetail");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccountType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BankID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.ToTable("BankAccountType");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CertifiedItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CertifiedByID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CertifiedByID");

                    b.ToTable("CertifiedItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CheckbookItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AttachedTransacitonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Variant")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AttachedTransacitonID");

                    b.ToTable("CheckbookItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Contractus.Contract", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromUserID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TopBidderID")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("ID");

                    b.HasIndex("FromUserID");

                    b.HasIndex("TopBidderID");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.IncomeItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FederalJurisdictionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LocalJurisdictionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("MiscIncome")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("ID");

                    b.HasIndex("FederalJurisdictionID");

                    b.HasIndex("LocalJurisdictionID");

                    b.HasIndex("UserID");

                    b.ToTable("IncomeItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Apartment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("B1Rent")
                        .HasColumnType("int");

                    b.Property<int>("B1Units")
                        .HasColumnType("int");

                    b.Property<int>("B2Rent")
                        .HasColumnType("int");

                    b.Property<int>("B2Units")
                        .HasColumnType("int");

                    b.Property<int>("B3Rent")
                        .HasColumnType("int");

                    b.Property<int>("B3Units")
                        .HasColumnType("int");

                    b.Property<Guid?>("IncomeItemID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PRent")
                        .HasColumnType("int");

                    b.Property<int>("PUnits")
                        .HasColumnType("int");

                    b.Property<int>("SRent")
                        .HasColumnType("int");

                    b.Property<int>("SUnits")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IncomeItemID");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Business", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvgSpend")
                        .HasColumnType("int");

                    b.Property<int>("Chairs")
                        .HasColumnType("int");

                    b.Property<int>("CustPerHour")
                        .HasColumnType("int");

                    b.Property<int>("HoursOpen")
                        .HasColumnType("int");

                    b.Property<Guid?>("IncomeItemID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("IncomeItemID");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Hotel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IncomeItemID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MiscIncome")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomRate")
                        .HasColumnType("int");

                    b.Property<int>("Rooms")
                        .HasColumnType("int");

                    b.Property<int>("SuiteRate")
                        .HasColumnType("int");

                    b.Property<int>("Suites")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IncomeItemID");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxBracket", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("End")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("JurisdictionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<long>("Start")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("TypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("JurisdictionID");

                    b.HasIndex("TypeID");

                    b.ToTable("TaxBracket");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxJurisdiction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountID")
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("TaxJurisdiction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxReport", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CSVReport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ExtraIncome")
                        .HasColumnType("bigint");

                    b.Property<long>("ExtraIncomeTaxable")
                        .HasColumnType("bigint");

                    b.Property<long>("GrandTotalTax")
                        .HasColumnType("bigint");

                    b.Property<string>("OwnerID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("PreparedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Report")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("StaticIncome")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("TaxReport");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Country", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FederalBankAccountID")
                        .HasColumnType("nvarchar(9)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePerSquareMeter")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FederalBankAccountID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.District", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DistrictBankAccountID")
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Points")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePerSquareMeter")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("DistrictBankAccountID");

                    b.ToTable("District");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Plot", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DistrictID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Points")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DistrictID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Plot");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Road", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Points")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Thickness")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Road");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Notification", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Transaction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("FromAccountID")
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("ToAccountID")
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("ID");

                    b.HasIndex("FromAccountID");

                    b.HasIndex("ToAccountID");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.UMSAT.Asset", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Complete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<Guid?>("PlotID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SpecificLocaiton")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.HasIndex("PlotID");

                    b.ToTable("Asset");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.User", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("TypeID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.UserAuth", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Pin")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("ID");

                    b.ToTable("UserAuth");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.UserType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Taxation")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccount", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankID");

                    b.HasOne("Igtampe.Neco.Common.BankAccountDetail", "Details")
                        .WithMany()
                        .HasForeignKey("DetailsID");

                    b.HasOne("Igtampe.Neco.Common.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerID");

                    b.HasOne("Igtampe.Neco.Common.BankAccountType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID");

                    b.Navigation("Bank");

                    b.Navigation("Details");

                    b.Navigation("Owner");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccountType", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Bank", "Bank")
                        .WithMany("AccountTypes")
                        .HasForeignKey("BankID");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CertifiedItem", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "CertifiedBy")
                        .WithMany()
                        .HasForeignKey("CertifiedByID");

                    b.Navigation("CertifiedBy");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CheckbookItem", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Transaction", "AttachedTransaciton")
                        .WithMany()
                        .HasForeignKey("AttachedTransacitonID");

                    b.Navigation("AttachedTransaciton");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Contractus.Contract", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserID");

                    b.HasOne("Igtampe.Neco.Common.User", "TopBidder")
                        .WithMany()
                        .HasForeignKey("TopBidderID");

                    b.Navigation("FromUser");

                    b.Navigation("TopBidder");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.IncomeItem", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.TaxJurisdiction", "FederalJurisdiction")
                        .WithMany()
                        .HasForeignKey("FederalJurisdictionID");

                    b.HasOne("Igtampe.Neco.Common.EzTax.TaxJurisdiction", "LocalJurisdiction")
                        .WithMany()
                        .HasForeignKey("LocalJurisdictionID");

                    b.HasOne("Igtampe.Neco.Common.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("FederalJurisdiction");

                    b.Navigation("LocalJurisdiction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Apartment", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.IncomeItem", "IncomeItem")
                        .WithMany("Apartments")
                        .HasForeignKey("IncomeItemID");

                    b.Navigation("IncomeItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Business", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.IncomeItem", "IncomeItem")
                        .WithMany("Businesses")
                        .HasForeignKey("IncomeItemID");

                    b.Navigation("IncomeItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Hotel", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.IncomeItem", "IncomeItem")
                        .WithMany("Hotels")
                        .HasForeignKey("IncomeItemID");

                    b.Navigation("IncomeItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxBracket", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.TaxJurisdiction", "Jurisdiction")
                        .WithMany("Brackets")
                        .HasForeignKey("JurisdictionID");

                    b.HasOne("Igtampe.Neco.Common.UserType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID");

                    b.Navigation("Jurisdiction");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxJurisdiction", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.BankAccount", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxReport", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Country", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.BankAccount", "FederalBankAccount")
                        .WithMany()
                        .HasForeignKey("FederalBankAccountID");

                    b.Navigation("FederalBankAccount");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.District", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.LandView.Country", "Country")
                        .WithMany("Districts")
                        .HasForeignKey("CountryID");

                    b.HasOne("Igtampe.Neco.Common.BankAccount", "DistrictBankAccount")
                        .WithMany()
                        .HasForeignKey("DistrictBankAccountID");

                    b.Navigation("Country");

                    b.Navigation("DistrictBankAccount");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Plot", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.LandView.District", "District")
                        .WithMany("Plots")
                        .HasForeignKey("DistrictID");

                    b.HasOne("Igtampe.Neco.Common.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID");

                    b.Navigation("District");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Road", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.LandView.Country", "Country")
                        .WithMany("Roads")
                        .HasForeignKey("CountryID");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Notification", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Transaction", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.BankAccount", "FromAccount")
                        .WithMany()
                        .HasForeignKey("FromAccountID");

                    b.HasOne("Igtampe.Neco.Common.BankAccount", "ToAccount")
                        .WithMany()
                        .HasForeignKey("ToAccountID");

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.UMSAT.Asset", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID");

                    b.HasOne("Igtampe.Neco.Common.LandView.Plot", "Plot")
                        .WithMany()
                        .HasForeignKey("PlotID");

                    b.Navigation("Owner");

                    b.Navigation("Plot");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.User", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.UserType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Bank", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("AccountTypes");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.IncomeItem", b =>
                {
                    b.Navigation("Apartments");

                    b.Navigation("Businesses");

                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxJurisdiction", b =>
                {
                    b.Navigation("Brackets");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Country", b =>
                {
                    b.Navigation("Districts");

                    b.Navigation("Roads");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.District", b =>
                {
                    b.Navigation("Plots");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
