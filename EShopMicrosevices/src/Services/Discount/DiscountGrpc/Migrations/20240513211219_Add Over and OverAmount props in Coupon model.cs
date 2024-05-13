using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountGrpc.Migrations
{
    /// <inheritdoc />
    public partial class AddOverandOverAmountpropsinCouponmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Over",
                table: "Coupons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverAmount",
                table: "Coupons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Over", "OverAmount" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Over", "OverAmount" },
                values: new object[] { 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Over",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "OverAmount",
                table: "Coupons");
        }
    }
}
