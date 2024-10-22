using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrder_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalFunds");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Координаты = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98db8cf7-fcbb-4077-8d30-36a61d11199b", "AQAAAAIAAYagAAAAEBqd0zEy3OwJce1kBdGwB1y50AvWUrEfJ4+orPz4BSyVoPk+pH8i0JZuoi6n39pxJA==", "e8fbd97e-c7f1-485c-bc94-8241fface8b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7be03f56-4da4-47dd-844f-6860b3fe9778", "AQAAAAIAAYagAAAAEBfPWqCjL66iYBZUGce/3I/Evv5tjCOrslFDz29dQ0MQwZGx1/sA3FT6nb5m3EPung==", "1c68d223-6c0c-4891-8d0c-50bace84f754" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderNumber",
                table: "Order",
                column: "OrderNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.CreateTable(
                name: "TotalFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentFund = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarnedProfits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Profits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalFunds", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2db62fc-68cc-40fc-862e-65bead5b72fe", "AQAAAAIAAYagAAAAEFGCtMip5+LGEpxymp7BMCjaExMH5VE/WQ3FPsX+lMqF05R06o4EAJiNrNbkO28Hdw==", "98bfd146-dd59-4e98-b0a9-f60530eaea37" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f129f214-e111-4787-a4c7-24471269f29a", "AQAAAAIAAYagAAAAEF9XPIY5Q6V8jdTnFvl2OSqN/CuOJueiUnNgrL8+SWotPqDUhWhP+OYOWtkeuKwwPA==", "e448816a-5653-4049-a04a-42ab8417ebce" });
        }
    }
}
