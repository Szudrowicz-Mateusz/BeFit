using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HomeFinances.Migrations
{
    /// <inheritdoc />
    public partial class SeedExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExerciseTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Przysiady ze sztangą" },
                    { 2, "Wyciskanie na ławce płaskiej" },
                    { 3, "Martwy ciąg" },
                    { 4, "Wiosłowanie sztangą w opadzie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExerciseTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExerciseTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExerciseTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExerciseTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
