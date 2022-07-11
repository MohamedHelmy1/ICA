using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class tharid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delete",
                table: "Courses");
        }
    }
}
