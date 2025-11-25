using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeFinances.Migrations
{
    /// <inheritdoc />
    public partial class seed_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b96445b7-beaf-4a8a-bdbc-075bdecae61b", null, "Adult", "ADULT" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b96445b7-beaf-4a8a-bdbc-075bdecae61b");
        }
    }
}
