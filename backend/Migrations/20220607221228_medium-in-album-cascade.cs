using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class mediuminalbumcascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "medium_in_album_album",
                table: "medium_in_album");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bb4aa009-8b88-4a00-9bd6-847cafb58771");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "54d6849b-fd43-4302-8ca5-029faafadde3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f033e1bb-0e70-4df7-ab53-7f81de659848");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73b1506c-0a33-4676-8627-4d3eb5d82bff", "AQAAAAEAACcQAAAAEK5Zl5cg4FIxxZERuCIfVKZ8MTcyk9tJIZVuBJixE/fauFXwS1fzoHIOpFxD64fHwg==", "bde4ab26-3a4a-40ab-b605-371e33e6547a" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a096b27b-3fb8-4f72-b795-719b031f4c5c", "AQAAAAEAACcQAAAAEG4IfRgZ2efAhcGcgW2KhDSBWiOE71CsH/rXdPeMyCks4sAAY5mv+NqWMK9l1F9AJw==", "ad953819-12ac-415a-ad36-865e01a4af9f" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d7d666d-a6ea-4823-81c8-9160c4b87d31", "AQAAAAEAACcQAAAAEJKe9l5IPESXa63Ux83MX8wLgFE9jjoUGdY0Ptnfm/qKsppBGNEsrhoJhXaiW4QL6A==", "39a7bf44-5076-48de-9be4-7e4687f1c0c6" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c6d2db8-3d33-4492-bcf4-efd6ee86cd1a", "AQAAAAEAACcQAAAAEMb4y9OXUJoNFv6iWCGtJ3uIEbdya5AOe2F/7J0CvuzO0kVr3Vwz8GWi+kuAPccGQw==", "be07176a-c1c2-4748-88bd-efd7a8a037c7" });

            migrationBuilder.AddForeignKey(
                name: "medium_in_album_album",
                table: "medium_in_album",
                column: "album_id",
                principalTable: "album",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "medium_in_album_album",
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
                name: "medium_in_album_album",
                table: "medium_in_album",
                column: "album_id",
                principalTable: "album",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
