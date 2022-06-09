using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class cascademedium : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "medium_in_album_media",
                table: "medium_in_album");

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
                column: "ConcurrencyStamp",
                value: "45002e1d-418a-4dc1-840b-68d767fed5c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d43be1b3-9280-45d8-90fe-2e53dd2e27ab");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "560a3f35-e713-4add-ab35-0ce110084019", "AQAAAAEAACcQAAAAECbR8ytlp4fkTjLT3DewmM6fgp9lKtpPBWeU0JUCzgWefJ2HN9b+/THuZkFbDB3bPQ==", "7144099d-973e-4218-ab7b-1e2337d7e07f" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e70088d5-d96e-46e1-a0d1-8266bd856d43", "AQAAAAEAACcQAAAAEN7CbGm7uwYmPfdkZaccKSVSvnJ26wpmTBNIXagVZ3sqqVyJY37QBlFUgGqBURUVVQ==", "aeb08f7c-ea7c-44af-94fb-97d27c14b3f3" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28589815-baac-4315-b14a-a2e16c3ef2c8", "AQAAAAEAACcQAAAAEECRBxzkwK5SFXBxkV3EEnlpz1aDvVk1MqPCy2AggZPauVTBsSMgmYH9cftmqc87lw==", "09413795-d586-4346-ae60-7f1c4fc2e82e" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a072d4b7-4b09-4533-9667-269b9c0216b2", "AQAAAAEAACcQAAAAEEIXM2KpMVzcFVrp43ho35lAziVzN1YihgNOHVL5HsV7xAQwOQYc6J2hP0mV1bUwCA==", "0acabab7-1a18-428b-a180-0c0b1536a59f" });

            migrationBuilder.AddForeignKey(
                name: "medium_in_album_media",
                table: "medium_in_album",
                column: "medium_id",
                principalTable: "media",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "medium_in_album_media",
                table: "medium_in_album");

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

            migrationBuilder.AddForeignKey(
                name: "medium_in_album_media",
                table: "medium_in_album",
                column: "medium_id",
                principalTable: "media",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
