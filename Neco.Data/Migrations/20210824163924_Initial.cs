using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAuths",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    InterestRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountTypes_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_BankAccountTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BankAccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertifiedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertifiedById = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertifiedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertifiedItems_Users_CertifiedById",
                        column: x => x.CertifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromUserId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    TopBidderId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    UpForAuction = table.Column<bool>(type: "bit", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_TopBidderId",
                        column: x => x.TopBidderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    PricePerSquareMeter = table.Column<int>(type: "int", nullable: false),
                    FederalSalesTax = table.Column<double>(type: "float", nullable: false),
                    FederalBankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Countries_BankAccounts_FederalBankAccountId",
                        column: x => x.FederalBankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jurisdictions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurisdictions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Jurisdictions_BankAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromUserId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    ToUserId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    FromAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToBankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    Executed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_FromAccountId",
                        column: x => x.FromAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_ToBankAccountId",
                        column: x => x.ToBankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerSquareMeter = table.Column<int>(type: "int", nullable: false),
                    DistrictSalesTax = table.Column<double>(type: "float", nullable: false),
                    DistrictBankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Districts_BankAccounts_DistrictBankAccountId",
                        column: x => x.DistrictBankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Districts_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brackets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JurisdictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<long>(type: "bigint", nullable: false),
                    End = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brackets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Brackets_Jurisdictions_JurisdictionID",
                        column: x => x.JurisdictionID,
                        principalTable: "Jurisdictions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brackets_UserTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    MiscIncome = table.Column<long>(type: "bigint", nullable: false),
                    LocalJurisdictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FederalJurisdictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxUserInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Items_Jurisdictions_FederalJurisdictionID",
                        column: x => x.FederalJurisdictionID,
                        principalTable: "Jurisdictions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Jurisdictions_LocalJurisdictionID",
                        column: x => x.LocalJurisdictionID,
                        principalTable: "Jurisdictions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_UserInfos_TaxUserInfoId",
                        column: x => x.TaxUserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plots",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiedAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PricePerSquareMeter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plots_BankAccounts_TiedAccountId",
                        column: x => x.TiedAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plots_Districts_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "Districts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appartments",
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
                    table.PrimaryKey("PK_Appartments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appartments_Items_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
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
                    table.PrimaryKey("PK_Businesses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Businesses_Items_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
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
                    table.PrimaryKey("PK_Hotels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hotels_Items_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificLocaiton = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Complete = table.Column<bool>(type: "bit", nullable: false),
                    IncomeItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlotID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assets_Items_IncomeItemID",
                        column: x => x.IncomeItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Plots_PlotID",
                        column: x => x.PlotID,
                        principalTable: "Plots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appartments_IncomeItemID",
                table: "Appartments",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_IncomeItemID",
                table: "Assets",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OwnerId",
                table: "Assets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_PlotID",
                table: "Assets",
                column: "PlotID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_TypeId",
                table: "BankAccounts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserId",
                table: "BankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTypes_BankId",
                table: "BankAccountTypes",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Brackets_JurisdictionID",
                table: "Brackets",
                column: "JurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Brackets_TypeId",
                table: "Brackets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_IncomeItemID",
                table: "Businesses",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CertifiedItems_CertifiedById",
                table: "CertifiedItems",
                column: "CertifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FromUserId",
                table: "Contracts",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TopBidderId",
                table: "Contracts",
                column: "TopBidderId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_FederalBankAccountId",
                table: "Countries",
                column: "FederalBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CountryID",
                table: "Districts",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_DistrictBankAccountId",
                table: "Districts",
                column: "DistrictBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_IncomeItemID",
                table: "Hotels",
                column: "IncomeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_FederalJurisdictionID",
                table: "Items",
                column: "FederalJurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_LocalJurisdictionID",
                table: "Items",
                column: "LocalJurisdictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TaxUserInfoId",
                table: "Items",
                column: "TaxUserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jurisdictions_AccountId",
                table: "Jurisdictions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_DistrictID",
                table: "Plots",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_TiedAccountId",
                table: "Plots",
                column: "TiedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromAccountId",
                table: "Transactions",
                column: "FromAccountId");

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
                name: "IX_UserInfos_UserId",
                table: "UserInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appartments");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Brackets");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "CertifiedItems");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserAuths");

            migrationBuilder.DropTable(
                name: "Plots");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Jurisdictions");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "BankAccountTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
