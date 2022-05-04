using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class AddNullsForTeamsInTeamInGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "team_id",
                table: "team_in_group",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c5a9253e-7aa6-4a7c-a5de-8ab1e2f1b677");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9e208d4d-4f32-4ed0-852f-085dc6ee131b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fffcba93-c189-467e-8a16-b7ed64c6b162");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8206e821-91de-4453-9755-a37337670e76", "AQAAAAEAACcQAAAAEDp/4ynVO4J2laMYvin/k9uh0DtL4p+dyjOmdVWXq8DCxOQKPHvbM0qkdUWsWHpKGQ==", "2ac2781e-f831-4411-86cc-03432c5996ed" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afbb79ee-2b57-41be-b230-6ceaa518bb9c", "AQAAAAEAACcQAAAAENP7y3WiPATkr41TsRvttfHHm2jJPYTkVDKwuxy6IhAwCJT22JRGnxhxrqPbpglw4Q==", "bf2b009a-d8ba-46da-9ffc-fc4d611b3283" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b101608b-a23c-434b-bcd9-a9d3e4e9a534", "AQAAAAEAACcQAAAAEHNIj0mjaPfbShkz0vDai18f1PBAIMxP0v+rDl9FM0rAH4evWiNdI/SFF3udr9V/wg==", "016a167d-4c9d-48c8-b93f-60d41fa537ed" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58fb9b0f-dd72-4e53-9039-0fd457fe41de", "AQAAAAEAACcQAAAAEOQN7ShPU/n2o5gRmvWN3QRmF2y7sO0em2YKPUBs7M0I01/vcg0PzSKR0CfPbQKBWA==", "22e5d7f8-be85-4604-b697-fb67efcea88b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "team_id",
                table: "team_in_group",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6fafb2dd-a732-4371-aeba-574c7be5deec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7df91437-da27-4725-9a1d-a1352f9fc0ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "01159956-4aa5-4a78-8043-8262a674e9b0");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be1eabad-df9a-4e58-8a15-6716d1a38e00", "AQAAAAEAACcQAAAAEMfSHkF2hwfqH2hxNNJe5dzKcnq2rn3Env6ol/MJLwbLWYkTQLfHEV2sU40TgwiHAQ==", "aa2ef628-d697-47d0-8d06-59cb7b3f4693" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee461096-8098-46cd-8555-8909106f09ea", "AQAAAAEAACcQAAAAECr5nbn3Z0bK5JH4c5j7sP4lp3uqHRKnERiiE1Qihev/zAZBg0C4HSuzbuFYIploFw==", "9a9db7a9-3ac2-49c0-81c7-fbfe384cc03e" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "938a9846-de3f-4dd5-a987-121ff94191ba", "AQAAAAEAACcQAAAAEFw+I47PQKLJfLZ6QAtZc/++a0iinYePg/KPW3ca4rh+kCp8UPEUggmUppMuUc47zA==", "700468f1-7d41-4442-ae3c-610b782a2e82" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48721dc3-afdd-4196-8ca3-04d3cd19b56d", "AQAAAAEAACcQAAAAEHRxO0TBBauqRiH7Y23CPJFZ/bcqevTyghvUPD2FOP9WEZl6N9dVHT0TgFZ/H6yGoQ==", "24fe531f-3dda-4e23-89db-852030972812" });
        }
    }
}
