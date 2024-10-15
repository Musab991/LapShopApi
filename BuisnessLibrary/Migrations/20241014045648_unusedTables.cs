using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuisnessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class unusedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint first
            migrationBuilder.DropForeignKey(
                name: "FK_TbSalesInvoices_TbCustomers_CustomerId",
                table: "TbSalesInvoices");

            // Now drop the dependent tables
            migrationBuilder.DropTable("TbBusinessInfo");
            migrationBuilder.DropTable("TbCashTransacion");
            migrationBuilder.DropTable("TbIndexViewSettings");
            migrationBuilder.DropTable("TbItemDiscount");
            migrationBuilder.DropTable("TbSettings");
            migrationBuilder.DropTable("TbSlider");
            migrationBuilder.DropTable("TbViewNames");
            migrationBuilder.DropTable("TbCustomerItems");

            // Finally drop the TbCustomers table
            migrationBuilder.DropTable("TbCustomers");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
