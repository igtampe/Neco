using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    /// <summary>Overhaul Migration</summary>
    public partial class Overhaul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appartments_IncomeItems_IncomeItemID",
                table: "Appartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_IncomeItems_IncomeItemID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Plots_PlotID",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_OwnerId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_TypeId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Banks_BankId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_OwnerId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountTypes_Banks_BankId",
                table: "BankAccountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_IncomeItems_IncomeItemID",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_CertifiedItems_Users_CertifiedById",
                table: "CertifiedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckbookItems_Transactions_AttachedTransacitonId",
                table: "CheckbookItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_FromUserId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_TopBidderId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_BankAccounts_FederalBankAccountId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_BankAccounts_DistrictBankAccountId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Countries_CountryID",
                table: "Districts");

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
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Plots_BankAccounts_TiedAccountId",
                table: "Plots");

            migrationBuilder.DropForeignKey(
                name: "FK_Plots_Districts_DistrictID",
                table: "Plots");

            migrationBuilder.DropForeignKey(
                name: "FK_Road_Countries_CountryID",
                table: "Road");

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
                name: "FK_Transactions_BankAccounts_FromAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_ToBankAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_FromUserId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_ToUserId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_TypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "TaxUserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAuths",
                table: "UserAuths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FromUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ToBankAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ToUserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxJurisdictions",
                table: "TaxJurisdictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxBrackets",
                table: "TaxBrackets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plots",
                table: "Plots");

            migrationBuilder.DropIndex(
                name: "IX_Plots_TiedAccountId",
                table: "Plots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeItems",
                table: "IncomeItems");

            migrationBuilder.DropIndex(
                name: "IX_IncomeItems_TaxUserInfoId",
                table: "IncomeItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckbookItems",
                table: "CheckbookItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CertifiedItems",
                table: "CertifiedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Banks",
                table: "Banks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountTypes",
                table: "BankAccountTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appartments",
                table: "Appartments");

            migrationBuilder.DropColumn(
                name: "Executed",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Failed",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Taxable",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToBankAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PricePerSquareMeter",
                table: "Plots");

            migrationBuilder.DropColumn(
                name: "TiedAccountId",
                table: "Plots");

            migrationBuilder.DropColumn(
                name: "TaxUserInfoId",
                table: "IncomeItems");

            migrationBuilder.DropColumn(
                name: "DistrictSalesTax",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "FederalSalesTax",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpForAuction",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "UserTypes",
                newName: "UserType");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UserAuths",
                newName: "UserAuth");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "TaxJurisdictions",
                newName: "TaxJurisdiction");

            migrationBuilder.RenameTable(
                name: "TaxBrackets",
                newName: "TaxBracket");

            migrationBuilder.RenameTable(
                name: "Plots",
                newName: "Plot");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "IncomeItems",
                newName: "IncomeItem");

            migrationBuilder.RenameTable(
                name: "Hotels",
                newName: "Hotel");

            migrationBuilder.RenameTable(
                name: "Districts",
                newName: "District");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameTable(
                name: "CheckbookItems",
                newName: "CheckbookItem");

            migrationBuilder.RenameTable(
                name: "CertifiedItems",
                newName: "CertifiedItem");

            migrationBuilder.RenameTable(
                name: "Businesses",
                newName: "Business");

            migrationBuilder.RenameTable(
                name: "Banks",
                newName: "Bank");

            migrationBuilder.RenameTable(
                name: "BankAccountTypes",
                newName: "BankAccountType");

            migrationBuilder.RenameTable(
                name: "BankAccounts",
                newName: "BankAccount");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "Asset");

            migrationBuilder.RenameTable(
                name: "Appartments",
                newName: "Apartment");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserType",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "User",
                newName: "TypeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TypeId",
                table: "User",
                newName: "IX_User_TypeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserAuth",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "FromAccountId",
                table: "Transaction",
                newName: "FromAccountID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transaction",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_FromAccountId",
                table: "Transaction",
                newName: "IX_Transaction_FromAccountID");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "TaxJurisdiction",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_TaxJurisdictions_AccountId",
                table: "TaxJurisdiction",
                newName: "IX_TaxJurisdiction_AccountID");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "TaxBracket",
                newName: "TypeID");

            migrationBuilder.RenameIndex(
                name: "IX_TaxBrackets_TypeId",
                table: "TaxBracket",
                newName: "IX_TaxBracket_TypeID");

            migrationBuilder.RenameIndex(
                name: "IX_TaxBrackets_JurisdictionID",
                table: "TaxBracket",
                newName: "IX_TaxBracket_JurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_Plots_DistrictID",
                table: "Plot",
                newName: "IX_Plot_DistrictID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notification",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Notification",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notification",
                newName: "IX_Notification_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "IncomeItem",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_UserId",
                table: "IncomeItem",
                newName: "IX_IncomeItem_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_LocalJurisdictionID",
                table: "IncomeItem",
                newName: "IX_IncomeItem_LocalJurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_FederalJurisdictionID",
                table: "IncomeItem",
                newName: "IX_IncomeItem_FederalJurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_IncomeItemID",
                table: "Hotel",
                newName: "IX_Hotel_IncomeItemID");

            migrationBuilder.RenameColumn(
                name: "DistrictBankAccountId",
                table: "District",
                newName: "DistrictBankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_DistrictBankAccountId",
                table: "District",
                newName: "IX_District_DistrictBankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_CountryID",
                table: "District",
                newName: "IX_District_CountryID");

            migrationBuilder.RenameColumn(
                name: "FederalBankAccountId",
                table: "Country",
                newName: "FederalBankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_FederalBankAccountId",
                table: "Country",
                newName: "IX_Country_FederalBankAccountID");

            migrationBuilder.RenameColumn(
                name: "TopBidderId",
                table: "Contract",
                newName: "TopBidderID");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "Contract",
                newName: "FromUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_TopBidderId",
                table: "Contract",
                newName: "IX_Contract_TopBidderID");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_FromUserId",
                table: "Contract",
                newName: "IX_Contract_FromUserID");

            migrationBuilder.RenameColumn(
                name: "AttachedTransacitonId",
                table: "CheckbookItem",
                newName: "AttachedTransacitonID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CheckbookItem",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_CheckbookItems_AttachedTransacitonId",
                table: "CheckbookItem",
                newName: "IX_CheckbookItem_AttachedTransacitonID");

            migrationBuilder.RenameColumn(
                name: "CertifiedById",
                table: "CertifiedItem",
                newName: "CertifiedByID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CertifiedItem",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_CertifiedItems_CertifiedById",
                table: "CertifiedItem",
                newName: "IX_CertifiedItem_CertifiedByID");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_IncomeItemID",
                table: "Business",
                newName: "IX_Business_IncomeItemID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bank",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "BankAccountType",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BankAccountType",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountTypes_BankId",
                table: "BankAccountType",
                newName: "IX_BankAccountType_BankID");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "BankAccount",
                newName: "TypeID");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "BankAccount",
                newName: "OwnerID");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "BankAccount",
                newName: "BankID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BankAccount",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_TypeId",
                table: "BankAccount",
                newName: "IX_BankAccount_TypeID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_OwnerId",
                table: "BankAccount",
                newName: "IX_BankAccount_OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccount",
                newName: "IX_BankAccount_BankID");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Asset",
                newName: "OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_PlotID",
                table: "Asset",
                newName: "IX_Asset_PlotID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_OwnerId",
                table: "Asset",
                newName: "IX_Asset_OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_IncomeItemID",
                table: "Asset",
                newName: "IX_Asset_IncomeItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Appartments_IncomeItemID",
                table: "Apartment",
                newName: "IX_Apartment_IncomeItemID");

            migrationBuilder.AddColumn<int>(
                name: "Taxation",
                table: "UserType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FromAccountID",
                table: "Transaction",
                type: "nvarchar(9)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ToAccountID",
                table: "Transaction",
                type: "nvarchar(9)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountID",
                table: "TaxJurisdiction",
                type: "nvarchar(9)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Plot",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DistrictBankAccountID",
                table: "District",
                type: "nvarchar(9)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FederalBankAccountID",
                table: "Country",
                type: "nvarchar(9)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ID",
                table: "BankAccount",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "Closed",
                table: "BankAccount",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DetailsID",
                table: "BankAccount",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserType",
                table: "UserType",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAuth",
                table: "UserAuth",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxJurisdiction",
                table: "TaxJurisdiction",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxBracket",
                table: "TaxBracket",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plot",
                table: "Plot",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeItem",
                table: "IncomeItem",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckbookItem",
                table: "CheckbookItem",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CertifiedItem",
                table: "CertifiedItem",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Business",
                table: "Business",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bank",
                table: "Bank",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccountType",
                table: "BankAccountType",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset",
                table: "Asset",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment",
                column: "ID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ToAccountID",
                table: "Transaction",
                column: "ToAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Plot_OwnerID",
                table: "Plot",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_DetailsID",
                table: "BankAccount",
                column: "DetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxReport_OwnerID",
                table: "TaxReport",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_IncomeItem_IncomeItemID",
                table: "Apartment",
                column: "IncomeItemID",
                principalTable: "IncomeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_IncomeItem_IncomeItemID",
                table: "Asset",
                column: "IncomeItemID",
                principalTable: "IncomeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Plot_PlotID",
                table: "Asset",
                column: "PlotID",
                principalTable: "Plot",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_User_OwnerID",
                table: "Asset",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Bank_BankID",
                table: "BankAccount",
                column: "BankID",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankAccountDetail_DetailsID",
                table: "BankAccount",
                column: "DetailsID",
                principalTable: "BankAccountDetail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankAccountType_TypeID",
                table: "BankAccount",
                column: "TypeID",
                principalTable: "BankAccountType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_User_OwnerID",
                table: "BankAccount",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountType_Bank_BankID",
                table: "BankAccountType",
                column: "BankID",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Business_IncomeItem_IncomeItemID",
                table: "Business",
                column: "IncomeItemID",
                principalTable: "IncomeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CertifiedItem_User_CertifiedByID",
                table: "CertifiedItem",
                column: "CertifiedByID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckbookItem_Transaction_AttachedTransacitonID",
                table: "CheckbookItem",
                column: "AttachedTransacitonID",
                principalTable: "Transaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_User_FromUserID",
                table: "Contract",
                column: "FromUserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_User_TopBidderID",
                table: "Contract",
                column: "TopBidderID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Country_BankAccount_FederalBankAccountID",
                table: "Country",
                column: "FederalBankAccountID",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_District_BankAccount_DistrictBankAccountID",
                table: "District",
                column: "DistrictBankAccountID",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_District_Country_CountryID",
                table: "District",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_IncomeItem_IncomeItemID",
                table: "Hotel",
                column: "IncomeItemID",
                principalTable: "IncomeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItem_TaxJurisdiction_FederalJurisdictionID",
                table: "IncomeItem",
                column: "FederalJurisdictionID",
                principalTable: "TaxJurisdiction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItem_TaxJurisdiction_LocalJurisdictionID",
                table: "IncomeItem",
                column: "LocalJurisdictionID",
                principalTable: "TaxJurisdiction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItem_User_UserID",
                table: "IncomeItem",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserID",
                table: "Notification",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plot_District_DistrictID",
                table: "Plot",
                column: "DistrictID",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plot_User_OwnerID",
                table: "Plot",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Road_Country_CountryID",
                table: "Road",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxBracket_TaxJurisdiction_JurisdictionID",
                table: "TaxBracket",
                column: "JurisdictionID",
                principalTable: "TaxJurisdiction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxBracket_UserType_TypeID",
                table: "TaxBracket",
                column: "TypeID",
                principalTable: "UserType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxJurisdiction_BankAccount_AccountID",
                table: "TaxJurisdiction",
                column: "AccountID",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BankAccount_FromAccountID",
                table: "Transaction",
                column: "FromAccountID",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_BankAccount_ToAccountID",
                table: "Transaction",
                column: "ToAccountID",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserType_TypeID",
                table: "User",
                column: "TypeID",
                principalTable: "UserType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_IncomeItem_IncomeItemID",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_IncomeItem_IncomeItemID",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Plot_PlotID",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_User_OwnerID",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank_BankID",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankAccountDetail_DetailsID",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankAccountType_TypeID",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_User_OwnerID",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountType_Bank_BankID",
                table: "BankAccountType");

            migrationBuilder.DropForeignKey(
                name: "FK_Business_IncomeItem_IncomeItemID",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_CertifiedItem_User_CertifiedByID",
                table: "CertifiedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckbookItem_Transaction_AttachedTransacitonID",
                table: "CheckbookItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_User_FromUserID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_User_TopBidderID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_BankAccount_FederalBankAccountID",
                table: "Country");

            migrationBuilder.DropForeignKey(
                name: "FK_District_BankAccount_DistrictBankAccountID",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_District_Country_CountryID",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_IncomeItem_IncomeItemID",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItem_TaxJurisdiction_FederalJurisdictionID",
                table: "IncomeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItem_TaxJurisdiction_LocalJurisdictionID",
                table: "IncomeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItem_User_UserID",
                table: "IncomeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_UserID",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Plot_District_DistrictID",
                table: "Plot");

            migrationBuilder.DropForeignKey(
                name: "FK_Plot_User_OwnerID",
                table: "Plot");

            migrationBuilder.DropForeignKey(
                name: "FK_Road_Country_CountryID",
                table: "Road");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxBracket_TaxJurisdiction_JurisdictionID",
                table: "TaxBracket");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxBracket_UserType_TypeID",
                table: "TaxBracket");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxJurisdiction_BankAccount_AccountID",
                table: "TaxJurisdiction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BankAccount_FromAccountID",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_BankAccount_ToAccountID",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserType_TypeID",
                table: "User");

            migrationBuilder.DropTable(
                name: "BankAccountDetail");

            migrationBuilder.DropTable(
                name: "TaxReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserType",
                table: "UserType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAuth",
                table: "UserAuth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ToAccountID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxJurisdiction",
                table: "TaxJurisdiction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxBracket",
                table: "TaxBracket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plot",
                table: "Plot");

            migrationBuilder.DropIndex(
                name: "IX_Plot_OwnerID",
                table: "Plot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeItem",
                table: "IncomeItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckbookItem",
                table: "CheckbookItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CertifiedItem",
                table: "CertifiedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Business",
                table: "Business");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountType",
                table: "BankAccountType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_DetailsID",
                table: "BankAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bank",
                table: "Bank");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset",
                table: "Asset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment");

            migrationBuilder.DropColumn(
                name: "Taxation",
                table: "UserType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ToAccountID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Plot");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Closed",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "DetailsID",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "UserType",
                newName: "UserTypes");

            migrationBuilder.RenameTable(
                name: "UserAuth",
                newName: "UserAuths");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "TaxJurisdiction",
                newName: "TaxJurisdictions");

            migrationBuilder.RenameTable(
                name: "TaxBracket",
                newName: "TaxBrackets");

            migrationBuilder.RenameTable(
                name: "Plot",
                newName: "Plots");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "IncomeItem",
                newName: "IncomeItems");

            migrationBuilder.RenameTable(
                name: "Hotel",
                newName: "Hotels");

            migrationBuilder.RenameTable(
                name: "District",
                newName: "Districts");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameTable(
                name: "CheckbookItem",
                newName: "CheckbookItems");

            migrationBuilder.RenameTable(
                name: "CertifiedItem",
                newName: "CertifiedItems");

            migrationBuilder.RenameTable(
                name: "Business",
                newName: "Businesses");

            migrationBuilder.RenameTable(
                name: "BankAccountType",
                newName: "BankAccountTypes");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "Bank",
                newName: "Banks");

            migrationBuilder.RenameTable(
                name: "Asset",
                newName: "Assets");

            migrationBuilder.RenameTable(
                name: "Apartment",
                newName: "Appartments");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserAuths",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "Users",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_User_TypeID",
                table: "Users",
                newName: "IX_Users_TypeId");

            migrationBuilder.RenameColumn(
                name: "FromAccountID",
                table: "Transactions",
                newName: "FromAccountId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_FromAccountID",
                table: "Transactions",
                newName: "IX_Transactions_FromAccountId");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "TaxJurisdictions",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxJurisdiction_AccountID",
                table: "TaxJurisdictions",
                newName: "IX_TaxJurisdictions_AccountId");

            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "TaxBrackets",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxBracket_TypeID",
                table: "TaxBrackets",
                newName: "IX_TaxBrackets_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxBracket_JurisdictionID",
                table: "TaxBrackets",
                newName: "IX_TaxBrackets_JurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_Plot_DistrictID",
                table: "Plots",
                newName: "IX_Plots_DistrictID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Notifications",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_UserID",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "IncomeItems",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItem_UserID",
                table: "IncomeItems",
                newName: "IX_IncomeItems_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItem_LocalJurisdictionID",
                table: "IncomeItems",
                newName: "IX_IncomeItems_LocalJurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItem_FederalJurisdictionID",
                table: "IncomeItems",
                newName: "IX_IncomeItems_FederalJurisdictionID");

            migrationBuilder.RenameIndex(
                name: "IX_Hotel_IncomeItemID",
                table: "Hotels",
                newName: "IX_Hotels_IncomeItemID");

            migrationBuilder.RenameColumn(
                name: "DistrictBankAccountID",
                table: "Districts",
                newName: "DistrictBankAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_District_DistrictBankAccountID",
                table: "Districts",
                newName: "IX_Districts_DistrictBankAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_District_CountryID",
                table: "Districts",
                newName: "IX_Districts_CountryID");

            migrationBuilder.RenameColumn(
                name: "FederalBankAccountID",
                table: "Countries",
                newName: "FederalBankAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Country_FederalBankAccountID",
                table: "Countries",
                newName: "IX_Countries_FederalBankAccountId");

            migrationBuilder.RenameColumn(
                name: "TopBidderID",
                table: "Contracts",
                newName: "TopBidderId");

            migrationBuilder.RenameColumn(
                name: "FromUserID",
                table: "Contracts",
                newName: "FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_TopBidderID",
                table: "Contracts",
                newName: "IX_Contracts_TopBidderId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_FromUserID",
                table: "Contracts",
                newName: "IX_Contracts_FromUserId");

            migrationBuilder.RenameColumn(
                name: "AttachedTransacitonID",
                table: "CheckbookItems",
                newName: "AttachedTransacitonId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CheckbookItems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CheckbookItem_AttachedTransacitonID",
                table: "CheckbookItems",
                newName: "IX_CheckbookItems_AttachedTransacitonId");

            migrationBuilder.RenameColumn(
                name: "CertifiedByID",
                table: "CertifiedItems",
                newName: "CertifiedById");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CertifiedItems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CertifiedItem_CertifiedByID",
                table: "CertifiedItems",
                newName: "IX_CertifiedItems_CertifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Business_IncomeItemID",
                table: "Businesses",
                newName: "IX_Businesses_IncomeItemID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                table: "BankAccountTypes",
                newName: "BankId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BankAccountTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountType_BankID",
                table: "BankAccountTypes",
                newName: "IX_BankAccountTypes_BankId");

            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "BankAccounts",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "BankAccounts",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "BankID",
                table: "BankAccounts",
                newName: "BankId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BankAccounts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_TypeID",
                table: "BankAccounts",
                newName: "IX_BankAccounts_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_OwnerID",
                table: "BankAccounts",
                newName: "IX_BankAccounts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_BankID",
                table: "BankAccounts",
                newName: "IX_BankAccounts_BankId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Banks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Assets",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_PlotID",
                table: "Assets",
                newName: "IX_Assets_PlotID");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_OwnerID",
                table: "Assets",
                newName: "IX_Assets_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_IncomeItemID",
                table: "Assets",
                newName: "IX_Assets_IncomeItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_IncomeItemID",
                table: "Appartments",
                newName: "IX_Appartments_IncomeItemID");

            migrationBuilder.AlterColumn<Guid>(
                name: "FromAccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Executed",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Failed",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FromUserId",
                table: "Transactions",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Taxable",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ToBankAccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUserId",
                table: "Transactions",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "TaxJurisdictions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PricePerSquareMeter",
                table: "Plots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TiedAccountId",
                table: "Plots",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaxUserInfoId",
                table: "IncomeItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DistrictBankAccountId",
                table: "Districts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DistrictSalesTax",
                table: "Districts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<Guid>(
                name: "FederalBankAccountId",
                table: "Countries",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FederalSalesTax",
                table: "Countries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UpForAuction",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BankAccounts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AddColumn<long>(
                name: "Balance",
                table: "BankAccounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAuths",
                table: "UserAuths",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxJurisdictions",
                table: "TaxJurisdictions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxBrackets",
                table: "TaxBrackets",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plots",
                table: "Plots",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeItems",
                table: "IncomeItems",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckbookItems",
                table: "CheckbookItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CertifiedItems",
                table: "CertifiedItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccountTypes",
                table: "BankAccountTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banks",
                table: "Banks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appartments",
                table: "Appartments",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "TaxUserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxUserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxUserInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromUserId",
                table: "Transactions",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToBankAccountId",
                table: "Transactions",
                column: "ToBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToUserId",
                table: "Transactions",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_TiedAccountId",
                table: "Plots",
                column: "TiedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItems_TaxUserInfoId",
                table: "IncomeItems",
                column: "TaxUserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxUserInfos_UserId",
                table: "TaxUserInfos",
                column: "UserId");

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
                name: "FK_Assets_Plots_PlotID",
                table: "Assets",
                column: "PlotID",
                principalTable: "Plots",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_OwnerId",
                table: "Assets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_TypeId",
                table: "BankAccounts",
                column: "TypeId",
                principalTable: "BankAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Banks_BankId",
                table: "BankAccounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_OwnerId",
                table: "BankAccounts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountTypes_Banks_BankId",
                table: "BankAccountTypes",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_IncomeItems_IncomeItemID",
                table: "Businesses",
                column: "IncomeItemID",
                principalTable: "IncomeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CertifiedItems_Users_CertifiedById",
                table: "CertifiedItems",
                column: "CertifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckbookItems_Transactions_AttachedTransacitonId",
                table: "CheckbookItems",
                column: "AttachedTransacitonId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_FromUserId",
                table: "Contracts",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_TopBidderId",
                table: "Contracts",
                column: "TopBidderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_BankAccounts_FederalBankAccountId",
                table: "Countries",
                column: "FederalBankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_BankAccounts_DistrictBankAccountId",
                table: "Districts",
                column: "DistrictBankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Countries_CountryID",
                table: "Districts",
                column: "CountryID",
                principalTable: "Countries",
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
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_BankAccounts_TiedAccountId",
                table: "Plots",
                column: "TiedAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plots_Districts_DistrictID",
                table: "Plots",
                column: "DistrictID",
                principalTable: "Districts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Road_Countries_CountryID",
                table: "Road",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
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
                name: "FK_Transactions_BankAccounts_FromAccountId",
                table: "Transactions",
                column: "FromAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_ToBankAccountId",
                table: "Transactions",
                column: "ToBankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_FromUserId",
                table: "Transactions",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_ToUserId",
                table: "Transactions",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_TypeId",
                table: "Users",
                column: "TypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
