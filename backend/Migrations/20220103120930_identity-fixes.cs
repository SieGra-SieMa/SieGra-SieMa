using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class identityfixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "70918f65-cf53-482e-b18a-9eae49d13eaa", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "223611a3-51df-48ef-8ce6-5e03bf9f28dc", "Emp" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "545290f6-3797-44f4-9c9a-8e75893910c7", "User" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ab3f7eb-a1f5-4f8e-8bbc-a923d38c92e6", "AQAAAAEAACcQAAAAEHUg4GvNj6MpaHUJZVGKCE6rdGvG1mog8WHXKTGeycAh9N4JJa96wJsOnstXC/OPMQ==", "8245e200-0eae-44ac-a2fd-74670c1166fb" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "009cad0f-ce8f-47a3-b961-73fccb3542e6", "AQAAAAEAACcQAAAAENjRLErJbe4i3QZIBsCBL4PVRGX3L9/PxGSJFrEZMp/wpU7ugLDF6O+nHKrmjlX0Tg==", "205a93c8-2d7e-4bf7-822a-f102480855f8" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db6ac4d6-ab17-477d-b15d-7aa6d46ce591", "AQAAAAEAACcQAAAAENHGecrABa0I1j/qH2ErzfHEWDi7YVBmHSY2ZY6O7jvYdnuAHdX0tXiRri2RoWHG6Q==", "ab71e3bc-dd32-4a0e-bb92-150c1556ab89" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7ab9b29-48cf-4bb6-9dd8-0cf549d57c66", "AQAAAAEAACcQAAAAEDcPfJecegOsq5ca+s2JlbjePjWUWs2uJ1zYC12wu/o7G5Vujw0q6MG2o5z3gys+tw==", "baf93244-a4cf-4922-9d57-88c403b662b1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "97a3d137-b4b0-4dc0-a641-02d1626e775d", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "bcb32600-464d-419f-870c-5c8fc4cb84da", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "63abd1a6-1d0c-402b-ba38-368839020aa0", null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c93ddc7-33f1-41f0-b810-40d45ae7d578", "AQAAAAEAACcQAAAAED07CiCerH0A4WLDAQMdhmeHeyXa33sQmAgqBu4vVW8KKjEiUXr0QmpLH9zb5ptpXA==", "4c21df0b-aeb4-4d11-a525-13efcc1a5256" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21500a86-061c-4722-bae4-56b72859e5be", "AQAAAAEAACcQAAAAEKcSrIQaFxt5R9w9+f38oDuqqnVl0X7BIrnvj+YjIp7lM+qzHD4HKnfQEziCNvPKhw==", "4f5ab9a3-774b-4e6b-920c-582dc0da3c83" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51fe4305-8e21-4ef2-beca-3d797f17a380", "AQAAAAEAACcQAAAAEKtGd1FJJEjzXWRb6CJ+e7diA1mHd4U10lEB6fa2TEZhEsWXefxkVM039vKF5q/TvQ==", "c3a4f6ef-6222-4b00-90a3-bb1bc1cb9f2e" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "871572d7-0c58-4c21-aef7-18b9e087e7c9", "AQAAAAEAACcQAAAAEPhrGy4OZNYb+EGzqQK1uBOQtLqvTFa/KkoNz4TARdxKLYglR75upuCqCfCP5jNkhQ==", "612b4c83-c52d-4091-9b68-e1ac851b606d" });
        }
    }
}
