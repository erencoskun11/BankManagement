using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp.BankManagement.Migrations
{
    /// <inheritdoc />
    public partial class Add_Outbox_Inbox_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                schema: "bank_management",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 588, DateTimeKind.Utc).AddTicks(1465),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(9118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 587, DateTimeKind.Utc).AddTicks(7935),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(6302));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                schema: "bank_management",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(9118),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 588, DateTimeKind.Utc).AddTicks(1465));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(6302),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 587, DateTimeKind.Utc).AddTicks(7935));
        }
    }
}
