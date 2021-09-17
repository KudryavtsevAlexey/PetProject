using Microsoft.EntityFrameworkCore.Migrations;

namespace PetProject.Migrations
{
    public partial class AddedExecutionPriorityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExecutionPriority",
                table: "TaskModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutionPriority",
                table: "TaskModels");
        }
    }
}
