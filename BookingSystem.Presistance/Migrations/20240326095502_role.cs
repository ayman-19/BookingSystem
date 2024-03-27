using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3099b5ed-cd86-4289-b19b-8fecf5044cba", "b4c5531b-8097-4fc6-a448-e34432326fbb", "Admin", "ADMIN" },
                    { "85831ec4-2019-42bd-9040-e0569ec1bbba", "ef233e0d-c086-4270-b77f-6090349824dc", "User", "USER" },
                    { "ba2a13a4-3a32-414a-8d20-e0a05058c4ac", "f05794f4-f6ca-45e7-9be9-5683af5213c1", "Doctor", "DOCTOR" },
                    { "d535a51b-575b-4889-90d0-e406b278dbb6", "a895e06e-1df0-4564-b850-fee03a470983", "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3099b5ed-cd86-4289-b19b-8fecf5044cba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85831ec4-2019-42bd-9040-e0569ec1bbba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba2a13a4-3a32-414a-8d20-e0a05058c4ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d535a51b-575b-4889-90d0-e406b278dbb6");
        }
    }
}
