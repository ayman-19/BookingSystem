using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class addproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RomeId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RomeId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ab03b21-549f-4a98-8315-c2764b17a1fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81358ff8-e0a1-4f40-8a3c-13dc4a9dc876");

            migrationBuilder.DropColumn(
                name: "RomeId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bc932bc7-8c15-46e1-8047-ae3bcef24ddd", "f967b917-b94c-4759-bcee-5412dc8a8058", "User", "USER" },
                    { "e9a1e7ff-9ae1-4cd5-a366-6ea22872c0f9", "d1e89e9a-99f9-4a53-aa03-ff3b04fb4a77", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReservationId",
                table: "Users",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ReservationId",
                table: "Rooms",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Reservations_ReservationId",
                table: "Rooms",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Reservations_ReservationId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ReservationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ReservationId",
                table: "Rooms");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc932bc7-8c15-46e1-8047-ae3bcef24ddd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9a1e7ff-9ae1-4cd5-a366-6ea22872c0f9");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "RomeId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ab03b21-549f-4a98-8315-c2764b17a1fb", "e2cd0adb-a7c9-4dc8-a365-0e7d47da23da", "Admin", "ADMIN" },
                    { "81358ff8-e0a1-4f40-8a3c-13dc4a9dc876", "637869b4-45a7-4950-b87b-5b9f2ca601eb", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RomeId",
                table: "Reservations",
                column: "RomeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RomeId",
                table: "Reservations",
                column: "RomeId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
