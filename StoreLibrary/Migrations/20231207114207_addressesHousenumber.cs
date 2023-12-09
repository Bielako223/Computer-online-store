using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addressesHousenumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "AddressesForOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "AddressesForOrders");
        }
    }
}
