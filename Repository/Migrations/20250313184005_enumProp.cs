using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class enumProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisabiltyType",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "DisabilityType",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisabilityType",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "DisabiltyType",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
