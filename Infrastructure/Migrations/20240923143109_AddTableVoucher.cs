using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "CustomCanvas");

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultVoucherId",
                table: "CustomCanvas",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    DiscountType = table.Column<string>(type: "text", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "money", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomCanvas_DefaultVoucherId",
                table: "CustomCanvas",
                column: "DefaultVoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCanvas_Voucher_DefaultVoucherId",
                table: "CustomCanvas",
                column: "DefaultVoucherId",
                principalTable: "Voucher",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomCanvas_Voucher_DefaultVoucherId",
                table: "CustomCanvas");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_CustomCanvas_DefaultVoucherId",
                table: "CustomCanvas");

            migrationBuilder.DropColumn(
                name: "DefaultVoucherId",
                table: "CustomCanvas");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CustomCanvas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
