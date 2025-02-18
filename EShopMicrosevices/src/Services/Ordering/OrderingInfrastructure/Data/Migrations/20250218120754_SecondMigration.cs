using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingInfrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillingAdress_ZipCode",
                table: "Orders",
                newName: "BillingAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "BillingAdress_State",
                table: "Orders",
                newName: "BillingAddress_State");

            migrationBuilder.RenameColumn(
                name: "BillingAdress_LastName",
                table: "Orders",
                newName: "BillingAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "BillingAdress_FirstName",
                table: "Orders",
                newName: "BillingAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "BillingAdress_EmailAddress",
                table: "Orders",
                newName: "BillingAddress_EmailAddress");

            migrationBuilder.RenameColumn(
                name: "BillingAdress_Country",
                table: "Orders",
                newName: "BillingAddress_Country");

            migrationBuilder.RenameColumn(
                name: "BillingAdress_AddressLine",
                table: "Orders",
                newName: "BillingAddress_AddressLine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillingAddress_ZipCode",
                table: "Orders",
                newName: "BillingAdress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_State",
                table: "Orders",
                newName: "BillingAdress_State");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_LastName",
                table: "Orders",
                newName: "BillingAdress_LastName");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_FirstName",
                table: "Orders",
                newName: "BillingAdress_FirstName");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_EmailAddress",
                table: "Orders",
                newName: "BillingAdress_EmailAddress");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_Country",
                table: "Orders",
                newName: "BillingAdress_Country");

            migrationBuilder.RenameColumn(
                name: "BillingAddress_AddressLine",
                table: "Orders",
                newName: "BillingAdress_AddressLine");
        }
    }
}
