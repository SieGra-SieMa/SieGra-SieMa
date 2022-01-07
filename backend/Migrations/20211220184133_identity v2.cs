using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SieGraSieMa.Migrations
{
    public partial class identityv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "user",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "user",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "user",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "user",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 85, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 85, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    Name = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 85, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "5fce4829-6f60-4c52-93e7-fb3e1748b450", "9Bmh0pII0X/QLOMKLgP+Ga+iec9O4ZMPINbvKJGfaIU=", "Gw7C7t0msH5lYUhJSAcvoVgwAT8Q9fmBtBxMTJWWw1I=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "7a07b03a-a155-4305-917b-ae5644a99c0c", "9Bmh0pII0X/QLOMKLgP+Ga+iec9O4ZMPINbvKJGfaIU=", "Gw7C7t0msH5lYUhJSAcvoVgwAT8Q9fmBtBxMTJWWw1I=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "ab1b6b3e-dbb0-4c10-8bfc-03a57e5883cd", "9Bmh0pII0X/QLOMKLgP+Ga+iec9O4ZMPINbvKJGfaIU=", "Gw7C7t0msH5lYUhJSAcvoVgwAT8Q9fmBtBxMTJWWw1I=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "721d5fb7-63ee-4636-b3f6-03e33122924d", "9Bmh0pII0X/QLOMKLgP+Ga+iec9O4ZMPINbvKJGfaIU=", "Gw7C7t0msH5lYUhJSAcvoVgwAT8Q9fmBtBxMTJWWw1I=" });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "user",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "user",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "user");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "user");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "user");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "user");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "user");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "user");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "user");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "user");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "user");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "user");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "user");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "user");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "user");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "user");

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
