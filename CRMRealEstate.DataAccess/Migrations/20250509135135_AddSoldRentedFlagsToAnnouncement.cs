using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMRealEstate.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSoldRentedFlagsToAnnouncement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRent",
                table: "Announcements",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Announcements",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRent",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Announcements");
        }
    }
}
