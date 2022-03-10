using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Igtampe.Neco.Data.Migrations
{
    public partial class AddAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Asset_OwnerID",
                table: "Asset",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIncomeItem (Dictionary<string, object>)_RelatedIncomeI~",
                table: "AssetIncomeItem (Dictionary<string, object>)",
                column: "RelatedIncomeItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_Building_JurisdictionID",
                table: "Building",
                column: "JurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_BuildingID",
                table: "Unit",
                column: "BuildingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetIncomeItem (Dictionary<string, object>)");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Asset");
        }
    }
}
