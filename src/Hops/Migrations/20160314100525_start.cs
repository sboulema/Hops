using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Hops.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hop",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aroma = table.Column<string>(nullable: true),
                    BrewingUsage = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Pedigree = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hop", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Substitution",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HopId = table.Column<long>(nullable: false),
                    SubId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substitution", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Hop");
            migrationBuilder.DropTable("Substitution");
        }
    }
}
