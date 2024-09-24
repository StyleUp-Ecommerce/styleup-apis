using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_AuthorId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "TemplateCode",
                table: "TemplateCanvas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Order",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
                table: "Order",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CanvasCode",
                table: "CustomCanvas",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_AuthorId",
                table: "Order",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_AuthorId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TemplateCode",
                table: "TemplateCanvas");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CanvasCode",
                table: "CustomCanvas");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_AuthorId",
                table: "Order",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
