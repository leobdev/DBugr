using Microsoft.EntityFrameworkCore.Migrations;

namespace DBugr.Data.Migrations
{
    public partial class _007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Ticket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Comments",
                table: "Ticket",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
