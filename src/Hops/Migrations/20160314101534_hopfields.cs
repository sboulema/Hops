using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Hops.Migrations
{
    public partial class hopfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlphaMax",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "AlphaMin",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "BetaMax",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "BetaMin",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "CoHumuloneMax",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "CoHumuloneMin",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Hop",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Styles",
                table: "Hop",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "TotalOilMax",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "TotalOilMin",
                table: "Hop",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<string>(
                name: "Trade",
                table: "Hop",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "AlphaMax", table: "Hop");
            migrationBuilder.DropColumn(name: "AlphaMin", table: "Hop");
            migrationBuilder.DropColumn(name: "BetaMax", table: "Hop");
            migrationBuilder.DropColumn(name: "BetaMin", table: "Hop");
            migrationBuilder.DropColumn(name: "CoHumuloneMax", table: "Hop");
            migrationBuilder.DropColumn(name: "CoHumuloneMin", table: "Hop");
            migrationBuilder.DropColumn(name: "Info", table: "Hop");
            migrationBuilder.DropColumn(name: "Styles", table: "Hop");
            migrationBuilder.DropColumn(name: "TotalOilMax", table: "Hop");
            migrationBuilder.DropColumn(name: "TotalOilMin", table: "Hop");
            migrationBuilder.DropColumn(name: "Trade", table: "Hop");
        }
    }
}
