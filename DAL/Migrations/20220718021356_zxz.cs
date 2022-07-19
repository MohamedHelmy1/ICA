using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class zxz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextLeather",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextLeather",
                table: "Courses");
        }
    }
}
