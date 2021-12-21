using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SieGraSieMa.Migrations
{
    public partial class identityv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "AspNetUserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "4dc29c1c-eabe-44e5-b76c-c926bc252ffb", "Admin", null },
                    { 2, "1e830814-7957-49f6-88be-ba3040e7aa1d", "Emp", null },
                    { 3, "5246f887-9de1-453f-bb61-058385558bae", "User", null }
                });

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "UserId1" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 2, 2, null },
                    { 3, 2, null },
                    { 3, 3, null },
                    { 3, 4, null }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_user_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles");

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "user_role_role",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_role_user",
                        column: x => x.user_id,
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

            migrationBuilder.CreateIndex(
                name: "Table_26_role",
                table: "user_roles",
                column: "role_id");
        }
    }
}
