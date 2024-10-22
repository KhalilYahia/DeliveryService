using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateorderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Координаты",
                table: "Order",
                newName: "Coordinates");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1fd20ba-aa38-47ed-a716-d7ee158a7f92", "AQAAAAIAAYagAAAAENIxlcMiSM8bQFH+Ejlne/oL04FBVMnBlecEixSbXZX1htr48AMDLbdWHS6CKi0uLg==", "e7a5bebd-85cc-4510-9d9e-351c04ea140d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61cc40d5-8c3e-4449-89f1-2b95f54053c9", "AQAAAAIAAYagAAAAEBEZ5yK68sD+Gk/JsISn+5jIX5M9B3MqquWcfaOZb0FzCAH3Bpnf2T+u2Ice7BtgyA==", "be6e772e-3db1-45aa-8ca7-473a5f467191" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinates",
                table: "Order",
                newName: "Координаты");

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
        }
    }
}
