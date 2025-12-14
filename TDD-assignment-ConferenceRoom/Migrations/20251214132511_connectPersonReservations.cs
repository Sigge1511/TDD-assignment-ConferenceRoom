using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDD_assignment_ConferenceRoom.Migrations
{
    /// <inheritdoc />
    public partial class connectPersonReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "ReservationSet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSet_PersonId",
                table: "ReservationSet",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSet_PersonSet_PersonId",
                table: "ReservationSet",
                column: "PersonId",
                principalTable: "PersonSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSet_PersonSet_PersonId",
                table: "ReservationSet");

            migrationBuilder.DropIndex(
                name: "IX_ReservationSet_PersonId",
                table: "ReservationSet");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ReservationSet");
        }
    }
}
