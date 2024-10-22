using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class OpAddressLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPAdressLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPAdressLog", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3d4ff40-681a-407f-b4e8-508a65974684", "AQAAAAIAAYagAAAAEK0gSpTCF6LtwCeYYO4VtLrCsfYVwEdPZf1FknRR8or52SfzKbv1dR3xtoAFTJlLGQ==", "c56475d1-d334-421d-a144-b02d4fce55fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "841d3b44-c908-454d-9328-2cfcbfc19c2e", "AQAAAAIAAYagAAAAEAnGwmadymEzh7I7RfR6f3H48R2Eak3T4wCHzXwEFja+jIQJU4F+wxc//Xcusr+K6Q==", "5b8b5953-287a-433b-b09a-1f669078dcce" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPAdressLog");

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
    }
}
