using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTracker.API.Migrations
{
    public partial class InitialSetupCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Valies",
                table: "Valies");

            migrationBuilder.RenameTable(
                name: "Valies",
                newName: "Values");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Values",
                table: "Values",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Values",
                table: "Values");

            migrationBuilder.RenameTable(
                name: "Values",
                newName: "Valies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Valies",
                table: "Valies",
                column: "Id");
        }
    }
}
