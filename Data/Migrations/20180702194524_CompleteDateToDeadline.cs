using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasks.Data.Migrations
{
    public partial class CompleteDateToDeadline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompletedDate",
                table: "Tasks",
                newName: "Deadline");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "Tasks",
                newName: "CompletedDate");
        }
    }
}
