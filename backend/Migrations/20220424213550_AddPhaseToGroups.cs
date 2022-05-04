using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class AddPhaseToGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "phase",
                table: "group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f549d8fa-2cfa-4e63-8ea8-ce3364853c16");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e391dd02-8088-4966-b5d0-dd045d33a956");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "19712254-d755-4df3-ad82-49fec4345003");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc91146b-851a-4504-a646-fc8e51b1447e", "AQAAAAEAACcQAAAAEMHbG20DStwYORajE6gTnmmHtH9+YI2YQJDnB6+NKh3I/2r4NuRscEyc4Ir01jEl7w==", "49496d3a-1241-4d5c-9026-4e3b8c006d09" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f849bf9f-bd2c-4b62-a450-ea8f07201522", "AQAAAAEAACcQAAAAEDsHoVArIC2hzzd7tH0Z60eMobXxYfTjYdaiOaKt2FHwrunBUzzNv3m/DqDUlz2ifQ==", "7a4de017-3129-4b02-b2e0-7af1afdac04f" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c09666b3-21f2-44f3-b626-2d2e47c4a31b", "AQAAAAEAACcQAAAAEEy1RrRP8IuK+F0i1R/TJiDBr9DPyId/qfJeDVORU5u5/8bbAfVebMLaCFVq6YwzEw==", "2678df16-fcd8-46d4-aefa-d644aff7e94b" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48c361cc-763c-49d7-952b-51221c3def18", "AQAAAAEAACcQAAAAEAYlelwpLiQ+Fvn2q13d0yglnNsr7e7j/TKqzI2zH+hzfhiyvpb+lgglZ//LbY60ng==", "028a25a2-ea63-4a26-ac2c-8b5c944a90f8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phase",
                table: "group");

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
    }
}
