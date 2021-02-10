using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiBankBackBone.Data.Migrations
{
    public partial class addnameproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Apis",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Apis");
        }
    }
}
