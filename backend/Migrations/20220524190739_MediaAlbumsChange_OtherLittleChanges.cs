using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class MediaAlbumsChange_OtherLittleChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "media_album",
                table: "media");

            migrationBuilder.DropForeignKey(
                name: "player_user",
                table: "player");

            migrationBuilder.DropIndex(
                name: "media_album",
                table: "media");

            migrationBuilder.DropColumn(
                name: "album_id",
                table: "media");

            migrationBuilder.RenameIndex(
                name: "Table_27_tournament",
                table: "team_in_tournament",
                newName: "team_in_tournament_tournament");

            migrationBuilder.RenameIndex(
                name: "team_group",
                table: "team_in_group",
                newName: "team_in_group_group");

            migrationBuilder.RenameIndex(
                name: "Table_25_team",
                table: "team_in_group",
                newName: "team_in_group_team");

            migrationBuilder.RenameIndex(
                name: "Table_28_user",
                table: "newsletter",
                newName: "newsletter_user");

            migrationBuilder.Sql(@"ALTER TABLE `match` CHANGE `TournamentId` `tournament_Id` INT NOT NULL DEFAULT '0', CHANGE `Phase` `phase` INT NOT NULL DEFAULT '0';");

            /*migrationBuilder.RenameColumn(
                name: "Phase",
                table: "match",
                newName: "phase");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "match",
                newName: "tournament_id");*/

            migrationBuilder.RenameIndex(
                name: "match_away",
                table: "match",
                newName: "IX_match_team_away_id");

            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "tournament",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "captain_id",
                table: "team",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "contestants",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "album",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "medium_in_album",
                columns: table => new
                {
                    album_id = table.Column<int>(type: "int", nullable: false),
                    medium_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.album_id, x.medium_id });
                    table.ForeignKey(
                        name: "medium_in_album_album",
                        column: x => x.album_id,
                        principalTable: "album",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "medium_in_album_media",
                        column: x => x.medium_id,
                        principalTable: "media",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b7065742-4e3f-4c9b-843a-ed4fa6af8752");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2c1cffa2-20e9-40ba-a277-8cde4000c177");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8306e7d0-3e16-4645-9971-e157102a7a43");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d0aa1aa-f1b8-458e-a987-da14a951bb32", "AQAAAAEAACcQAAAAEA2+q9cCUOoeFByry88+GaK3uBsDCOoN330ZckqTD/c7QjUzMYDZdZ97rxHwd0xeWw==", "e9205fdd-3861-40a5-b226-5499f0d70c0f" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e98156ba-1418-44a9-b223-9a4f66a6b9c5", "AQAAAAEAACcQAAAAEICS1oIPyJXxADMyDF7CF0L0U1fCICB0YDD+21PcoYhIBtvXy6IHhboHNo77x4NCrQ==", "5c893db7-705d-4401-bf1d-6e3e9a29dbce" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81cc1420-7000-431a-bc52-46d2a3758a5b", "AQAAAAEAACcQAAAAENuUNUcdZcYuT+yYHBetlJ/oGSUCCxPrpIRDdHVOmA5feQt1ysz9aVzr0/5mxL12vg==", "9b122e83-044f-44d4-b525-11dcbc3e30b0" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a58a9db-f458-4a23-9acc-3a29c5e75b5e", "AQAAAAEAACcQAAAAEL2LXFSobVB/zFW11oDDzeHrne4wRbCiV3BeryKq2RVB3mU445qY8Q3tf203qoIgzw==", "350f3fcf-9412-4f78-a94b-0bd66bfdd364" });

            migrationBuilder.CreateIndex(
                name: "IX_medium_in_album_medium_id",
                table: "medium_in_album",
                column: "medium_id");

            migrationBuilder.AddForeignKey(
                name: "player_user",
                table: "player",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "player_user",
                table: "player");

            migrationBuilder.DropTable(
                name: "medium_in_album");

            migrationBuilder.DropColumn(
                name: "summary",
                table: "tournament");

            migrationBuilder.RenameIndex(
                name: "team_in_tournament_tournament",
                table: "team_in_tournament",
                newName: "Table_27_tournament");

            migrationBuilder.RenameIndex(
                name: "team_in_group_team",
                table: "team_in_group",
                newName: "Table_25_team");

            migrationBuilder.RenameIndex(
                name: "team_in_group_group",
                table: "team_in_group",
                newName: "team_group");

            migrationBuilder.RenameIndex(
                name: "newsletter_user",
                table: "newsletter",
                newName: "Table_28_user");

            migrationBuilder.Sql(@"ALTER TABLE `match` CHANGE `tournament_id` `TournamentId` INT NOT NULL DEFAULT '0', CHANGE `phase` `Phase` INT NOT NULL DEFAULT '0';");
            
            /*migrationBuilder.RenameColumn(
                name: "phase",
                table: "match",
                newName: "Phase");

            migrationBuilder.RenameColumn(
                name: "tournament_id",
                table: "match",
                newName: "TournamentId");*/

            migrationBuilder.RenameIndex(
                name: "IX_match_team_away_id",
                table: "match",
                newName: "match_away");

            migrationBuilder.AlterColumn<int>(
                name: "captain_id",
                table: "team",
                type: "int",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "album_id",
                table: "media",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "contestants",
                type: "int",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "create_date",
                table: "album",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c5a9253e-7aa6-4a7c-a5de-8ab1e2f1b677");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9e208d4d-4f32-4ed0-852f-085dc6ee131b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fffcba93-c189-467e-8a16-b7ed64c6b162");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8206e821-91de-4453-9755-a37337670e76", "AQAAAAEAACcQAAAAEDp/4ynVO4J2laMYvin/k9uh0DtL4p+dyjOmdVWXq8DCxOQKPHvbM0qkdUWsWHpKGQ==", "2ac2781e-f831-4411-86cc-03432c5996ed" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afbb79ee-2b57-41be-b230-6ceaa518bb9c", "AQAAAAEAACcQAAAAENP7y3WiPATkr41TsRvttfHHm2jJPYTkVDKwuxy6IhAwCJT22JRGnxhxrqPbpglw4Q==", "bf2b009a-d8ba-46da-9ffc-fc4d611b3283" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b101608b-a23c-434b-bcd9-a9d3e4e9a534", "AQAAAAEAACcQAAAAEHNIj0mjaPfbShkz0vDai18f1PBAIMxP0v+rDl9FM0rAH4evWiNdI/SFF3udr9V/wg==", "016a167d-4c9d-48c8-b93f-60d41fa537ed" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58fb9b0f-dd72-4e53-9039-0fd457fe41de", "AQAAAAEAACcQAAAAEOQN7ShPU/n2o5gRmvWN3QRmF2y7sO0em2YKPUBs7M0I01/vcg0PzSKR0CfPbQKBWA==", "22e5d7f8-be85-4604-b697-fb67efcea88b" });

            migrationBuilder.CreateIndex(
                name: "media_album",
                table: "media",
                column: "album_id");

            migrationBuilder.AddForeignKey(
                name: "media_album",
                table: "media",
                column: "album_id",
                principalTable: "album",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "player_user",
                table: "player",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
