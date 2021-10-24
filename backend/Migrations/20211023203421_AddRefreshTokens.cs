using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SieGraSieMa.Migrations
{
    public partial class AddRefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Table_28_user",
                table: "newsletter");

            migrationBuilder.DropForeignKey(
                name: "Table_25_team",
                table: "team_in_group");

            migrationBuilder.DropForeignKey(
                name: "team_group",
                table: "team_in_group");

            migrationBuilder.DropForeignKey(
                name: "Table_27_team",
                table: "team_in_tournament");

            migrationBuilder.DropForeignKey(
                name: "Table_27_tournament",
                table: "team_in_tournament");

            migrationBuilder.DropForeignKey(
                name: "Table_26_role",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "Table_26_user",
                table: "user_roles");

            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    token = table.Column<string>(type: "text", nullable: false),
                    expires = table.Column<DateTime>(type: "datetime", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdByIp = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    revoked = table.Column<DateTime>(type: "datetime", nullable: true),
                    revokedByIp = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    replacedByToken = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "refresh_token_user",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Emp" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "email", "name", "password", "salt", "surname" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "Adm", "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=", "In" },
                    { 2, "pracownik@gmail.com", "Prac", "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=", "Ownik" },
                    { 3, "kapitan@gmail.com", "Kap", "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=", "Itan" },
                    { 4, "gracz@gmail.com", "Gr", "zp4T0xc46FUkh8xbPl0lZpzt/oRUVI1eZPRf0kiH3Ok=", "mnUKOG9o0N+XellFoMC8Oo0IomwLFygk6AhBDQqdzI0=", "acz" }
                });

            migrationBuilder.InsertData(
                table: "newsletter",
                columns: new[] { "id", "user_id" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "team",
                columns: new[] { "id", "captain_id", "code", "name" },
                values: new object[,]
                {
                    { 1, 3, "ABCDE", "Bogowie" },
                    { 2, 3, "EDCBA", "Demony" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "player",
                columns: new[] { "team_id", "user_id" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "player",
                columns: new[] { "team_id", "user_id" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "player",
                columns: new[] { "team_id", "user_id" },
                values: new object[] { 2, 3 });

            migrationBuilder.CreateIndex(
                name: "Id",
                table: "refresh_token",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_UserId",
                table: "refresh_token",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "newsletter_user",
                table: "newsletter",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "team_in_group_group",
                table: "team_in_group",
                column: "group_id",
                principalTable: "group",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "team_in_group_team",
                table: "team_in_group",
                column: "team_id",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "team_in_tournament_team",
                table: "team_in_tournament",
                column: "team_id",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "team_in_tournament_tournament",
                table: "team_in_tournament",
                column: "tournament_id",
                principalTable: "tournament",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "user_role_role",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "user_role_user",
                table: "user_roles",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "newsletter_user",
                table: "newsletter");

            migrationBuilder.DropForeignKey(
                name: "team_in_group_group",
                table: "team_in_group");

            migrationBuilder.DropForeignKey(
                name: "team_in_group_team",
                table: "team_in_group");

            migrationBuilder.DropForeignKey(
                name: "team_in_tournament_team",
                table: "team_in_tournament");

            migrationBuilder.DropForeignKey(
                name: "team_in_tournament_tournament",
                table: "team_in_tournament");

            migrationBuilder.DropForeignKey(
                name: "user_role_role",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "user_role_user",
                table: "user_roles");

            migrationBuilder.DropTable(
                name: "refresh_token");

            migrationBuilder.DeleteData(
                table: "newsletter",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "player",
                keyColumns: new[] { "team_id", "user_id" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "player",
                keyColumns: new[] { "team_id", "user_id" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "player",
                keyColumns: new[] { "team_id", "user_id" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "team",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "team",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.AddForeignKey(
                name: "Table_28_user",
                table: "newsletter",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_25_team",
                table: "team_in_group",
                column: "team_id",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "team_group",
                table: "team_in_group",
                column: "group_id",
                principalTable: "group",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_27_team",
                table: "team_in_tournament",
                column: "team_id",
                principalTable: "team",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_27_tournament",
                table: "team_in_tournament",
                column: "tournament_id",
                principalTable: "tournament",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_26_role",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Table_26_user",
                table: "user_roles",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
