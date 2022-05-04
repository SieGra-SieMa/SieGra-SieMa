using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class AddNullsForTeamsInMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "team_home_score",
                table: "match",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "team_home_id",
                table: "match",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "team_away_score",
                table: "match",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "team_away_id",
                table: "match",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "team_home_score",
                table: "match",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "team_home_id",
                table: "match",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "team_away_score",
                table: "match",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "team_away_id",
                table: "match",
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
                value: "7adda3ad-444d-4b7d-aa42-1c9108963578");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "951819df-b7ee-4042-add7-0c330ac381b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e8b16a4b-4240-4028-9503-e59036bdfade");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25c96dca-7c9b-4e50-a1ea-dbf60e3b600c", "AQAAAAEAACcQAAAAENL9nThV0Zlq/ESgjRKKt08IWVzQIXRuCjZeNIRe5frXPUSUgeBKDcQ6xZ81bABTrw==", "e845e3e5-1518-46ea-a817-593464778588" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "700dce6f-af95-4df2-86f7-09e23fad2ef7", "AQAAAAEAACcQAAAAEJrZ2QzG1HxvtJICtXcutunOqI3iiDo9cZaTs3aEjGDOsyytHrxxEitVZRLYEvjbJA==", "52c62210-e897-4611-8a31-b79998d3a82b" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e36ec28-c099-4625-86d4-24f3b739d088", "AQAAAAEAACcQAAAAEC00oEdkoXNLts6ELSpqPZcmfKW73Bm13nBy7sZ9/dzFh2yrETyEJ2DSkVi7rDHPqQ==", "11918afe-cdeb-46eb-a8f5-29efdfefe712" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5439c69-dc97-4db7-a74b-7390dc0f4a15", "AQAAAAEAACcQAAAAELCpsk4c4TmMLYXfxAFwXgnJl/jFyLqPpUsi1Yiu9ur454zMNdQtRNECgLlQyP0GtA==", "43fcfc51-8036-4942-9806-07d074084474" });
        }
    }
}
