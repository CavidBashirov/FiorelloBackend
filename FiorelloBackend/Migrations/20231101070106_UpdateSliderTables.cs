using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorelloBackend.Migrations
{
    public partial class UpdateSliderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sliders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
