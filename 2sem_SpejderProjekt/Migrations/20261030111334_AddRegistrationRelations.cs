using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2sem_SpejderProjekt.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistrationRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ActivityId",
                table: "Registrations",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ScoutId_ActivityId",
                table: "Registrations",
                columns: new[] { "ScoutId", "ActivityId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Activities_ActivityId",
                table: "Registrations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Scouts_ScoutId",
                table: "Registrations",
                column: "ScoutId",
                principalTable: "Scouts",
                principalColumn: "ScoutId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Activities_ActivityId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Scouts_ScoutId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_ActivityId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_ScoutId_ActivityId",
                table: "Registrations");
        }
    }
}
