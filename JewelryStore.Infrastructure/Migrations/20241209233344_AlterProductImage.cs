using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId_Order",
                table: "ProductImages",
                columns: new[] { "ProductId", "Order" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductImage_Order",
                table: "ProductImages",
                sql: "[Order] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId_Order",
                table: "ProductImages");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductImage_Order",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ProductImages");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }
    }
}
