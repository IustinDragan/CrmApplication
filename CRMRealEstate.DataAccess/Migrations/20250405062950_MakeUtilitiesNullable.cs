using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMRealEstate.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeUtilitiesNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Utilities",
                table: "Properties",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ApartamentFloor",
                table: "Properties",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FloorsTotalNumber",
                table: "Properties",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartamentFloor",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FloorsTotalNumber",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "Utilities",
                table: "Properties",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
