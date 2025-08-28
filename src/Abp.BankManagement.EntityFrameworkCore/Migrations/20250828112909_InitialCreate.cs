using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp.BankManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                defaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 845, DateTimeKind.Utc).AddTicks(599),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 588, DateTimeKind.Utc).AddTicks(1465));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 844, DateTimeKind.Utc).AddTicks(1552),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 587, DateTimeKind.Utc).AddTicks(7935));
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
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 588, DateTimeKind.Utc).AddTicks(1465),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 845, DateTimeKind.Utc).AddTicks(599));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 37, 587, DateTimeKind.Utc).AddTicks(7935),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 844, DateTimeKind.Utc).AddTicks(1552));
        }
    }
}
