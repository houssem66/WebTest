using Microsoft.EntityFrameworkCore.Migrations;

namespace TourMe.Data.Migrations
{
    public partial class commercant2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommercant",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NumMat",
                table: "AspNetUsers",
                newName: "Secteur");

            migrationBuilder.AddColumn<int>(
                name: "DomainActivite",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EffectFemme",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EffectHomme",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormeJuridique",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomGerant",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patente",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SituationEntreprise",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DomainActivite",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EffectFemme",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EffectHomme",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FormeJuridique",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NomGerant",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Patente",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SituationEntreprise",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Secteur",
                table: "AspNetUsers",
                newName: "NumMat");

            migrationBuilder.AddColumn<bool>(
                name: "IsCommercant",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
