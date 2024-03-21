using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P335_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class seed_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContactInfo",
                columns: new[] { "Id", "Email", "PhoneNumber" },
                values: new object[] { 1, "fruit@mail.com", "+9941234567" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactInfo",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
