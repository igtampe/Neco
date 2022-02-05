using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Igtampe.Neco.Data.Migrations
{
    public partial class IncomeItemRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airline_Account_AccountID1",
                table: "Airline");

            migrationBuilder.DropIndex(
                name: "IX_Airline_AccountID1",
                table: "Airline");

            migrationBuilder.DropColumn(
                name: "AccountID1",
                table: "Airline");

            migrationBuilder.CreateTable(
                name: "IncomeItem2",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    JurisdictionID = table.Column<string>(type: "text", nullable: true),
                    MiscIncome = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Approved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeItem2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomeItem2_Jurisdiction_JurisdictionID",
                        column: x => x.JurisdictionID,
                        principalTable: "Jurisdiction",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Apartment2",
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
                    PRent = table.Column<int>(type: "integer", nullable: false),
                    AccountID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Apartment2_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Apartment2_IncomeItem2_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem2",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Business2",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PointsOfSale = table.Column<int>(type: "integer", nullable: false),
                    AvgSpend = table.Column<int>(type: "integer", nullable: false),
                    CustPerHour = table.Column<int>(type: "integer", nullable: false),
                    HoursOpen = table.Column<int>(type: "integer", nullable: false),
                    AccountID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Business2_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Business2_IncomeItem2_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem2",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Corporation2",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    RLE = table.Column<string>(type: "text", nullable: false),
                    RLENetYearly = table.Column<long>(type: "bigint", nullable: false),
                    Buildings = table.Column<int>(type: "integer", nullable: false),
                    Mergers = table.Column<int>(type: "integer", nullable: false),
                    MetroAds = table.Column<bool>(type: "boolean", nullable: false),
                    AirportAds = table.Column<bool>(type: "boolean", nullable: false),
                    International = table.Column<bool>(type: "boolean", nullable: false),
                    AccountID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Corporation2_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Corporation2_IncomeItem2_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem2",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotel2",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Rooms = table.Column<int>(type: "integer", nullable: false),
                    Suites = table.Column<int>(type: "integer", nullable: false),
                    RoomRate = table.Column<int>(type: "integer", nullable: false),
                    SuiteRate = table.Column<int>(type: "integer", nullable: false),
                    AccountID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hotel2_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Hotel2_IncomeItem2_ID",
                        column: x => x.ID,
                        principalTable: "IncomeItem2",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Airline2",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    GatesSM = table.Column<int>(type: "integer", nullable: false),
                    GatesMD = table.Column<int>(type: "integer", nullable: false),
                    GatesLG = table.Column<int>(type: "integer", nullable: false),
                    AccountID1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Airline2_Account_AccountID1",
                        column: x => x.AccountID1,
                        principalTable: "Account",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Airline2_Corporation2_ID",
                        column: x => x.ID,
                        principalTable: "Corporation2",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airline2_AccountID1",
                table: "Airline2",
                column: "AccountID1");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment2_AccountID",
                table: "Apartment2",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Business2_AccountID",
                table: "Business2",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Corporation2_AccountID",
                table: "Corporation2",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel2_AccountID",
                table: "Hotel2",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItem2_JurisdictionID",
                table: "IncomeItem2",
                column: "JurisdictionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airline2");

            migrationBuilder.DropTable(
                name: "Apartment2");

            migrationBuilder.DropTable(
                name: "Business2");

            migrationBuilder.DropTable(
                name: "Hotel2");

            migrationBuilder.DropTable(
                name: "Corporation2");

            migrationBuilder.DropTable(
                name: "IncomeItem2");

            migrationBuilder.AddColumn<string>(
                name: "AccountID1",
                table: "Airline",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Airline_AccountID1",
                table: "Airline",
                column: "AccountID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Airline_Account_AccountID1",
                table: "Airline",
                column: "AccountID1",
                principalTable: "Account",
                principalColumn: "ID");
        }
    }
}
