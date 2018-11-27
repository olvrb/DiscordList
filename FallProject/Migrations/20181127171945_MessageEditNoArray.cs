using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FallProject.Migrations {
    public partial class MessageEditNoArray : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                                        "Edits",
                                        "message");

            migrationBuilder.AddColumn<string>(
                                               "EditsAsString",
                                               "message",
                                               nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                                        "EditsAsString",
                                        "message");

            migrationBuilder.AddColumn<List<string>>(
                                                     "Edits",
                                                     "message",
                                                     nullable: true);
        }
    }
}