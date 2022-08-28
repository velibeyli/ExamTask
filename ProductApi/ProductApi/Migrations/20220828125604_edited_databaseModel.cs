using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    public partial class edited_databaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_product_categoryId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_product_categoryId",
                table: "Products",
                column: "product_categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_product_categoryId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_product_categoryId",
                table: "Products",
                column: "product_categoryId",
                unique: true);
        }
    }
}
