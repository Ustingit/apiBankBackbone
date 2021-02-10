using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiBankBackBone.Data.Migrations
{
    public partial class addapiandmethodmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsFree = table.Column<bool>(nullable: false),
                    AccessCost = table.Column<decimal>(nullable: false),
                    MonthlyCost = table.Column<decimal>(nullable: false),
                    AdditionalAccessRules = table.Column<string>(nullable: false),
                    License = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Methods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApiId = table.Column<Guid>(nullable: false),
                    Verb = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apis");

            migrationBuilder.DropTable(
                name: "Methods");
        }
    }
}
