using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class addrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae3f4330-f1ba-4973-88b9-dc8dec714e15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e92e1e7a-1432-4908-84de-1ff331dce60d");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "811cc63d-7552-4746-bad0-56998247f31c", "20225d57-202a-4bf0-a587-743204fef86d", "User", "USER" },
                    { "f6d5ef20-b526-41d7-a423-ea866c469357", "31bcd87e-10c7-42f4-b6f7-7fe2881d36c0", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoomId",
                table: "Users",
                column: "RoomId",
                unique: true,
                filter: "[RoomId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoomId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "811cc63d-7552-4746-bad0-56998247f31c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d5ef20-b526-41d7-a423-ea866c469357");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Users");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ae3f4330-f1ba-4973-88b9-dc8dec714e15", "91be9c30-f1ad-49e9-9d8a-f38b1bea5f7b", "User", "USER" },
                    { "e92e1e7a-1432-4908-84de-1ff331dce60d", "0d19be3f-82d2-4fc9-81e7-2443e70b8529", "Admin", "ADMIN" }
                });
        }
    }
}
