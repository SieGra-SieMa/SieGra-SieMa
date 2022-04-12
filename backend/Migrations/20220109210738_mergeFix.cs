using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class mergeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f8b48b49-2bff-435e-b60b-e618cef935e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "53947a80-840e-4da1-9375-85c12e18e318");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "614dcd0a-affd-4fd4-af13-1341245ff6d8");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "259617d9-f8b8-492e-bcdb-cc55db1e2030", "AQAAAAEAACcQAAAAEKttNKQYq3OOBpUI8lALtKk9tQJWrkf5GGlPT3jUN4Oy2ugHKF70WBQ3MQjR2oRvAQ==", "457cd223-68a5-4dab-b5eb-1f39b8bfd827" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c869d21-c3fe-49df-a462-f2ad9f588ae0", "AQAAAAEAACcQAAAAEMc7lB1A5ox0fCoK1xsi9sgqaFIWZKsmTIbxSaRX5ccdqs+0dVD50EyDm1lMLzvxbw==", "ea33d2c1-4430-490b-b63e-9c788b45cd67" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbb815f2-0418-44fe-bfbb-3f8d55c04f23", "AQAAAAEAACcQAAAAEPKR6UfcGmNmFQ43NAO9jYhITe/VIGTX9MjffQbzvJz3xFMFMT2N3HPTqaHWVEgrFw==", "527ce89a-2c6b-469c-a353-0285af097c23" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2338a37d-c06b-4ec0-9717-c0a678d11e4f", "AQAAAAEAACcQAAAAEH8gvG7cHtb6iAXuut5ihFG1tx9PLNdalH4FnUgwNwe3lkUqqM7rxwtc1DSpkm4JoA==", "e4d81bb5-05f4-409b-aa0f-686387cc861e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "70918f65-cf53-482e-b18a-9eae49d13eaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "223611a3-51df-48ef-8ce6-5e03bf9f28dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "545290f6-3797-44f4-9c9a-8e75893910c7");

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
    }
}
