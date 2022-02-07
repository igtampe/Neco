using Microsoft.EntityFrameworkCore.Migrations;

namespace Igtampe.Neco.Data.Migrations
{
    /// <summary></summary>
    public partial class AddUserOpenableUserType : Migration
    {
        /// <summary></summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UserOpenable",
                table: "UserType",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <summary></summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserOpenable",
                table: "UserType");
        }
    }
}
