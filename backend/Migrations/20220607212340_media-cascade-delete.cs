using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class mediacascadedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                value: "cbf10858-a13c-4156-8e08-81d58480df1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bf21eac7-40d3-41bd-ba67-1425b3134794");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "127e1e40-d1d6-45fd-9971-3cd58cd9d194");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4764238a-a0db-45aa-a83d-4c660b193488", "AQAAAAEAACcQAAAAEHzh5j9adddIrPsx5nuX30WQa8tH5BL1VN9JL9NGDcCsIOMGdmC0Oh7v4FEI5ebaSA==", "4fa3044d-1407-410e-a11f-2b4b9238e6a3" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36e65dc8-668e-4d00-b800-10eb08c96cbf", "AQAAAAEAACcQAAAAEElRD9dFHM7wN27i65k2ij0qQ/saEPRAUjLa+9DRnS8hInvf/QBMJAm1PJwg+4NKRg==", "d4d698cd-7109-45e8-8544-b0311df2c951" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9d4d10c-80c2-447c-8893-32283822f907", "AQAAAAEAACcQAAAAEHOO0GEgFiFT2Qz/M+q0Ks09YzgaG39kzddw8Yios8Jn4ayvbQp+3GFnOJFszBYyUw==", "941689bf-f145-4f88-8b07-3a68e9acfd28" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93154686-1b68-48d0-8240-81a0005eea77", "AQAAAAEAACcQAAAAEPy5cdaYetfixEX6psWAXyWdH+zyl3EoqUBwaSe9Zmuxl1PV99Jp0CJfg08hiF0VXQ==", "93541d66-4ec4-4fc1-87c2-038c7f60f5b9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
