using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Hops.Migrations
{
    public partial class doubles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalOilMin",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<double>(
                name: "TotalOilMax",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<double>(
                name: "BetaMin",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<double>(
                name: "BetaMax",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<double>(
                name: "AlphaMin",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<double>(
                name: "AlphaMax",
                table: "Hop",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalOilMin",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "TotalOilMax",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "BetaMin",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "BetaMax",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "AlphaMin",
                table: "Hop",
                nullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "AlphaMax",
                table: "Hop",
                nullable: false);
        }
    }
}
