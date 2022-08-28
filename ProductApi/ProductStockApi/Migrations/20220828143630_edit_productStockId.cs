using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStockApi.Migrations
{
    public partial class edit_productStockId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ProductStocks",
                newName: "productStock_productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productStock_productId",
                table: "ProductStocks",
                newName: "productId");
        }
    }
}
