using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class RepairRefreshTokensByNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "revokedByIp",
                table: "refresh_token",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "replacedByToken",
                table: "refresh_token",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "password", "salt" },
                values: new object[] { "b24LKo6IxW6bO+vMJ67prmsu19fELXEoxUgVazMy7OU=", "XA3ciUNSx3mxr27NnYq8Zt3xGcV8FOQVcONg6YsJHB0=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "password", "salt" },
                values: new object[] { "b24LKo6IxW6bO+vMJ67prmsu19fELXEoxUgVazMy7OU=", "XA3ciUNSx3mxr27NnYq8Zt3xGcV8FOQVcONg6YsJHB0=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "password", "salt" },
                values: new object[] { "b24LKo6IxW6bO+vMJ67prmsu19fELXEoxUgVazMy7OU=", "XA3ciUNSx3mxr27NnYq8Zt3xGcV8FOQVcONg6YsJHB0=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "password", "salt" },
                values: new object[] { "b24LKo6IxW6bO+vMJ67prmsu19fELXEoxUgVazMy7OU=", "XA3ciUNSx3mxr27NnYq8Zt3xGcV8FOQVcONg6YsJHB0=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "revokedByIp",
                table: "refresh_token",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "replacedByToken",
                table: "refresh_token",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "password", "salt" },
                values: new object[] { "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "password", "salt" },
                values: new object[] { "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "password", "salt" },
                values: new object[] { "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "password", "salt" },
                values: new object[] { "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=" });
        }
    }
}
