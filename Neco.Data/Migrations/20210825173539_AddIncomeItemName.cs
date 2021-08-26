using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    /// <summary>Add income item name migration</summary>
    public partial class AddIncomeItemName : Migration
    {
        /// <summary>Ups the migration</summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "IncomeItems",
                type: "nvarchar(max)",
                nullable: true);
        }
        /// <summary>Downs the migration</summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "IncomeItems");
        }
    }
}
