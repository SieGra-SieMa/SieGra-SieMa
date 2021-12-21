using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class identityv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_user_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dfc5819b-6258-4a9b-a175-32aec88eb2d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7ec758ea-29bd-4a82-9843-a7928a0181a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2fa99d7b-d5b3-47bf-8e5f-ee4a85d53607");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "7c626a73-7849-4a30-bc7c-447204b9c417", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "e7a2c73b-2a20-4295-b50d-3c4037575add", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "556ae830-8601-4a78-a0b5-d874db1f3194", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "f94a6a73-62e3-43ba-b15c-8f6b21c0119f", "8s/huq0Ozwzx8ZwP95f6VQI+l/WiplA/4dSW52dhWs8=", "OIrrEZPEdZ4JQ+84ziThv8MieV8/P2qJu9c5fK8lizs=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "AspNetUserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4dc29c1c-eabe-44e5-b76c-c926bc252ffb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1e830814-7957-49f6-88be-ba3040e7aa1d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5246f887-9de1-453f-bb61-058385558bae");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "2f31b0de-af21-4b08-a931-490d9b8f5e8c", "4o1yeW5A7w2hSrYUM48Y543cZgdy3w08cmif6gWW3gM=", "gBRAEc6WXbxx17Ce60OHRHCb2fb+dd9/GUfwsGoXlQg=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "72c6c412-7e1a-4350-8fdb-cb5ca2e088ce", "4o1yeW5A7w2hSrYUM48Y543cZgdy3w08cmif6gWW3gM=", "gBRAEc6WXbxx17Ce60OHRHCb2fb+dd9/GUfwsGoXlQg=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "6fdd79ca-a661-4ae3-8c14-acfc4dcfdc50", "4o1yeW5A7w2hSrYUM48Y543cZgdy3w08cmif6gWW3gM=", "gBRAEc6WXbxx17Ce60OHRHCb2fb+dd9/GUfwsGoXlQg=" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "password", "salt" },
                values: new object[] { "182b4316-ac52-485d-8573-6b200c6a367f", "4o1yeW5A7w2hSrYUM48Y543cZgdy3w08cmif6gWW3gM=", "gBRAEc6WXbxx17Ce60OHRHCb2fb+dd9/GUfwsGoXlQg=" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_user_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
