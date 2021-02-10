using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiBankBackBone.Data.Migrations
{
    public partial class addapiandmethodmodelstry2withspecifiedtypenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyCost",
                table: "Apis",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccessCost",
                table: "Apis",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyCost",
                table: "Apis",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccessCost",
                table: "Apis",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}
