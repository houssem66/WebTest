using Microsoft.EntityFrameworkCore.Migrations;

namespace TourMe.Data.Migrations
{
    public partial class ss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaires_Experience_ExperienceId",
                table: "Commentaires");

            migrationBuilder.DropIndex(
                name: "IX_Commentaires_ExperienceId",
                table: "Commentaires");

            migrationBuilder.AlterColumn<string>(
                name: "ExperienceId",
                table: "Experience",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ExperienceId1",
                table: "Commentaires",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_ExperienceId1",
                table: "Commentaires",
                column: "ExperienceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaires_Experience_ExperienceId1",
                table: "Commentaires",
                column: "ExperienceId1",
                principalTable: "Experience",
                principalColumn: "ExperienceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaires_Experience_ExperienceId1",
                table: "Commentaires");

            migrationBuilder.DropIndex(
                name: "IX_Commentaires_ExperienceId1",
                table: "Commentaires");

            migrationBuilder.DropColumn(
                name: "ExperienceId1",
                table: "Commentaires");

            migrationBuilder.AlterColumn<int>(
                name: "ExperienceId",
                table: "Experience",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_ExperienceId",
                table: "Commentaires",
                column: "ExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaires_Experience_ExperienceId",
                table: "Commentaires",
                column: "ExperienceId",
                principalTable: "Experience",
                principalColumn: "ExperienceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
