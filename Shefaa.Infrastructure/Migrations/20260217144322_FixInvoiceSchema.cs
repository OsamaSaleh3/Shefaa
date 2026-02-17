using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shefaa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixInvoiceSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "InvoiceItems");

         
            migrationBuilder.Sql("UPDATE Invoices SET TotalAmount = 0 WHERE TotalAmount IS NULL");
            migrationBuilder.Sql("UPDATE Invoices SET PaidAmount = 0 WHERE PaidAmount IS NULL");
            migrationBuilder.Sql("UPDATE Invoices SET Status = 'Unpaid' WHERE Status IS NULL");
            migrationBuilder.Sql("UPDATE InvoiceItems SET Quantity = 1 WHERE Quantity IS NULL");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Invoices",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Unpaid",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValueSql: "'Unpaid'");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "InvoiceItems",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 1);

          
            migrationBuilder.AddColumn<decimal>(
                name: "RemainingAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Quantity] * [UnitPrice]",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(name: "RemainingAmount", table: "Invoices");
            migrationBuilder.DropColumn(name: "TotalPrice", table: "InvoiceItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: true,
                computedColumnSql: "[TotalAmount] - [PaidAmount]",
                stored: true);

            migrationBuilder.AddColumn<decimal>(
               name: "TotalPrice",
               table: "InvoiceItems",
               type: "decimal(18,2)",
               nullable: true,
               computedColumnSql: "[Quantity] * [UnitPrice]",
               stored: true);
        }
    }
}