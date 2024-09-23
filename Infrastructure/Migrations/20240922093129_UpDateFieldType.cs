using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpDateFieldType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "TemplateCanvas");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "CustomCanvas",
                newName: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "TemplateCanvas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ProviderRate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CustomCanvas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "TemplateCanvas");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CustomCanvas");

            migrationBuilder.RenameColumn(
                name: "Images",
                table: "CustomCanvas",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TemplateCanvas",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ProviderRate",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
