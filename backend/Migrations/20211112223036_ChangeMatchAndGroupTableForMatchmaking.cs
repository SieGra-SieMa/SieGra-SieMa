using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class ChangeMatchAndGroupTableForMatchmaking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date",
                table: "match");

            migrationBuilder.DropColumn(
                name: "referee",
                table: "match");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "match");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "tournament",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "group",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldFixedLength: true,
                oldMaxLength: 1);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "Emp");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3,
                column: "name",
                value: "User");

            migrationBuilder.UpdateData(
                table: "team",
                keyColumn: "id",
                keyValue: 1,
                column: "code",
                value: "ABCDE");

            migrationBuilder.UpdateData(
                table: "team",
                keyColumn: "id",
                keyValue: 2,
                column: "code",
                value: "EDCBA");

            migrationBuilder.InsertData(
                table: "tournament",
                columns: new[] { "id", "address", "description", "end_date", "name", "start_date" },
                values: new object[] { 1, "Zbożowa -1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turniej testowy numer 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "password", "salt" },
                values: new object[] { "Fqg7LOUe9eVFTwxGRRrLv0rgSY4EHkL1vTrlA065rvc=", "JCzZ5lbgkL6pl25XG6kqaPDwaqCbX2GWJg0hu0Lke5k=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "password", "salt" },
                values: new object[] { "Fqg7LOUe9eVFTwxGRRrLv0rgSY4EHkL1vTrlA065rvc=", "JCzZ5lbgkL6pl25XG6kqaPDwaqCbX2GWJg0hu0Lke5k=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "password", "salt" },
                values: new object[] { "Fqg7LOUe9eVFTwxGRRrLv0rgSY4EHkL1vTrlA065rvc=", "JCzZ5lbgkL6pl25XG6kqaPDwaqCbX2GWJg0hu0Lke5k=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "password", "salt" },
                values: new object[] { "Fqg7LOUe9eVFTwxGRRrLv0rgSY4EHkL1vTrlA065rvc=", "JCzZ5lbgkL6pl25XG6kqaPDwaqCbX2GWJg0hu0Lke5k=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tournament",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "tournament",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date",
                table: "match",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "referee",
                table: "match",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date",
                table: "match",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "group",
                type: "char(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "Administrator");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "Pracownik");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3,
                column: "name",
                value: "Użytkownik");

            migrationBuilder.UpdateData(
                table: "team",
                keyColumn: "id",
                keyValue: 1,
                column: "code",
                value: "ABCD");

            migrationBuilder.UpdateData(
                table: "team",
                keyColumn: "id",
                keyValue: 2,
                column: "code",
                value: "DCBA");

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
    }
}
