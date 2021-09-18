using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    public partial class Restart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountDetail",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserAuth",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Pin = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuth", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    InterestRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankAccountType_Bank_BankID",
                        column: x => x.BankID,
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_UserType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "UserType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    BankID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    DetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Closed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankAccount_Bank_BankID",
                        column: x => x.BankID,
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_BankAccountDetail_DetailsID",
                        column: x => x.DetailsID,
                        principalTable: "BankAccountDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_BankAccountType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "BankAccountType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_User_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertifiedItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertifiedByID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertifiedItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CertifiedItem_User_CertifiedByID",
                        column: x => x.CertifiedByID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromUserID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    TopBidderID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contract_User_FromUserID",
                        column: x => x.FromUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_User_TopBidderID",
                        column: x => x.TopBidderID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxReport",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    PreparedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaticIncome = table.Column<long>(type: "bigint", nullable: false),
                    ExtraIncome = table.Column<long>(type: "bigint", nullable: false),
                    ExtraIncomeTaxable = table.Column<long>(type: "bigint", nullable: false),
                    GrandTotalTax = table.Column<long>(type: "bigint", nullable: false),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CSVReport = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxReport", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaxReport_User_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    PricePerSquareMeter = table.Column<int>(type: "int", nullable: false),
                    FederalBankAccountID = table.Column<string>(type: "nvarchar(9)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Country_BankAccount_FederalBankAccountID",
                        column: x => x.FederalBankAccountID,
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxJurisdiction",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountID = table.Column<string>(type: "nvarchar(9)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxJurisdiction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaxJurisdiction_BankAccount_AccountID",
                        column: x => x.AccountID,
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromAccountID = table.Column<string>(type: "nvarchar(9)", nullable: true),
                    ToAccountID = table.Column<string>(type: "nvarchar(9)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_BankAccount_FromAccountID",
                        column: x => x.FromAccountID,
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_BankAccount_ToAccountID",
                        column: x => x.ToAccountID,
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerSquareMeter = table.Column<int>(type: "int", nullable: false),
                    DistrictBankAccountID = table.Column<string>(type: "nvarchar(9)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.ID);
                    table.ForeignKey(
                        name: "FK_District_BankAccount_DistrictBankAccountID",
                        column: x => x.DistrictBankAccountID,
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_District_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Road",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Road", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Road_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncomeItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    MiscIncome = table.Column<long>(type: "bigint", nullable: false),
                    LocalJurisdictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FederalJurisdictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomeItem_TaxJurisdiction_FederalJurisdictionID",
                        column: x => x.FederalJurisdictionID,
                        principalTable: "TaxJurisdiction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncomeItem_TaxJurisdiction_LocalJurisdictionID",
                        column: x => x.LocalJurisdictionID,
                        principalTable: "TaxJurisdiction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncomeItem_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxBracket",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JurisdictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<long>(type: "bigint", nullable: false),
                    End = table.Column<long>(type: "bigint", nullable: false),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBracket", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaxBracket_TaxJurisdiction_JurisdictionID",
                        column: x => x.JurisdictionID,
                        principalTable: "TaxJurisdiction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxBracket_UserType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "UserType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckbookItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachedTransacitonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Variant = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckbookItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckbookItem_Transaction_AttachedTransacitonID",
                        column: x => x.AttachedTransacitonID,
                        principalTable: "Transaction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plot",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plot", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plot_District_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plot_User_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncomeItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SUnits = table.Column<int>(type: "int", nullable: false),
                    B1Units = table.Column<int>(type: "int", nullable: false),
                    B2Units = table.Column<int>(type: "int", nullable: false),
                    B3Units = table.Column<int>(type: "int", nullable: false),
                    PUnits = table.Column<int>(type: "int", nullable: false),
                    SRent = table.Column<int>(type: "int", nullable: false),
                    B1Rent = table.Column<int>(type: "int", nullable: false),
                    B2Rent = table.Column<int>(type: "int", nullable: false),
                    B3Rent = table.Column<int>(type: "int", nullable: false),
                    PRent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Apartment_IncomeItem_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncomeItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chairs = table.Column<int>(type: "int", nullable: false),
                    AvgSpend = table.Column<int>(type: "int", nullable: false),
                    CustPerHour = table.Column<int>(type: "int", nullable: false),
                    HoursOpen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Business_IncomeItem_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncomeItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    Suites = table.Column<int>(type: "int", nullable: false),
                    RoomRate = table.Column<int>(type: "int", nullable: false),
                    SuiteRate = table.Column<int>(type: "int", nullable: false),
                    MiscIncome = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hotel_IncomeItem_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificLocaiton = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerID = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Complete = table.Column<bool>(type: "bit", nullable: false),
                    IncomeItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlotID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Asset_IncomeItem_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asset_Plot_PlotID",
                        column: x => x.PlotID,
                        principalTable: "Plot",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asset_User_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_IncomeItemID",
                table: "Apartment",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_IncomeItemID",
                table: "Asset",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_OwnerID",
                table: "Asset",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_PlotID",
                table: "Asset",
                column: "PlotID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankID",
                table: "BankAccount",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_DetailsID",
                table: "BankAccount",
                column: "DetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_OwnerID",
                table: "BankAccount",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_TypeID",
                table: "BankAccount",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountType_BankID",
                table: "BankAccountType",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Business_IncomeItemID",
                table: "Business",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CertifiedItem_CertifiedByID",
                table: "CertifiedItem",
                column: "CertifiedByID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckbookItem_AttachedTransacitonID",
                table: "CheckbookItem",
                column: "AttachedTransacitonID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_FromUserID",
                table: "Contract",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_TopBidderID",
                table: "Contract",
                column: "TopBidderID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_FederalBankAccountID",
                table: "Country",
                column: "FederalBankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_District_CountryID",
                table: "District",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_District_DistrictBankAccountID",
                table: "District",
                column: "DistrictBankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_IncomeItemID",
                table: "Hotel",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItem_FederalJurisdictionID",
                table: "IncomeItem",
                column: "FederalJurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItem_LocalJurisdictionID",
                table: "IncomeItem",
                column: "LocalJurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItem_UserID",
                table: "IncomeItem",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserID",
                table: "Notification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_DistrictID",
                table: "Plot",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_OwnerID",
                table: "Plot",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Road_CountryID",
                table: "Road",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxBracket_JurisdictionID",
                table: "TaxBracket",
                column: "JurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxBracket_TypeID",
                table: "TaxBracket",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxJurisdiction_AccountID",
                table: "TaxJurisdiction",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxReport_OwnerID",
                table: "TaxReport",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FromAccountID",
                table: "Transaction",
                column: "FromAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ToAccountID",
                table: "Transaction",
                column: "ToAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_User_TypeID",
                table: "User",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "CertifiedItem");

            migrationBuilder.DropTable(
                name: "CheckbookItem");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Road");

            migrationBuilder.DropTable(
                name: "TaxBracket");

            migrationBuilder.DropTable(
                name: "TaxReport");

            migrationBuilder.DropTable(
                name: "UserAuth");

            migrationBuilder.DropTable(
                name: "Plot");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "IncomeItem");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "TaxJurisdiction");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankAccountDetail");

            migrationBuilder.DropTable(
                name: "BankAccountType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
