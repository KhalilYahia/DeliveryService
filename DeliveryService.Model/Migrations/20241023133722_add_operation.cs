using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_operation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Operation",
                table: "IPAdressLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "393013ea-0a8b-41f4-9dfd-c806e0f24ede", "AQAAAAIAAYagAAAAEHYxO6DBAXQj3rC/JtI/wrT89GxhkO2XNbYaC9iNklwvqHmYim5rZ+DrjY+zhCoS0Q==", "4a1e0cf7-c59b-4320-acd2-c69011e155af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38612ac7-ee1d-4ebe-a60a-5d9142d964f2", "AQAAAAIAAYagAAAAENwFB1aCg02OfC2eBZ1IgmReRUJfC3liSOzf8BNFZv7AxQdq0FP4D60ddYmEFD5+tw==", "4f17ffb2-8748-4f71-a2e6-b4390e256a1a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operation",
                table: "IPAdressLog");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81bfaa5a-a875-4928-a7f0-9de4f90a62eb", "AQAAAAIAAYagAAAAEBU2B05L/dnwclJ2BQK7pBbWQAd5xdAjQREV4BylYTN2q0POi8Kh0tKEXw+BNr06gw==", "2f3f5191-6db4-4402-ae96-eafe12883ef0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31484c2c-0428-4ba9-a418-0ad49c248380", "AQAAAAIAAYagAAAAEPsoruEhFPbEo7/cMwvXltGTILEQafDZMZ3J+5QWrNRgrxVGXNcIRAe3YtnSUAm49A==", "f723a037-127c-454a-8dbb-1eec2eed1b37" });
        }
    }
}
