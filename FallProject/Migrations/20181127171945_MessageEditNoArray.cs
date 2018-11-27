using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FallProject.Migrations
{
    public partial class MessageEditNoArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edits",
                table: "message");

            migrationBuilder.AddColumn<string>(
                name: "EditsAsString",
                table: "message",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditsAsString",
                table: "message");

            migrationBuilder.AddColumn<List<string>>(
                name: "Edits",
                table: "message",
                nullable: true);
        }
    }
}
