using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdentityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3d2384f4-587c-4fb6-9d8e-85f7a426ae4f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ca9ac7e7-2de0-4553-b15c-be5902729eed");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16484989-e856-402c-bd84-c4afeac581fb", null, "User", "USER" },
                    { "39c1a258-ee06-4cd1-936d-a148d5a0eead", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "16484989-e856-402c-bd84-c4afeac581fb");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "39c1a258-ee06-4cd1-936d-a148d5a0eead");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d2384f4-587c-4fb6-9d8e-85f7a426ae4f", null, "Administrator", "ADMINISTRATOR" },
                    { "ca9ac7e7-2de0-4553-b15c-be5902729eed", null, "User", "USER" }
                });
        }
    }
}
