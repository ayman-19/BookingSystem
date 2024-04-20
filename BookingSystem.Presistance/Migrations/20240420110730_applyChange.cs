using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingSystem.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class applyChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Floor",
                table: "Floor");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31fd681b-5bfc-4ce6-a82f-5b12809f63bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "798e91a2-6416-49c9-af28-61be04f1d598");

            migrationBuilder.RenameTable(
                name: "Floor",
                newName: "Floors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Floors",
                table: "Floors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => new { x.ProductId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_Foods_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foods_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ae3f4330-f1ba-4973-88b9-dc8dec714e15", "91be9c30-f1ad-49e9-9d8a-f38b1bea5f7b", "User", "USER" },
                    { "e92e1e7a-1432-4908-84de-1ff331dce60d", "0d19be3f-82d2-4fc9-81e7-2443e70b8529", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_RoomId",
                table: "Foods",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Floors_FloorId",
                table: "Rooms",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Floors_FloorId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Floors",
                table: "Floors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae3f4330-f1ba-4973-88b9-dc8dec714e15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e92e1e7a-1432-4908-84de-1ff331dce60d");

            migrationBuilder.RenameTable(
                name: "Floors",
                newName: "Floor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Floor",
                table: "Floor",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31fd681b-5bfc-4ce6-a82f-5b12809f63bc", "fa937799-c677-41d1-a3bb-f60505aa9916", "Admin", "ADMIN" },
                    { "798e91a2-6416-49c9-af28-61be04f1d598", "21f4147f-b12e-4fb1-a0ea-f4e67aac8d9c", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
