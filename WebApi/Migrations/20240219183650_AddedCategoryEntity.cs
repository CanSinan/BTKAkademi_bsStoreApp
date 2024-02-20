using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d519057-73b4-4a2c-8194-7d3a46029177");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f723ef9-5a08-4494-ab9c-6b107983bc84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "864f2966-85b3-4dfb-9667-a3fee97b02e6");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bb51d99-3121-4d5b-b072-10ee41c4b1b0", null, "Editor", "EDITOR" },
                    { "a67c472a-8462-4b1f-b2cc-c92dba6bf9a1", null, "Admin", "ADMIN" },
                    { "c2408466-fa76-4b19-8382-6b62cd6e4c42", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Network" },
                    { 3, "Database Management Systems" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bb51d99-3121-4d5b-b072-10ee41c4b1b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a67c472a-8462-4b1f-b2cc-c92dba6bf9a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2408466-fa76-4b19-8382-6b62cd6e4c42");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d519057-73b4-4a2c-8194-7d3a46029177", null, "Admin", "ADMIN" },
                    { "4f723ef9-5a08-4494-ab9c-6b107983bc84", null, "User", "USER" },
                    { "864f2966-85b3-4dfb-9667-a3fee97b02e6", null, "Editor", "EDITOR" }
                });
        }
    }
}
