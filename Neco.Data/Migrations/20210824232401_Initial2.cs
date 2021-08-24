using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appartments_Items_IncomeItemID",
                table: "Appartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Items_IncomeItemID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Brackets_Jurisdictions_JurisdictionID",
                table: "Brackets");

            migrationBuilder.DropForeignKey(
                name: "FK_Brackets_UserTypes_TypeId",
                table: "Brackets");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Items_IncomeItemID",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Items_IncomeItemID",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Jurisdictions_FederalJurisdictionID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Jurisdictions_LocalJurisdictionID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_UserInfos_TaxUserInfoId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Jurisdictions_BankAccounts_AccountId",
                table: "Jurisdictions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jurisdictions",
                table: "Jurisdictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brackets",
                table: "Brackets");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "TaxUserInfos");

            migrationBuilder.RenameTable(
                name: "Jurisdictions",
                newName: "TaxJurisdictions");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "IncomeItems");

            migrationBuilder.RenameTable(
                name: "Brackets",
                newName: "TaxBrackets");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_UserId",
                table: "TaxUserInfos",
                newName: "IX_TaxUserInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Jurisdictions_AccountId",
                table: "TaxJurisdictions",
                newName: "IX_TaxJurisdictions_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_UserId",
                table: "IncomeItems",
                newName: "IX_IncomeItems_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_TaxUserInfoId",
                table: "IncomeItems",
                newName: "IX_IncomeItems_TaxUserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_LocalJurisdictionID",
                table: "IncomeItems",
                newName: "IX_IncomeItems_LocalJurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_Items_FederalJurisdictionID",
                table: "IncomeItems",
                newName: "IX_IncomeItems_FederalJurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_Brackets_TypeId",
                table: "TaxBrackets",
                newName: "IX_TaxBrackets_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Brackets_JurisdictionID",
                table: "TaxBrackets",
                newName: "IX_TaxBrackets_JurisdictionID");

            migrationBuilder.AddColumn<string>(
                name: "Pin",
                table: "UserAuths",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxUserInfos",
                table: "TaxUserInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxJurisdictions",
                table: "TaxJurisdictions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeItems",
                table: "IncomeItems",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxBrackets",
                table: "TaxBrackets",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "CheckbookItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachedTransacitonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Variant = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckbookItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckbookItems_Transactions_AttachedTransacitonId",
                        column: x => x.AttachedTransacitonId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckbookItems_AttachedTransacitonId",
                table: "CheckbookItems",
                column: "AttachedTransacitonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appartments_IncomeItems_IncomeItemID",
                table: "Appartments",
                column: "IncomeItemID",
                principalTable: "IncomeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_IncomeItems_IncomeItemID",
                table: "Assets",
                column: "IncomeItemID",
                principalTable: "IncomeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_IncomeItems_IncomeItemID",
                table: "Businesses",
                column: "IncomeItemID",
                principalTable: "IncomeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_IncomeItems_IncomeItemID",
                table: "Hotels",
                column: "IncomeItemID",
                principalTable: "IncomeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItems_TaxJurisdictions_FederalJurisdictionID",
                table: "IncomeItems",
                column: "FederalJurisdictionID",
                principalTable: "TaxJurisdictions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItems_TaxJurisdictions_LocalJurisdictionID",
                table: "IncomeItems",
                column: "LocalJurisdictionID",
                principalTable: "TaxJurisdictions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItems_TaxUserInfos_TaxUserInfoId",
                table: "IncomeItems",
                column: "TaxUserInfoId",
                principalTable: "TaxUserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItems_Users_UserId",
                table: "IncomeItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxBrackets_TaxJurisdictions_JurisdictionID",
                table: "TaxBrackets",
                column: "JurisdictionID",
                principalTable: "TaxJurisdictions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxBrackets_UserTypes_TypeId",
                table: "TaxBrackets",
                column: "TypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxJurisdictions_BankAccounts_AccountId",
                table: "TaxJurisdictions",
                column: "AccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxUserInfos_Users_UserId",
                table: "TaxUserInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appartments_IncomeItems_IncomeItemID",
                table: "Appartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_IncomeItems_IncomeItemID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_IncomeItems_IncomeItemID",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_IncomeItems_IncomeItemID",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItems_TaxJurisdictions_FederalJurisdictionID",
                table: "IncomeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItems_TaxJurisdictions_LocalJurisdictionID",
                table: "IncomeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItems_TaxUserInfos_TaxUserInfoId",
                table: "IncomeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItems_Users_UserId",
                table: "IncomeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxBrackets_TaxJurisdictions_JurisdictionID",
                table: "TaxBrackets");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxBrackets_UserTypes_TypeId",
                table: "TaxBrackets");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxJurisdictions_BankAccounts_AccountId",
                table: "TaxJurisdictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxUserInfos_Users_UserId",
                table: "TaxUserInfos");

            migrationBuilder.DropTable(
                name: "CheckbookItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxUserInfos",
                table: "TaxUserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxJurisdictions",
                table: "TaxJurisdictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxBrackets",
                table: "TaxBrackets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeItems",
                table: "IncomeItems");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "UserAuths");

            migrationBuilder.RenameTable(
                name: "TaxUserInfos",
                newName: "UserInfos");

            migrationBuilder.RenameTable(
                name: "TaxJurisdictions",
                newName: "Jurisdictions");

            migrationBuilder.RenameTable(
                name: "TaxBrackets",
                newName: "Brackets");

            migrationBuilder.RenameTable(
                name: "IncomeItems",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_TaxUserInfos_UserId",
                table: "UserInfos",
                newName: "IX_UserInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxJurisdictions_AccountId",
                table: "Jurisdictions",
                newName: "IX_Jurisdictions_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxBrackets_TypeId",
                table: "Brackets",
                newName: "IX_Brackets_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxBrackets_JurisdictionID",
                table: "Brackets",
                newName: "IX_Brackets_JurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_UserId",
                table: "Items",
                newName: "IX_Items_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_TaxUserInfoId",
                table: "Items",
                newName: "IX_Items_TaxUserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_LocalJurisdictionID",
                table: "Items",
                newName: "IX_Items_LocalJurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_FederalJurisdictionID",
                table: "Items",
                newName: "IX_Items_FederalJurisdictionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jurisdictions",
                table: "Jurisdictions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brackets",
                table: "Brackets",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appartments_Items_IncomeItemID",
                table: "Appartments",
                column: "IncomeItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Items_IncomeItemID",
                table: "Assets",
                column: "IncomeItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brackets_Jurisdictions_JurisdictionID",
                table: "Brackets",
                column: "JurisdictionID",
                principalTable: "Jurisdictions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brackets_UserTypes_TypeId",
                table: "Brackets",
                column: "TypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Items_IncomeItemID",
                table: "Businesses",
                column: "IncomeItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Items_IncomeItemID",
                table: "Hotels",
                column: "IncomeItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Jurisdictions_FederalJurisdictionID",
                table: "Items",
                column: "FederalJurisdictionID",
                principalTable: "Jurisdictions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Jurisdictions_LocalJurisdictionID",
                table: "Items",
                column: "LocalJurisdictionID",
                principalTable: "Jurisdictions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UserInfos_TaxUserInfoId",
                table: "Items",
                column: "TaxUserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jurisdictions_BankAccounts_AccountId",
                table: "Jurisdictions",
                column: "AccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
