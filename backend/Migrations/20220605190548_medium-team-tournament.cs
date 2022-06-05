using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class mediumteamtournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MediumId",
                table: "tournament",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediumId",
                table: "team",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7ecc0db6-5f83-4a79-b606-1be8e2ba649c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "51db9cda-fd63-4c4f-831d-9bab91756013");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6580c194-4591-46fd-b407-74db4a66b3b9");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b2f60b4-551b-48d4-ad93-6798fead9255", "AQAAAAEAACcQAAAAEPyphdUsid6R0/I/XWG3+BH/q9eAVdhFBEyWgy6Y5grsR6kJHhazE4GSoBUtCZIWfA==", "e8d670fb-08d9-43d4-9e21-3d2548d80f11" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1cabc5e3-934d-485f-bf41-70b300fc1fab", "AQAAAAEAACcQAAAAEFTD5s6XVOSvJlctLZm+OYGJrbUUnnMtkHH0jikhXYr8mwPZUxelcDjeuZnjVLPMHg==", "3a7bc004-cbed-44a4-b8a7-adc7d75503f3" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d48ccec-1afa-41c7-8647-150a78dbc38f", "AQAAAAEAACcQAAAAENiZXhrAbQcNxdO2jI07f9esFU1bncf5j83K1fzRRmfIYIwkR3loAeKOnU7h/Ci3GA==", "23903c5d-22bd-46f6-aa21-03324751c1f8" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5495bb15-0d75-4406-b104-b6a8594cedc4", "AQAAAAEAACcQAAAAEPCc6SntDULx0HR23KDtLk+083pN0IUi5tUAbVQxjkKPQAKPmlWd2ygowqZtKFmL7w==", "70ecb0b7-c902-44f6-91cf-6856bcde8315" });

            migrationBuilder.CreateIndex(
                name: "IX_tournament_MediumId",
                table: "tournament",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_team_MediumId",
                table: "team",
                column: "MediumId");

            migrationBuilder.AddForeignKey(
                name: "team_medium",
                table: "team",
                column: "MediumId",
                principalTable: "media",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "tournament_medium",
                table: "tournament",
                column: "MediumId",
                principalTable: "media",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "team_medium",
                table: "team");

            migrationBuilder.DropForeignKey(
                name: "tournament_medium",
                table: "tournament");

            migrationBuilder.DropIndex(
                name: "IX_tournament_MediumId",
                table: "tournament");

            migrationBuilder.DropIndex(
                name: "IX_team_MediumId",
                table: "team");

            migrationBuilder.DropColumn(
                name: "MediumId",
                table: "tournament");

            migrationBuilder.DropColumn(
                name: "MediumId",
                table: "team");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d94e3ce3-e2c6-4efc-929f-30ca48e3ad94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d03f2547-be3e-4a7a-9e8e-05032b6dc4b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "39bb7555-264c-4d5f-93cd-75c25e78fe92");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fca72cee-e4ff-4230-a9cf-f82800665db2", "AQAAAAEAACcQAAAAEOdyW+tYyXyGxuA19vVkI1agUPfVaEMBr4zOZbGvtedIQ0VeRx/bqXpbIn2utZ5SBg==", "cd8d7598-ff96-41e3-ac62-ab3b69e624fb" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8df7d149-dbc7-4972-84b3-e26c60d7cc9b", "AQAAAAEAACcQAAAAEFFwXDsUzw1ZSLr4L/qtONz9HQM/6oi3YCt8PjgzD4xUgxWOcvk+tl9YMJ507Yi5jg==", "356f4603-87b1-4d31-8d00-84a01be58b5f" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d754d25c-fbbd-43e3-99ad-5086fd5b4965", "AQAAAAEAACcQAAAAEMUTiVFbxJ1ivnVs9PKe5KsAB2H9mhjYvAzxGWu5wBHLWiawVIBNmzld8HSHkhBRzA==", "1e6f7245-b131-453e-b640-d00493c5f9bf" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c05b549-2934-43f2-9091-5840ae7f1578", "AQAAAAEAACcQAAAAEBhW5RQc/T2bbR2cr/L2dlG8RtrKrFXbBLsid9bBSMj4v+MGmjzmKyVjbvOvMKTRUw==", "4bb18fca-bad6-4630-843b-950d8e40e0bb" });
        }
    }
}
