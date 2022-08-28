using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStockApi.Migrations
{
    public partial class edited_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockCount",
                table: "ProductStocks",
                newName: "productStock_stockCount");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductStocks",
                newName: "productStock_name");

            migrationBuilder.RenameColumn(
                name: "NewSoldProductCount",
                table: "ProductStocks",
                newName: "productStock_newSoldProductCount");

            migrationBuilder.RenameColumn(
                name: "NewAddedProductCount",
                table: "ProductStocks",
                newName: "productStock_newAddedProductCount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductStocks",
                newName: "productStock_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productStock_stockCount",
                table: "ProductStocks",
                newName: "StockCount");

            migrationBuilder.RenameColumn(
                name: "productStock_newSoldProductCount",
                table: "ProductStocks",
                newName: "NewSoldProductCount");

            migrationBuilder.RenameColumn(
                name: "productStock_newAddedProductCount",
                table: "ProductStocks",
                newName: "NewAddedProductCount");

            migrationBuilder.RenameColumn(
                name: "productStock_name",
                table: "ProductStocks",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "productStock_id",
                table: "ProductStocks",
                newName: "Id");
        }
    }
}
