using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurboProject.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class NullablePropForToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
       name: "RevokedByIp",
       table: "RefreshTokens",
       type: "nvarchar(max)",
       nullable: true,
       oldClrType: typeof(string),
       oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
        name: "RevokedByIp",
        table: "RefreshTokens",
        type: "nvarchar(max)",
        nullable: false,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
