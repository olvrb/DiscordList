using Microsoft.EntityFrameworkCore.Migrations;

namespace FallProject.Migrations {
    public partial class InitialCreate : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                                         "message",
                                         table => new {
                                                          id        = table.Column<string>(nullable: false),
                                                          content   = table.Column<string>(nullable: false),
                                                          channelid = table.Column<string>(nullable: false),
                                                          guildid   = table.Column<string>(nullable: false),
                                                          authorid  = table.Column<string>(nullable: false)
                                                      },
                                         constraints: table => { table.PrimaryKey("PK_message", x => x.id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                                       "message");
        }
    }
}