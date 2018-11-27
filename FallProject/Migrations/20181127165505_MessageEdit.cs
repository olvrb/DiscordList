using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FallProject.Migrations
{
    public partial class MessageEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Edits",
                table: "message",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edits",
                table: "message");
        }
    }
}
