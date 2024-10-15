using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuisnessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class removecustomerFromSalesInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
         
           

            

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "TbSalesInvoices");

            migrationBuilder.AlterColumn<int>(
                name: "Qty",
                table: "TbSalesInvoiceItems",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 1.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "TbSalesInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Qty",
                table: "TbSalesInvoiceItems",
                type: "float",
                nullable: false,
                defaultValue: 1.0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.CreateTable(
                name: "TbIndexViewSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false),
                    Productpara = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title1SectionTSpace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleInner1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbIndexViewSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    ContactNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    ContantEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    FacebookLink = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    InstgramLink = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    LastPanner = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    Logo = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    MiddlePanner = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    TwitterLink = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    WebsiteDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    WebsiteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YoutubeLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbSlider",
                columns: table => new
                {
                    SliderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentState = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSlider", x => x.SliderId);
                });

            migrationBuilder.CreateTable(
                name: "TbViewNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbViewNames", x => x.Id);
                });
        }
    }
}
