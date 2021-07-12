using Microsoft.EntityFrameworkCore.Migrations;

namespace PlaylistApp.Migrations
{
    public partial class addedtitleproertytoSongentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Songs");
        }
    }
}
