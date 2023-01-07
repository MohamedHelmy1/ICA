using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FK_MarketerId",
                schema: "security",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FK_MarketerId",
                schema: "security",
                table: "Users",
                column: "FK_MarketerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_FK_MarketerId",
                schema: "security",
                table: "Users",
                column: "FK_MarketerId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_FK_MarketerId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FK_MarketerId",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FK_MarketerId",
                schema: "security",
                table: "Users");
        }
    }
}
