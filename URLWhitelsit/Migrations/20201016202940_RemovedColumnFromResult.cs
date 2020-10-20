using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URLWhitelsit.Migrations
{
    public partial class RemovedColumnFromResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedDate",
                table: "Results");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ChangedDate",
                table: "Results",
                nullable: true);
        }
    }
}
