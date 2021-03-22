using Microsoft.EntityFrameworkCore.Migrations;

namespace TourMe.Data.Migrations
{
    public partial class sk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOrgan",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOrgan",
                table: "AspNetUsers");
        }
    }
}
