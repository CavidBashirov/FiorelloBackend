using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorelloBackend.Migrations
{
    public partial class UpdateColumnSliderInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SignatureImg",
                table: "SliderInfos",
                newName: "SignImg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SignImg",
                table: "SliderInfos",
                newName: "SignatureImg");
        }
    }
}
