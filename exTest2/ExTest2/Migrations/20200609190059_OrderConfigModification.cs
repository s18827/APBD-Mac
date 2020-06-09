using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExTest2.Migrations
{
    public partial class OrderConfigModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFinished",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFinished",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
