using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class captainCanBeNullInTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "captain_id",
                table: "team",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "tournament_id",
                table: "album",
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
                value: "bf2f9035-6bf2-4b76-a1a8-19b5ff580e5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ceb42bba-6238-402e-9868-27f7a90227e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "753b2024-c096-4fa3-80df-378f37d02baa");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "685e4212-208c-44f6-a810-2cc56ccc02b0", "AQAAAAEAACcQAAAAEOFSLXuEHnNncQFiad4m85/0x85wORPAyQE5WK8ncxtWLLxoBRBim7o5tOvm9Ozs5Q==", "7ca3ef0f-088e-447a-883f-c5c92bdba4b9" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7460d836-1e7b-4181-9206-e7be181fc174", "AQAAAAEAACcQAAAAEBeP6bO8Qbp0ee5Ni0TvfOKGGKZKOYpfrB6+DhdXaobbn17/nDgVNjW3kz0iNNYa1A==", "652ac659-8838-46ff-bae5-db7647c6ce97" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e55ff576-dafe-4c70-aa04-b967a1fb06d9", "AQAAAAEAACcQAAAAEN4XMeEpTdw4pu1bsxJBxWn5RI73bC5Yy8x5q72rPCA/S/iDNEKH0o7OKQtaBBLzUg==", "f5009782-c8f7-49dc-bfac-abed514c3984" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fb1a364-e73d-4b21-95bc-ce54ce3f9f53", "AQAAAAEAACcQAAAAEC9TBsN9+Esq6CxW6osGKtiDoDFVXGr84621jcU2Cq0qNNrrTIbpAlkBRfsxg3e65w==", "e0122aab-da0c-4075-8831-ba64bf96db37" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "captain_id",
                table: "team",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "tournament_id",
                table: "album",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
