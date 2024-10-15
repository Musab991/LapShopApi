using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuisnessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class unusedTable23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("TbPurchaseInvoiceItems");
            migrationBuilder.DropTable("TbPurchaseInvoices");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
