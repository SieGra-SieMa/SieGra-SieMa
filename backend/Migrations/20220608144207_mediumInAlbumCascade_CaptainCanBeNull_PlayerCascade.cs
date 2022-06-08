using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class mediumInAlbumCascade_CaptainCanBeNull_PlayerCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "contestants_user",
                table: "contestants");

            migrationBuilder.DropForeignKey(
                name: "logs_user",
                table: "logs");

            migrationBuilder.DropForeignKey(
                name: "medium_in_album_album",
                table: "medium_in_album");

            migrationBuilder.DropForeignKey(
                name: "newsletter_user",
                table: "newsletter");

            migrationBuilder.DropForeignKey(
                name: "player_user",
                table: "player");

            migrationBuilder.DropForeignKey(
                name: "refresh_token_user",
                table: "refresh_token");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 4 });

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
                table: "tournament",
                keyColumn: "id",
                keyValue: 1);

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

            migrationBuilder.AlterColumn<int>(
                name: "captain_id",
                table: "team",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "0032aa79-cfc9-4549-bb44-b99e013a909f", "Employee" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "39f79fc7-c19b-4b9b-b1b4-0b2ae59e9c74");

            migrationBuilder.AddForeignKey(
                name: "contestants_user",
                table: "contestants",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "logs_user",
                table: "logs",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "medium_in_album_album",
                table: "medium_in_album",
                column: "album_id",
                principalTable: "album",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "newsletter_user",
                table: "newsletter",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "player_user",
                table: "player",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "refresh_token_user",
                table: "refresh_token",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "contestants_user",
                table: "contestants");

            migrationBuilder.DropForeignKey(
                name: "logs_user",
                table: "logs");

            migrationBuilder.DropForeignKey(
                name: "medium_in_album_album",
                table: "medium_in_album");

            migrationBuilder.DropForeignKey(
                name: "newsletter_user",
                table: "newsletter");

            migrationBuilder.DropForeignKey(
                name: "player_user",
                table: "player");

            migrationBuilder.DropForeignKey(
                name: "refresh_token_user",
                table: "refresh_token");

            migrationBuilder.AlterColumn<int>(
                name: "captain_id",
                table: "team",
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
                value: "cd59806b-6f6a-4faa-8673-bde78fc7b4c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "45002e1d-418a-4dc1-840b-68d767fed5c6", "Emp" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d43be1b3-9280-45d8-90fe-2e53dd2e27ab");

            migrationBuilder.InsertData(
                table: "tournament",
                columns: new[] { "id", "address", "description", "end_date", "MediumId", "name", "start_date" },
                values: new object[] { 1, "Zbożowa -1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Turniej testowy numer 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "AccessFailedCount", "ConcurrencyStamp", "email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "560a3f35-e713-4add-ab35-0ce110084019", "admin@gmail.com", true, false, null, "Adm", "admin@gmail.com", null, "AQAAAAEAACcQAAAAECbR8ytlp4fkTjLT3DewmM6fgp9lKtpPBWeU0JUCzgWefJ2HN9b+/THuZkFbDB3bPQ==", null, false, "7144099d-973e-4218-ab7b-1e2337d7e07f", "In", false, null },
                    { 2, 0, "e70088d5-d96e-46e1-a0d1-8266bd856d43", "pracownik@gmail.com", true, false, null, "Prac", "pracownik@gmail.com", null, "AQAAAAEAACcQAAAAEN7CbGm7uwYmPfdkZaccKSVSvnJ26wpmTBNIXagVZ3sqqVyJY37QBlFUgGqBURUVVQ==", null, false, "aeb08f7c-ea7c-44af-94fb-97d27c14b3f3", "Ownik", false, null },
                    { 3, 0, "28589815-baac-4315-b14a-a2e16c3ef2c8", "kapitan@gmail.com", true, false, null, "Kap", "kapitan@gmail.com", null, "AQAAAAEAACcQAAAAEECRBxzkwK5SFXBxkV3EEnlpz1aDvVk1MqPCy2AggZPauVTBsSMgmYH9cftmqc87lw==", null, false, "09413795-d586-4346-ae60-7f1c4fc2e82e", "Itan", false, null },
                    { 4, 0, "a072d4b7-4b09-4533-9667-269b9c0216b2", "gracz@gmail.com", true, false, null, "Gr", "gracz@gmail.com", null, "AQAAAAEAACcQAAAAEEIXM2KpMVzcFVrp43ho35lAziVzN1YihgNOHVL5HsV7xAQwOQYc6J2hP0mV1bUwCA==", null, false, "0acabab7-1a18-428b-a180-0c0b1536a59f", "acz", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "newsletter",
                columns: new[] { "id", "user_id" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "team",
                columns: new[] { "id", "captain_id", "code", "MediumId", "name" },
                values: new object[,]
                {
                    { 1, 3, "ABCDE", null, "Bogowie" },
                    { 2, 3, "EDCBA", null, "Demony" }
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

            migrationBuilder.AddForeignKey(
                name: "contestants_user",
                table: "contestants",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "logs_user",
                table: "logs",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "medium_in_album_album",
                table: "medium_in_album",
                column: "album_id",
                principalTable: "album",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "newsletter_user",
                table: "newsletter",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "player_user",
                table: "player",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "refresh_token_user",
                table: "refresh_token",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
