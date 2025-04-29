using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMRealEstate.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestsTableV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersAnnouncements_Announcements_AnnouncementId1",
                table: "UsersAnnouncements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAnnouncements",
                table: "UsersAnnouncements");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnnouncements_AnnouncementId1",
                table: "UsersAnnouncements");

            migrationBuilder.DropColumn(
                name: "AnnouncementId1",
                table: "UsersAnnouncements");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersAnnouncements",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAnnouncements",
                table: "UsersAnnouncements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnnouncements_UserId",
                table: "UsersAnnouncements",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAnnouncements",
                table: "UsersAnnouncements");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnnouncements_UserId",
                table: "UsersAnnouncements");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersAnnouncements");

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementId1",
                table: "UsersAnnouncements",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAnnouncements",
                table: "UsersAnnouncements",
                columns: new[] { "UserId", "AnnouncementId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnnouncements_AnnouncementId1",
                table: "UsersAnnouncements",
                column: "AnnouncementId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAnnouncements_Announcements_AnnouncementId1",
                table: "UsersAnnouncements",
                column: "AnnouncementId1",
                principalTable: "Announcements",
                principalColumn: "Id");
        }
    }
}
