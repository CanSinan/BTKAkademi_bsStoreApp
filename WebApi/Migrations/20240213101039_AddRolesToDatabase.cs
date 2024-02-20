using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6133797d-148d-4b90-996e-e3e41eb8dd6f", null, "Editor", "EDITOR" },
                    { "9d922652-9740-4d09-bf8f-2258571f324a", null, "Admin", "ADMIN" },
                    { "d72a3c54-3829-403f-84e5-a49ac7176fa3", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6133797d-148d-4b90-996e-e3e41eb8dd6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d922652-9740-4d09-bf8f-2258571f324a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d72a3c54-3829-403f-84e5-a49ac7176fa3");
        }
    }
}
