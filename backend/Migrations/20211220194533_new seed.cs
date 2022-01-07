using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class newseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8a700770-1369-4ad6-9b39-93242bea267b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a46164cb-1e4c-4d1f-966f-fec9a301a31b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "53ed36f3-f8e4-40b2-bfed-3e861addae11");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "49b517fa-37e6-45a7-9e29-e543f8bd4d8a", "AQAAAAEAACcQAAAAECUjlFhKI8ckOt9u9MD0mIB7+56cqk1CZ+3sfR7dXiAb821dXzbDZHt5x4hNZRcGMw==", "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "e9399456-2cf0-4c17-9adc-6ab93e625397" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "cbefb095-80d3-425b-8529-bd9b42cb0d9e", "Ek2NbjScFCcoUrTZDnIdFJyd3CNHxX4p0R8pfb9a/+A=", "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "77bb1986-b58e-46b0-9a36-0764eb0b9e66" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "d71480d2-2325-40ff-9b11-ba14f39f9011", "Ek2NbjScFCcoUrTZDnIdFJyd3CNHxX4p0R8pfb9a/+A=", "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "ca19a45b-a23b-475f-b455-8486206e3540" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "67c3a919-c82e-48ea-a47c-9fa6608d46f4", "Ek2NbjScFCcoUrTZDnIdFJyd3CNHxX4p0R8pfb9a/+A=", "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "174e0a3c-f41e-47a1-be99-6880eda57987" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dfc5819b-6258-4a9b-a175-32aec88eb2d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7ec758ea-29bd-4a82-9843-a7928a0181a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2fa99d7b-d5b3-47bf-8e5f-ee4a85d53607");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "7c626a73-7849-4a30-bc7c-447204b9c417", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=", null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "e7a2c73b-2a20-4295-b50d-3c4037575add", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=", null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "556ae830-8601-4a78-a0b5-d874db1f3194", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=", null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "password", "salt", "SecurityStamp" },
                values: new object[] { "f94a6a73-62e3-43ba-b15c-8f6b21c0119f", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=", null });
        }
    }
}
