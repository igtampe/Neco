using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    public partial class ChangeRoadWidthToThickness : Migration
    {
        /// <summary></summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_IncomeItem_IncomeItemID",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_IncomeItemID",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "IncomeItemID",
                table: "Asset");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Road",
                newName: "Thickness");
        }

        /// <summary></summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thickness",
                table: "Road",
                newName: "Width");

            migrationBuilder.AddColumn<Guid>(
                name: "IncomeItemID",
                table: "Asset",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_IncomeItemID",
                table: "Asset",
                column: "IncomeItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_IncomeItem_IncomeItemID",
                table: "Asset",
                column: "IncomeItemID",
                principalTable: "IncomeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
