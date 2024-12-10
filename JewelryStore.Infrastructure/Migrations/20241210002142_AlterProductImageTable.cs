using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterProductImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId_Order",
                table: "ProductImages");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId_Order",
                table: "ProductImages",
                columns: new[] { "ProductId", "Order" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId_Order",
                table: "ProductImages");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId_Order",
                table: "ProductImages",
                columns: new[] { "ProductId", "Order" },
                unique: true);
        }
    }
}
