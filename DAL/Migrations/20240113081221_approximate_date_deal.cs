using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class approximate_date_deal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "StopDate",
                table: "Deals");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ApproximateDate",
                table: "Deals",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproximateDate",
                table: "Deals");

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Deals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StopDate",
                table: "Deals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
