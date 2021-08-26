using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    public partial class BankAddOwner : Migration
    {
        /// <summary>Ups migration</summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_UserId",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BankAccounts",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_UserId",
                table: "BankAccounts",
                newName: "IX_BankAccounts_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_OwnerId",
                table: "BankAccounts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <summary>Downs migration</summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_OwnerId",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "BankAccounts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_OwnerId",
                table: "BankAccounts",
                newName: "IX_BankAccounts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_UserId",
                table: "BankAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
