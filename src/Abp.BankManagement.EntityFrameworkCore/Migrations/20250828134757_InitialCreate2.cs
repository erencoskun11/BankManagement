using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp.BankManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
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
                defaultValue: new DateTime(2025, 8, 28, 13, 47, 55, 890, DateTimeKind.Utc).AddTicks(5059),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 845, DateTimeKind.Utc).AddTicks(599));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 8, 28, 13, 47, 55, 890, DateTimeKind.Utc).AddTicks(874),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 844, DateTimeKind.Utc).AddTicks(1552));
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
                defaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 845, DateTimeKind.Utc).AddTicks(599),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 8, 28, 13, 47, 55, 890, DateTimeKind.Utc).AddTicks(5059));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 8, 28, 11, 29, 6, 844, DateTimeKind.Utc).AddTicks(1552),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 8, 28, 13, 47, 55, 890, DateTimeKind.Utc).AddTicks(874));
        }
    }
}
