using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class editproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc932bc7-8c15-46e1-8047-ae3bcef24ddd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9a1e7ff-9ae1-4cd5-a366-6ea22872c0f9");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5fd72153-bf77-4a24-b26f-d1a56649880a", "dc3b8184-e40f-450b-96e5-c10898d2efa5", "Admin", "ADMIN" },
                    { "de33f954-210e-4339-adce-2db7e861599d", "ddbfa06c-21b1-4dd0-b3fd-eb6d40f7ff0c", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fd72153-bf77-4a24-b26f-d1a56649880a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de33f954-210e-4339-adce-2db7e861599d");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bc932bc7-8c15-46e1-8047-ae3bcef24ddd", "f967b917-b94c-4759-bcee-5412dc8a8058", "User", "USER" },
                    { "e9a1e7ff-9ae1-4cd5-a366-6ea22872c0f9", "d1e89e9a-99f9-4a53-aa03-ff3b04fb4a77", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reservations_ReservationId",
                table: "Users",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
