using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2sem_SpejderProjekt.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteRegistrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Activities_ActivityId",
                table: "Registrations");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Activities_ActivityId",
                table: "Registrations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Activities_ActivityId",
                table: "Registrations");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Activities_ActivityId",
                table: "Registrations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
