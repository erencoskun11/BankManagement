using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abp.BankManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedEventInboxAndOutboxTables : Migration
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
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(9118),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 26, 13, 5, 20, 557, DateTimeKind.Utc).AddTicks(5234));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(6302),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 26, 13, 5, 20, 556, DateTimeKind.Utc).AddTicks(4498));

            migrationBuilder.CreateTable(
                name: "AbpEventInbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EventData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEventInbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpEventOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EventData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEventOutbox", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEventInbox_MessageId",
                table: "AbpEventInbox",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpEventInbox_Processed_CreationTime",
                table: "AbpEventInbox",
                columns: new[] { "Processed", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEventOutbox_CreationTime",
                table: "AbpEventOutbox",
                column: "CreationTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpEventInbox");

            migrationBuilder.DropTable(
                name: "AbpEventOutbox");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                schema: "bank_management",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 26, 13, 5, 20, 557, DateTimeKind.Utc).AddTicks(5234),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(9118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenedAt",
                schema: "bank_management",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 26, 13, 5, 20, 556, DateTimeKind.Utc).AddTicks(4498),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 31, 7, 13, 20, 462, DateTimeKind.Utc).AddTicks(6302));
        }
    }
}
