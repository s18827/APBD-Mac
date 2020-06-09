using Microsoft.EntityFrameworkCore.Migrations;

namespace ExTest2.Migrations
{
    public partial class CustomerNullableFieldsModif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_IdEmployee",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "IdEmployee",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdCustomer",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "IdCustomer",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_IdEmployee",
                table: "Orders",
                column: "IdEmployee",
                principalTable: "Employees",
                principalColumn: "IdEmployee",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_IdEmployee",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "IdEmployee",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCustomer",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "IdCustomer",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_IdEmployee",
                table: "Orders",
                column: "IdEmployee",
                principalTable: "Employees",
                principalColumn: "IdEmployee",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
