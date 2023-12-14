using Microsoft.EntityFrameworkCore.Migrations;

namespace OusMarieWedding.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PlusOnes_PlusOneId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PlusOneId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PlusOneId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "PlusOneId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlusOneId",
                table: "Reservations",
                column: "PlusOneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PlusOnes_PlusOneId",
                table: "Reservations",
                column: "PlusOneId",
                principalTable: "PlusOnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
