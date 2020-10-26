using Microsoft.EntityFrameworkCore.Migrations;

namespace URLWhitelsit.Migrations
{
    public partial class ChangedInResultTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedAnswer",
                table: "Results",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAnswer",
                table: "Results");
        }
    }
}
