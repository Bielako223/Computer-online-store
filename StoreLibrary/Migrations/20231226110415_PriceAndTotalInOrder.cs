using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class PriceAndTotalInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ItemPrice",
                table: "OrderItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ItemPrice",
                table: "OrderItems");
        }
    }
}
