using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FallProject.Migrations {
    public partial class MessageEdit : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<List<string>>(
                                                     "Edits",
                                                     "message",
                                                     nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                                        "Edits",
                                        "message");
        }
    }
}