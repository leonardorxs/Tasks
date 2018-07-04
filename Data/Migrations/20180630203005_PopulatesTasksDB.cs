using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasks.Data.Migrations
{
    public partial class PopulatesTasksDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Tasks VALUES ('Tarefa 1', 1, '20181010 08:00:00')");
            migrationBuilder.Sql("INSERT INTO Tasks VALUES ('Tarefa 2', 0, '20181010 08:00:00')");
            migrationBuilder.Sql("INSERT INTO Tasks VALUES ('Tarefa 3', 0, '20181010 08:00:00')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE * FROM Tasks");
        }
    }
}
