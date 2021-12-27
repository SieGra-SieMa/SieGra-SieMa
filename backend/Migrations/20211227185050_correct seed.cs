using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class correctseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "user");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "user");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "97a3d137-b4b0-4dc0-a641-02d1626e775d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bcb32600-464d-419f-870c-5c8fc4cb84da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "63abd1a6-1d0c-402b-ba38-368839020aa0");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c93ddc7-33f1-41f0-b810-40d45ae7d578", true, "admin@gmail.com", "AQAAAAEAACcQAAAAED07CiCerH0A4WLDAQMdhmeHeyXa33sQmAgqBu4vVW8KKjEiUXr0QmpLH9zb5ptpXA==", "4c21df0b-aeb4-4d11-a525-13efcc1a5256" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21500a86-061c-4722-bae4-56b72859e5be", true, "pracownik@gmail.com", "AQAAAAEAACcQAAAAEKcSrIQaFxt5R9w9+f38oDuqqnVl0X7BIrnvj+YjIp7lM+qzHD4HKnfQEziCNvPKhw==", "4f5ab9a3-774b-4e6b-920c-582dc0da3c83" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51fe4305-8e21-4ef2-beca-3d797f17a380", true, "kapitan@gmail.com", "AQAAAAEAACcQAAAAEKtGd1FJJEjzXWRb6CJ+e7diA1mHd4U10lEB6fa2TEZhEsWXefxkVM039vKF5q/TvQ==", "c3a4f6ef-6222-4b00-90a3-bb1bc1cb9f2e" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "871572d7-0c58-4c21-aef7-18b9e087e7c9", true, "gracz@gmail.com", "AQAAAAEAACcQAAAAEPhrGy4OZNYb+EGzqQK1uBOQtLqvTFa/KkoNz4TARdxKLYglR75upuCqCfCP5jNkhQ==", "612b4c83-c52d-4091-9b68-e1ac851b606d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "user",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "user",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "password", "PasswordHash", "salt", "SecurityStamp" },
                values: new object[] { "49b517fa-37e6-45a7-9e29-e543f8bd4d8a", false, null, "AQAAAAEAACcQAAAAECUjlFhKI8ckOt9u9MD0mIB7+56cqk1CZ+3sfR7dXiAb821dXzbDZHt5x4hNZRcGMw==", null, "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "e9399456-2cf0-4c17-9adc-6ab93e625397" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "password", "PasswordHash", "salt", "SecurityStamp" },
                values: new object[] { "cbefb095-80d3-425b-8529-bd9b42cb0d9e", false, null, "Ek2NbjScFCcoUrTZDnIdFJyd3CNHxX4p0R8pfb9a/+A=", null, "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "77bb1986-b58e-46b0-9a36-0764eb0b9e66" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "password", "PasswordHash", "salt", "SecurityStamp" },
                values: new object[] { "d71480d2-2325-40ff-9b11-ba14f39f9011", false, null, "Ek2NbjScFCcoUrTZDnIdFJyd3CNHxX4p0R8pfb9a/+A=", null, "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "ca19a45b-a23b-475f-b455-8486206e3540" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "password", "PasswordHash", "salt", "SecurityStamp" },
                values: new object[] { "67c3a919-c82e-48ea-a47c-9fa6608d46f4", false, null, "Ek2NbjScFCcoUrTZDnIdFJyd3CNHxX4p0R8pfb9a/+A=", null, "rmbJsCKod5jERzbwepczkEmC+rDrZtomQkN3rJ4wYBc=", "174e0a3c-f41e-47a1-be99-6880eda57987" });
        }
    }
}
