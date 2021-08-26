﻿// <auto-generated />
using System;
using Igtampe.Neco.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Igtampe.Neco.Data.Migrations
{
    [DbContext(typeof(NecoContext))]
    partial class EverythingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Igtampe.Neco.Common.Bank", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Balance")
                        .HasColumnType("bigint");

                    b.Property<string>("BankId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TypeId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccountType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BankId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("BankAccountTypes");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CertifiedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CertifiedById")
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CertifiedById");

                    b.ToTable("CertifiedItems");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CheckbookItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AttachedTransacitonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Variant")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttachedTransacitonId");

                    b.ToTable("CheckbookItems");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Contractus.Contract", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FromUserId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopBidderId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("UpForAuction")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("FromUserId");

                    b.HasIndex("TopBidderId");

                    b.ToTable("Contracts");
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

                    b.Property<Guid?>("TaxUserInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("ID");

                    b.HasIndex("FederalJurisdictionID");

                    b.HasIndex("LocalJurisdictionID");

                    b.HasIndex("TaxUserInfoId");

                    b.HasIndex("UserId");

                    b.ToTable("IncomeItems");
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

                    b.ToTable("Appartments");
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

                    b.ToTable("Businesses");
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

                    b.ToTable("Hotels");
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

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("JurisdictionID");

                    b.HasIndex("TypeId");

                    b.ToTable("TaxBrackets");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxJurisdiction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AccountId");

                    b.ToTable("TaxJurisdictions");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxUserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TaxUserInfos");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Country", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FederalBankAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("FederalSalesTax")
                        .HasColumnType("float");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePerSquareMeter")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FederalBankAccountId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.District", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DistrictBankAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DistrictSalesTax")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Points")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePerSquareMeter")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.HasIndex("DistrictBankAccountId");

                    b.ToTable("Districts");
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

                    b.Property<string>("Points")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePerSquareMeter")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("TiedAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("DistrictID");

                    b.HasIndex("TiedAccountId");

                    b.ToTable("Plots");
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

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Road");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<bool>("Executed")
                        .HasColumnType("bit");

                    b.Property<bool>("Failed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("FromAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FromUserId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("Taxable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ToBankAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ToUserId")
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToBankAccountId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Transactions");
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

                    b.Property<Guid?>("IncomeItemID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<Guid?>("PlotID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SpecificLocaiton")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("IncomeItemID");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PlotID");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.UserAuth", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Pin")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("Id");

                    b.ToTable("UserAuths");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.UserType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccount", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankId");

                    b.HasOne("Igtampe.Neco.Common.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId");

                    b.HasOne("Igtampe.Neco.Common.BankAccountType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Bank");

                    b.Navigation("Owner");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.BankAccountType", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Bank", "Bank")
                        .WithMany("AccountTypes")
                        .HasForeignKey("BankId");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CertifiedItem", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "CertifiedBy")
                        .WithMany()
                        .HasForeignKey("CertifiedById");

                    b.Navigation("CertifiedBy");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.CheckbookItem", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Transaction", "AttachedTransaciton")
                        .WithMany()
                        .HasForeignKey("AttachedTransacitonId");

                    b.Navigation("AttachedTransaciton");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Contractus.Contract", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("Igtampe.Neco.Common.User", "TopBidder")
                        .WithMany()
                        .HasForeignKey("TopBidderId");

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

                    b.HasOne("Igtampe.Neco.Common.EzTax.TaxUserInfo", null)
                        .WithMany("Items")
                        .HasForeignKey("TaxUserInfoId");

                    b.HasOne("Igtampe.Neco.Common.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("FederalJurisdiction");

                    b.Navigation("LocalJurisdiction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Apartment", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.IncomeItem", "IncomeItem")
                        .WithMany()
                        .HasForeignKey("IncomeItemID");

                    b.Navigation("IncomeItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Business", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.IncomeItem", "IncomeItem")
                        .WithMany()
                        .HasForeignKey("IncomeItemID");

                    b.Navigation("IncomeItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.Subitems.Hotel", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.IncomeItem", "IncomeItem")
                        .WithMany()
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
                        .HasForeignKey("TypeId");

                    b.Navigation("Jurisdiction");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxJurisdiction", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.BankAccount", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxUserInfo", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Country", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.BankAccount", "FederalBankAccount")
                        .WithMany()
                        .HasForeignKey("FederalBankAccountId");

                    b.Navigation("FederalBankAccount");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.District", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.LandView.Country", "Country")
                        .WithMany("Districts")
                        .HasForeignKey("CountryID");

                    b.HasOne("Igtampe.Neco.Common.BankAccount", "DistrictBankAccount")
                        .WithMany()
                        .HasForeignKey("DistrictBankAccountId");

                    b.Navigation("Country");

                    b.Navigation("DistrictBankAccount");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.LandView.Plot", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.LandView.District", "District")
                        .WithMany("Plots")
                        .HasForeignKey("DistrictID");

                    b.HasOne("Igtampe.Neco.Common.BankAccount", "TiedAccount")
                        .WithMany()
                        .HasForeignKey("TiedAccountId");

                    b.Navigation("District");

                    b.Navigation("TiedAccount");
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
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Transaction", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.BankAccount", "FromAccount")
                        .WithMany()
                        .HasForeignKey("FromAccountId");

                    b.HasOne("Igtampe.Neco.Common.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("Igtampe.Neco.Common.BankAccount", "ToBankAccount")
                        .WithMany()
                        .HasForeignKey("ToBankAccountId");

                    b.HasOne("Igtampe.Neco.Common.User", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");

                    b.Navigation("FromAccount");

                    b.Navigation("FromUser");

                    b.Navigation("ToBankAccount");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.UMSAT.Asset", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.EzTax.IncomeItem", "IncomeItem")
                        .WithMany()
                        .HasForeignKey("IncomeItemID");

                    b.HasOne("Igtampe.Neco.Common.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("Igtampe.Neco.Common.LandView.Plot", "Plot")
                        .WithMany()
                        .HasForeignKey("PlotID");

                    b.Navigation("IncomeItem");

                    b.Navigation("Owner");

                    b.Navigation("Plot");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.User", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.UserType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Bank", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("AccountTypes");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxJurisdiction", b =>
                {
                    b.Navigation("Brackets");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.EzTax.TaxUserInfo", b =>
                {
                    b.Navigation("Items");
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
