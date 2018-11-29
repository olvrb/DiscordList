using Microsoft.EntityFrameworkCore.Migrations;

namespace FallProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildMembers",
                columns: table => new
                {
                    Id = table.Column<decimal>(nullable: false),
                    GuildId = table.Column<decimal>(nullable: false),
                    UnmuteAt = table.Column<decimal>(nullable: false),
                    Xp = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    id = table.Column<decimal>(nullable: false),
                    content = table.Column<string>(nullable: false),
                    channelid = table.Column<decimal>(nullable: false),
                    guildid = table.Column<decimal>(nullable: false),
                    authorid = table.Column<decimal>(nullable: false),
                    EditsAsString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildMembers");

            migrationBuilder.DropTable(
                name: "message");
        }
    }
}
