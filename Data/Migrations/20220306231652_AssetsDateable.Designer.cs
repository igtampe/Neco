﻿// <auto-generated />
using System;
using Igtampe.Neco.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Igtampe.Neco.Data.Migrations
{
    [DbContext(typeof(NecoContext))]
    [Migration("20220306231652_AssetsDateable")]
    partial class AssetsDateable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountUser", b =>
                {
                    b.Property<string>("AccountsID")
                        .HasColumnType("text");

                    b.Property<string>("OwnersID")
                        .HasColumnType("text");

                    b.HasKey("AccountsID", "OwnersID");

                    b.HasIndex("OwnersID");

                    b.ToTable("AccountUser (Dictionary<string, object>)");
                });

            modelBuilder.Entity("AssetIncomeItem", b =>
                {
                    b.Property<Guid>("RelatedAssetsID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RelatedIncomeItemsID")
                        .HasColumnType("uuid");

                    b.HasKey("RelatedAssetsID", "RelatedIncomeItemsID");

                    b.HasIndex("RelatedIncomeItemsID");

                    b.ToTable("AssetIncomeItem (Dictionary<string, object>)");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Assets.Asset", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OwnerID")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Asset");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Banking.Account", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<long>("Balance")
                        .HasColumnType("bigint");

                    b.Property<string>("BankID")
                        .HasColumnType("text");

                    b.Property<bool>("Closed")
                        .HasColumnType("boolean");

                    b.Property<int>("IncomeType")
                        .HasColumnType("integer");

                    b.Property<string>("JurisdictionID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("PubliclyListed")
                        .HasColumnType("boolean");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.HasIndex("JurisdictionID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Banking.Bank", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Banking.Transaction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DestinationID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DestinationID");

                    b.HasIndex("OriginID");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Image", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Data")
                        .HasColumnType("bytea");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UploaderID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("UploaderID");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.IncomeItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountID")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<bool>("Approved")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JurisdictionID")
                        .HasColumnType("text");

                    b.Property<long>("MiscIncome")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.HasIndex("JurisdictionID");

                    b.ToTable("IncomeItem");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Notification", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Taxes.Bracket", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("End")
                        .HasColumnType("bigint");

                    b.Property<int>("IncomeType")
                        .HasColumnType("integer");

                    b.Property<string>("JurisdictionID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.Property<long>("Start")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("JurisdictionID");

                    b.ToTable("Bracket");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Taxes.Jurisdiction", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParentJurisdictionID")
                        .HasColumnType("text");

                    b.Property<int>("Population")
                        .HasColumnType("integer");

                    b.Property<string>("TiedAccountID")
                        .HasColumnType("text");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("ParentJurisdictionID");

                    b.HasIndex("TiedAccountID");

                    b.ToTable("Jurisdiction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Taxes.TaxReport", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountID")
                        .HasColumnType("text");

                    b.Property<string>("CSVReport")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateGenerated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ExtraIncome")
                        .HasColumnType("bigint");

                    b.Property<long>("ExtraIncomeTaxable")
                        .HasColumnType("bigint");

                    b.Property<long>("GrandTotalTax")
                        .HasColumnType("bigint");

                    b.Property<long>("StaticIncome")
                        .HasColumnType("bigint");

                    b.Property<string>("TextReport")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("TaxReport");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.User", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGov")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSDC")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUploader")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Assets.Building", b =>
                {
                    b.HasBaseType("Igtampe.Neco.Common.Assets.Asset");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("Beds")
                        .HasColumnType("integer");

                    b.Property<int>("BuildingType")
                        .HasColumnType("integer");

                    b.Property<string>("JurisdictionID")
                        .HasColumnType("text");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.Property<int>("Z")
                        .HasColumnType("integer");

                    b.HasIndex("JurisdictionID");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Assets.Unit", b =>
                {
                    b.HasBaseType("Igtampe.Neco.Common.Assets.Asset");

                    b.Property<Guid?>("BuildingID")
                        .HasColumnType("uuid");

                    b.HasIndex("BuildingID");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Apartment", b =>
                {
                    b.HasBaseType("Igtampe.Neco.Common.Income.IncomeItem");

                    b.Property<int>("B1Rent")
                        .HasColumnType("integer");

                    b.Property<int>("B1Units")
                        .HasColumnType("integer");

                    b.Property<int>("B2Rent")
                        .HasColumnType("integer");

                    b.Property<int>("B2Units")
                        .HasColumnType("integer");

                    b.Property<int>("B3Rent")
                        .HasColumnType("integer");

                    b.Property<int>("B3Units")
                        .HasColumnType("integer");

                    b.Property<int>("PRent")
                        .HasColumnType("integer");

                    b.Property<int>("PUnits")
                        .HasColumnType("integer");

                    b.Property<int>("SRent")
                        .HasColumnType("integer");

                    b.Property<int>("SUnits")
                        .HasColumnType("integer");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Business", b =>
                {
                    b.HasBaseType("Igtampe.Neco.Common.Income.IncomeItem");

                    b.Property<int>("AvgSpend")
                        .HasColumnType("integer");

                    b.Property<int>("CustPerHour")
                        .HasColumnType("integer");

                    b.Property<int>("HoursOpen")
                        .HasColumnType("integer");

                    b.Property<int>("PointsOfSale")
                        .HasColumnType("integer");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Corporation", b =>
                {
                    b.HasBaseType("Igtampe.Neco.Common.Income.IncomeItem");

                    b.Property<bool>("AirportAds")
                        .HasColumnType("boolean");

                    b.Property<int>("Buildings")
                        .HasColumnType("integer");

                    b.Property<bool>("International")
                        .HasColumnType("boolean");

                    b.Property<int>("Mergers")
                        .HasColumnType("integer");

                    b.Property<bool>("MetroAds")
                        .HasColumnType("boolean");

                    b.Property<string>("RLE")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("RLENetYearly")
                        .HasColumnType("bigint");

                    b.ToTable("Corporation");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Hotel", b =>
                {
                    b.HasBaseType("Igtampe.Neco.Common.Income.IncomeItem");

                    b.Property<int>("RoomRate")
                        .HasColumnType("integer");

                    b.Property<int>("Rooms")
                        .HasColumnType("integer");

                    b.Property<int>("SuiteRate")
                        .HasColumnType("integer");

                    b.Property<int>("Suites")
                        .HasColumnType("integer");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Airline", b =>
                {
                    b.HasBaseType("Igtampe.Neco.Common.Income.Corporation");

                    b.Property<int>("GatesLG")
                        .HasColumnType("integer");

                    b.Property<int>("GatesMD")
                        .HasColumnType("integer");

                    b.Property<int>("GatesSM")
                        .HasColumnType("integer");

                    b.ToTable("Airline");
                });

            modelBuilder.Entity("AccountUser", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Banking.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Igtampe.Neco.Common.User", null)
                        .WithMany()
                        .HasForeignKey("OwnersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetIncomeItem", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Assets.Asset", null)
                        .WithMany()
                        .HasForeignKey("RelatedAssetsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Igtampe.Neco.Common.Income.IncomeItem", null)
                        .WithMany()
                        .HasForeignKey("RelatedIncomeItemsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Assets.Asset", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Banking.Account", "Owner")
                        .WithMany("Assets")
                        .HasForeignKey("OwnerID");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Banking.Account", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Banking.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankID");

                    b.HasOne("Igtampe.Neco.Common.Taxes.Jurisdiction", "Jurisdiction")
                        .WithMany("AccountsLocatedIn")
                        .HasForeignKey("JurisdictionID");

                    b.Navigation("Bank");

                    b.Navigation("Jurisdiction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Banking.Transaction", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Banking.Account", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationID");

                    b.HasOne("Igtampe.Neco.Common.Banking.Account", "Origin")
                        .WithMany()
                        .HasForeignKey("OriginID");

                    b.Navigation("Destination");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Image", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "Uploader")
                        .WithMany()
                        .HasForeignKey("UploaderID");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.IncomeItem", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Banking.Account", "Account")
                        .WithMany("IncomeItems")
                        .HasForeignKey("AccountID");

                    b.HasOne("Igtampe.Neco.Common.Taxes.Jurisdiction", "Jurisdiction")
                        .WithMany()
                        .HasForeignKey("JurisdictionID");

                    b.Navigation("Account");

                    b.Navigation("Jurisdiction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Notification", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Taxes.Bracket", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Taxes.Jurisdiction", "Jurisdiction")
                        .WithMany("Brackets")
                        .HasForeignKey("JurisdictionID");

                    b.Navigation("Jurisdiction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Taxes.Jurisdiction", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Taxes.Jurisdiction", "ParentJurisdiction")
                        .WithMany("ChildJurisdictions")
                        .HasForeignKey("ParentJurisdictionID");

                    b.HasOne("Igtampe.Neco.Common.Banking.Account", "TiedAccount")
                        .WithMany()
                        .HasForeignKey("TiedAccountID");

                    b.Navigation("ParentJurisdiction");

                    b.Navigation("TiedAccount");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Taxes.TaxReport", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Banking.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Assets.Building", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Assets.Asset", null)
                        .WithOne()
                        .HasForeignKey("Igtampe.Neco.Common.Assets.Building", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Igtampe.Neco.Common.Taxes.Jurisdiction", "Jurisdiction")
                        .WithMany()
                        .HasForeignKey("JurisdictionID");

                    b.Navigation("Jurisdiction");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Assets.Unit", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Assets.Building", "Building")
                        .WithMany("Units")
                        .HasForeignKey("BuildingID");

                    b.HasOne("Igtampe.Neco.Common.Assets.Asset", null)
                        .WithOne()
                        .HasForeignKey("Igtampe.Neco.Common.Assets.Unit", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Apartment", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Income.IncomeItem", null)
                        .WithOne()
                        .HasForeignKey("Igtampe.Neco.Common.Income.Apartment", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Business", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Income.IncomeItem", null)
                        .WithOne()
                        .HasForeignKey("Igtampe.Neco.Common.Income.Business", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Corporation", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Income.IncomeItem", null)
                        .WithOne()
                        .HasForeignKey("Igtampe.Neco.Common.Income.Corporation", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Hotel", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Income.IncomeItem", null)
                        .WithOne()
                        .HasForeignKey("Igtampe.Neco.Common.Income.Hotel", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Income.Airline", b =>
                {
                    b.HasOne("Igtampe.Neco.Common.Income.Corporation", null)
                        .WithOne()
                        .HasForeignKey("Igtampe.Neco.Common.Income.Airline", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Banking.Account", b =>
                {
                    b.Navigation("Assets");

                    b.Navigation("IncomeItems");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Banking.Bank", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Taxes.Jurisdiction", b =>
                {
                    b.Navigation("AccountsLocatedIn");

                    b.Navigation("Brackets");

                    b.Navigation("ChildJurisdictions");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.User", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("Igtampe.Neco.Common.Assets.Building", b =>
                {
                    b.Navigation("Units");
                });
#pragma warning restore 612, 618
        }
    }
}
