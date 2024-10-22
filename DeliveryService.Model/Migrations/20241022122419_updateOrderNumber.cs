using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderNumber",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
    }
}
