using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "ProdDescription", "ProdImage", "ProdName", "ProdPrice" },
                values: new object[,]
                {
                    { 1, "Fin driver av högsta kvalité", "driver.jpg", "Golfklubba Driver", 2599m },
                    { 2, "Järnklubba i världsklass", "jarnklubba.jpg", "Golfklubba Järn", 1999m },
                    { 3, "Perfekt balans", "putter.jpg", "Golfklubba Putter", 1699m },
                    { 4, "Vattentålig", "golfbag.jpg", "Golfbag", 1999m },
                    { 5, "Bra grepp", "handske.jpg", "Golfhandske", 299m },
                    { 6, "Högkvalitativa bollar.", "golfballs.jpg", "Golfbollar (12-pack)", 349m },
                    { 7, "Perfekt när solen tittar fram", "keps.jpg", "Golfkeps", 249m },
                    { 8, "pegs i trä", "peg.jpg", "peg (10-pack)", 39m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 8);
        }
    }
}
