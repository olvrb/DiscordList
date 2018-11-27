using Microsoft.EntityFrameworkCore.Migrations;

namespace FallProject.Migrations
{
    public partial class GuildMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildMembers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    GuildId = table.Column<string>(nullable: true),
                    UnmuteAt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMembers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildMembers");
        }
    }
}
