using Microsoft.EntityFrameworkCore.Migrations;

namespace ExTest2.Migrations
{
    public partial class AddedTableNameAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confectioneries_Orders_Confectioneries_IdConfectionery",
                table: "Confectioneries_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Confectioneries_Orders_Orders_IdOrder",
                table: "Confectioneries_Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_IdEmployee",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Confectioneries_Orders",
                table: "Confectioneries_Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Confectioneries",
                table: "Confectioneries");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Confectioneries_Orders",
                newName: "Confectionery_Order");

            migrationBuilder.RenameTable(
                name: "Confectioneries",
                newName: "Confectionery");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_IdEmployee",
                table: "Order",
                newName: "IX_Order_IdEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_IdCustomer",
                table: "Order",
                newName: "IX_Order_IdCustomer");

            migrationBuilder.RenameIndex(
                name: "IX_Confectioneries_Orders_IdConfectionery",
                table: "Confectionery_Order",
                newName: "IX_Confectionery_Order_IdConfectionery");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "IdOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "IdEmployee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "IdCustomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Confectionery_Order",
                table: "Confectionery_Order",
                columns: new[] { "IdOrder", "IdConfectionery" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Confectionery",
                table: "Confectionery",
                column: "IdConfectionery");

            migrationBuilder.AddForeignKey(
                name: "FK_Confectionery_Order_Confectionery_IdConfectionery",
                table: "Confectionery_Order",
                column: "IdConfectionery",
                principalTable: "Confectionery",
                principalColumn: "IdConfectionery",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Confectionery_Order_Order_IdOrder",
                table: "Confectionery_Order",
                column: "IdOrder",
                principalTable: "Order",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_IdCustomer",
                table: "Order",
                column: "IdCustomer",
                principalTable: "Customer",
                principalColumn: "IdCustomer",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Employee_IdEmployee",
                table: "Order",
                column: "IdEmployee",
                principalTable: "Employee",
                principalColumn: "IdEmployee",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confectionery_Order_Confectionery_IdConfectionery",
                table: "Confectionery_Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Confectionery_Order_Order_IdOrder",
                table: "Confectionery_Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_IdCustomer",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Employee_IdEmployee",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Confectionery_Order",
                table: "Confectionery_Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Confectionery",
                table: "Confectionery");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Confectionery_Order",
                newName: "Confectioneries_Orders");

            migrationBuilder.RenameTable(
                name: "Confectionery",
                newName: "Confectioneries");

            migrationBuilder.RenameIndex(
                name: "IX_Order_IdEmployee",
                table: "Orders",
                newName: "IX_Orders_IdEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_Order_IdCustomer",
                table: "Orders",
                newName: "IX_Orders_IdCustomer");

            migrationBuilder.RenameIndex(
                name: "IX_Confectionery_Order_IdConfectionery",
                table: "Confectioneries_Orders",
                newName: "IX_Confectioneries_Orders_IdConfectionery");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "IdOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "IdEmployee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "IdCustomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Confectioneries_Orders",
                table: "Confectioneries_Orders",
                columns: new[] { "IdOrder", "IdConfectionery" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Confectioneries",
                table: "Confectioneries",
                column: "IdConfectionery");

            migrationBuilder.AddForeignKey(
                name: "FK_Confectioneries_Orders_Confectioneries_IdConfectionery",
                table: "Confectioneries_Orders",
                column: "IdConfectionery",
                principalTable: "Confectioneries",
                principalColumn: "IdConfectionery",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Confectioneries_Orders_Orders_IdOrder",
                table: "Confectioneries_Orders",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

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
    }
}
