using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Igtampe.Neco.Data.Migrations
{
    public partial class everything : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsGov = table.Column<bool>(type: "boolean", nullable: false),
                    IsSDC = table.Column<bool>(type: "boolean", nullable: false),
                    IsUploader = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UploaderID = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Image_User_UploaderID",
                        column: x => x.UploaderID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    PubliclyListed = table.Column<bool>(type: "boolean", nullable: false),
                    Closed = table.Column<bool>(type: "boolean", nullable: false),
                    BankID = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    JurisdictionID = table.Column<string>(type: "text", nullable: true),
                    IncomeType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Account_Bank_BankID",
                        column: x => x.BankID,
                        principalTable: "Bank",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AccountUser (Dictionary<string, object>)",
                columns: table => new
                {
                    AccountsID = table.Column<string>(type: "text", nullable: false),
                    OwnersID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUser (Dictionary<string, object>)", x => new { x.AccountsID, x.OwnersID });
                    table.ForeignKey(
                        name: "FK_AccountUser (Dictionary<string, object>)_Account_AccountsID",
                        column: x => x.AccountsID,
                        principalTable: "Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUser (Dictionary<string, object>)_User_OwnersID",
                        column: x => x.OwnersID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OwnerID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Asset_Account_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Account",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Jurisdiction",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Population = table.Column<int>(type: "integer", nullable: false),
                    TiedAccountID = table.Column<string>(type: "text", nullable: true),
                    ParentJurisdictionID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurisdiction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Jurisdiction_Account_TiedAccountID",
                        column: x => x.TiedAccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Jurisdiction_Jurisdiction_ParentJurisdictionID",
                        column: x => x.ParentJurisdictionID,
                        principalTable: "Jurisdiction",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TaxReport",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountID = table.Column<string>(type: "text", nullable: true),
                    StaticIncome = table.Column<long>(type: "bigint", nullable: false),
                    ExtraIncome = table.Column<long>(type: "bigint", nullable: false),
                    ExtraIncomeTaxable = table.Column<long>(type: "bigint", nullable: false),
                    GrandTotalTax = table.Column<long>(type: "bigint", nullable: false),
                    TextReport = table.Column<string>(type: "text", nullable: false),
                    CSVReport = table.Column<string>(type: "text", nullable: false),
                    DateGenerated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxReport", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaxReport_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginID = table.Column<string>(type: "text", nullable: true),
                    DestinationID = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_DestinationID",
                        column: x => x.DestinationID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Transaction_Account_OriginID",
                        column: x => x.OriginID,
                        principalTable: "Account",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Bracket",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    IncomeType = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<long>(type: "bigint", nullable: false),
                    End = table.Column<long>(type: "bigint", nullable: false),
                    JurisdictionID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bracket", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bracket_Jurisdiction_JurisdictionID",
                        column: x => x.JurisdictionID,
                        principalTable: "Jurisdiction",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingType = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    JurisdictionID = table.Column<string>(type: "text", nullable: true),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Z = table.Column<int>(type: "integer", nullable: false),
                    Beds = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Building_Asset_ID",
                        column: x => x.ID,
                        principalTable: "Asset",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Building_Jurisdiction_JurisdictionID",
                        column: x => x.JurisdictionID,
                        principalTable: "Jurisdiction",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "IncomeItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    JurisdictionID = table.Column<string>(type: "text", nullable: true),
                    AccountID = table.Column<string>(type: "text", nullable: true),
                    MiscIncome = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Approved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomeItem_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_IncomeItem_Jurisdiction_JurisdictionID",
                        column: x => x.JurisdictionID,
                        principalTable: "Jurisdiction",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Unit_Asset_ID",
                        column: x => x.ID,
                        principalTable: "Asset",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Unit_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    SUnits = table.Column<int>(type: "integer", nullable: false),
                    B1Units = table.Column<int>(type: "integer", nullable: false),
                    B2Units = table.Column<int>(type: "integer", nullable: false),
                    B3Units = table.Column<int>(type: "integer", nullable: false),
                    PUnits = table.Column<int>(type: "integer", nullable: false),
                    SRent = table.Column<int>(type: "integer", nullable: false),
                    B1Rent = table.Column<int>(type: "integer", nullable: false),
                    B2Rent = table.Column<int>(type: "integer", nullable: false),
                    B3Rent = table.Column<int>(type: "integer", nullable: false),
                    PRent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Apartment_IncomeItem_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetIncomeItem (Dictionary<string, object>)",
                columns: table => new
                {
                    RelatedAssetsID = table.Column<Guid>(type: "uuid", nullable: false),
                    RelatedIncomeItemsID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetIncomeItem (Dictionary<string, object>)", x => new { x.RelatedAssetsID, x.RelatedIncomeItemsID });
                    table.ForeignKey(
                        name: "FK_AssetIncomeItem (Dictionary<string, object>)_Asset_RelatedA~",
                        column: x => x.RelatedAssetsID,
                        principalTable: "Asset",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetIncomeItem (Dictionary<string, object>)_IncomeItem_Rel~",
                        column: x => x.RelatedIncomeItemsID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PointsOfSale = table.Column<int>(type: "integer", nullable: false),
                    AvgSpend = table.Column<int>(type: "integer", nullable: false),
                    CustPerHour = table.Column<int>(type: "integer", nullable: false),
                    HoursOpen = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Business_IncomeItem_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Corporation",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    RLE = table.Column<string>(type: "text", nullable: false),
                    RLENetYearly = table.Column<long>(type: "bigint", nullable: false),
                    Buildings = table.Column<int>(type: "integer", nullable: false),
                    Mergers = table.Column<int>(type: "integer", nullable: false),
                    MetroAds = table.Column<bool>(type: "boolean", nullable: false),
                    AirportAds = table.Column<bool>(type: "boolean", nullable: false),
                    International = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Corporation_IncomeItem_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Rooms = table.Column<int>(type: "integer", nullable: false),
                    Suites = table.Column<int>(type: "integer", nullable: false),
                    RoomRate = table.Column<int>(type: "integer", nullable: false),
                    SuiteRate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hotel_IncomeItem_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    GatesSM = table.Column<int>(type: "integer", nullable: false),
                    GatesMD = table.Column<int>(type: "integer", nullable: false),
                    GatesLG = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Airline_Corporation_ID",
                        column: x => x.ID,
                        principalTable: "Corporation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_BankID",
                table: "Account",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Account_JurisdictionID",
                table: "Account",
                column: "JurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUser (Dictionary<string, object>)_OwnersID",
                table: "AccountUser (Dictionary<string, object>)",
                column: "OwnersID");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_OwnerID",
                table: "Asset",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIncomeItem (Dictionary<string, object>)_RelatedIncomeI~",
                table: "AssetIncomeItem (Dictionary<string, object>)",
                column: "RelatedIncomeItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_Bracket_JurisdictionID",
                table: "Bracket",
                column: "JurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Building_JurisdictionID",
                table: "Building",
                column: "JurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_UploaderID",
                table: "Image",
                column: "UploaderID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItem_AccountID",
                table: "IncomeItem",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItem_JurisdictionID",
                table: "IncomeItem",
                column: "JurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Jurisdiction_ParentJurisdictionID",
                table: "Jurisdiction",
                column: "ParentJurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Jurisdiction_TiedAccountID",
                table: "Jurisdiction",
                column: "TiedAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserID",
                table: "Notification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxReport_AccountID",
                table: "TaxReport",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DestinationID",
                table: "Transaction",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OriginID",
                table: "Transaction",
                column: "OriginID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_BuildingID",
                table: "Unit",
                column: "BuildingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Jurisdiction_JurisdictionID",
                table: "Account",
                column: "JurisdictionID",
                principalTable: "Jurisdiction",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Bank_BankID",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_Jurisdiction_JurisdictionID",
                table: "Account");

            migrationBuilder.DropTable(
                name: "AccountUser (Dictionary<string, object>)");

            migrationBuilder.DropTable(
                name: "Airline");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "AssetIncomeItem (Dictionary<string, object>)");

            migrationBuilder.DropTable(
                name: "Bracket");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "TaxReport");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Corporation");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "IncomeItem");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Jurisdiction");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
