using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    public partial class AddIncomeItemName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "IncomeItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "IncomeItems");
        }
    }
}
