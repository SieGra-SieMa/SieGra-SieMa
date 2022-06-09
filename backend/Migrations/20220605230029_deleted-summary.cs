using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class deletedsummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "summary",
                table: "tournament");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6f5c6404-ef3e-4db2-a392-ea4a3c62dcad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "03e49fb1-174e-43ff-81d2-89f1d45df6b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3c1bf336-139f-41d7-bf13-c9006f511a1d");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edc537da-f551-4e32-9340-226d2d4e0235", "AQAAAAEAACcQAAAAEDKQzT1Lb/ipjBbAbBt6CFlsEOANOQEoVnDVCoVQMNg5VMyI28ZegllY2JPvLJnySg==", "ddcbb0ab-0056-4a93-a3a3-7e4a10084abb" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20e24c60-b927-4b7d-9dcc-27c6a8c2b928", "AQAAAAEAACcQAAAAEInT4Op0tZPcVJiYWZjV74gKqRr5kqp6fI6GDS7cd2imlYYK4UxGavCsSfbfEDNeTA==", "de88bb30-4be1-4fd2-ad2e-6a64bb8e43e8" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97b99ab1-35e5-4b5c-8e27-0400c276fd7b", "AQAAAAEAACcQAAAAEMAbfkFdBkysdvF+zDZ7v4ZSwFYu/xt7PkB4SJeyWuasjKvr2Y5sK0HOOoDe3GfARA==", "b323011b-e0ec-43cf-8fe1-85088fe5720e" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b04ad61b-4cf4-47af-8d97-a9375e48027c", "AQAAAAEAACcQAAAAEPnsp9vBzq9uf51nMwtNjOSBzjqLspIYDhgfaZ7MOjx1zoU0glpKNty7XkyZh4g9dQ==", "afa10294-3b4e-4217-aad7-ac33db27cf48" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "tournament",
                type: "text",
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
        }
    }
}
