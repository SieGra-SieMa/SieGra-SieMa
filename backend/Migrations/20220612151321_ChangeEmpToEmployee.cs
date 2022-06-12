using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class ChangeEmpToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3e2c088c-0385-4894-978d-4b8d1c0b089e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "09f98fa8-6932-43ae-b186-54bb3df0fec1", "Employee" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0f0fff19-77c9-4773-87f3-847ced49f7b5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "513306c8-5b73-4079-bdd8-2e27f5a0f5a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "0032aa79-cfc9-4549-bb44-b99e013a909f", "Emp" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "39f79fc7-c19b-4b9b-b1b4-0b2ae59e9c74");
        }
    }
}
