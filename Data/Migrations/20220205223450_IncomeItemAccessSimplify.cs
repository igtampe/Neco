using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Igtampe.Neco.Data.Migrations
{
    public partial class IncomeItemAccessSimplify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKey_Airline_Account_AccountID1",
                table: "Airline");

            migrationBuilder.DropForeignKey(
                name: "FKey_Apartment_Account_AccountID",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FKey_Business_Account_AccountID",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FKey_Corporation_Account_AccountID",
                table: "Corporation");

            migrationBuilder.DropForeignKey(
                name: "FKey_Hotel_Account_AccountID",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "INDEX_Hotel_AccountID",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "INDEX_Corporation_AccountID",
                table: "Corporation");

            migrationBuilder.DropIndex(
                name: "INDEX_Business_AccountID",
                table: "Business");

            migrationBuilder.DropIndex(
                name: "INDEX_Apartment_AccountID",
                table: "Apartment");

            migrationBuilder.DropIndex(
                name: "INDEX_Airline_AccountID1",
                table: "Airline");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Corporation");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Apartment");

            migrationBuilder.DropColumn(
                name: "AccountID1",
                table: "Airline");

            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "IncomeItem",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "INDEX_IncomeItem_AccountID",
                table: "IncomeItem",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FKey_IncomeItem_Account_AccountID",
                table: "IncomeItem",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKey_IncomeItem_Account_AccountID",
                table: "IncomeItem");

            migrationBuilder.DropIndex(
                name: "INDEX_IncomeItem_AccountID",
                table: "IncomeItem");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "IncomeItem");

            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "Hotel",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "Corporation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "Business",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountID",
                table: "Apartment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountID1",
                table: "Airline",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "INDEX_Hotel_AccountID",
                table: "Hotel",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "INDEX_Corporation_AccountID",
                table: "Corporation",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "INDEX_Business_AccountID",
                table: "Business",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "INDEX_Apartment_AccountID",
                table: "Apartment",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "INDEX_Airline_AccountID1",
                table: "Airline",
                column: "AccountID1");

            migrationBuilder.AddForeignKey(
                name: "FKey_Airline_Account_AccountID1",
                table: "Airline",
                column: "AccountID1",
                principalTable: "Account",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FKey_Apartment_Account_AccountID",
                table: "Apartment",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FKey_Business_Account_AccountID",
                table: "Business",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FKey_Corporation_Account_AccountID",
                table: "Corporation",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FKey_Hotel_Account_AccountID",
                table: "Hotel",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "ID");
        }
    }
}
