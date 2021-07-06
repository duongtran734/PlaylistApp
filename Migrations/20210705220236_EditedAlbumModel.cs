using Microsoft.EntityFrameworkCore.Migrations;

namespace PlaylistApp.Migrations
{
    public partial class EditedAlbumModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Albums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
