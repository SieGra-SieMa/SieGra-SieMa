using Microsoft.EntityFrameworkCore.Migrations;

namespace SieGraSieMa.Migrations
{
    public partial class IndexChanging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "team_in_tournament");

            migrationBuilder.DropIndex(
                name: "Id",
                table: "refresh_token");

            migrationBuilder.RenameIndex(
                name: "team_in_group_team",
                table: "team_in_group",
                newName: "IX_team_in_group_team_id");

            migrationBuilder.RenameIndex(
                name: "IX_medium_in_album_medium_id",
                table: "medium_in_album",
                newName: "medium_in_album_media");

            migrationBuilder.RenameIndex(
                name: "contestants_user",
                table: "contestants",
                newName: "IX_contestants_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "team_in_tournament",
                columns: new[] { "tournament_id", "team_id" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d94e3ce3-e2c6-4efc-929f-30ca48e3ad94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d03f2547-be3e-4a7a-9e8e-05032b6dc4b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "39bb7555-264c-4d5f-93cd-75c25e78fe92");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fca72cee-e4ff-4230-a9cf-f82800665db2", "AQAAAAEAACcQAAAAEOdyW+tYyXyGxuA19vVkI1agUPfVaEMBr4zOZbGvtedIQ0VeRx/bqXpbIn2utZ5SBg==", "cd8d7598-ff96-41e3-ac62-ab3b69e624fb" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8df7d149-dbc7-4972-84b3-e26c60d7cc9b", "AQAAAAEAACcQAAAAEFFwXDsUzw1ZSLr4L/qtONz9HQM/6oi3YCt8PjgzD4xUgxWOcvk+tl9YMJ507Yi5jg==", "356f4603-87b1-4d31-8d00-84a01be58b5f" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d754d25c-fbbd-43e3-99ad-5086fd5b4965", "AQAAAAEAACcQAAAAEMUTiVFbxJ1ivnVs9PKe5KsAB2H9mhjYvAzxGWu5wBHLWiawVIBNmzld8HSHkhBRzA==", "1e6f7245-b131-453e-b640-d00493c5f9bf" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c05b549-2934-43f2-9091-5840ae7f1578", "AQAAAAEAACcQAAAAEBhW5RQc/T2bbR2cr/L2dlG8RtrKrFXbBLsid9bBSMj4v+MGmjzmKyVjbvOvMKTRUw==", "4bb18fca-bad6-4630-843b-950d8e40e0bb" });

            migrationBuilder.CreateIndex(
                name: "IX_team_in_tournament_team_id",
                table: "team_in_tournament",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "contestants_contest",
                table: "contestants",
                column: "contest_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "team_in_tournament");

            migrationBuilder.DropIndex(
                name: "IX_team_in_tournament_team_id",
                table: "team_in_tournament");

            migrationBuilder.DropIndex(
                name: "contestants_contest",
                table: "contestants");

            migrationBuilder.RenameIndex(
                name: "IX_team_in_group_team_id",
                table: "team_in_group",
                newName: "team_in_group_team");

            migrationBuilder.RenameIndex(
                name: "medium_in_album_media",
                table: "medium_in_album",
                newName: "IX_medium_in_album_medium_id");

            migrationBuilder.RenameIndex(
                name: "IX_contestants_user_id",
                table: "contestants",
                newName: "contestants_user");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "team_in_tournament",
                columns: new[] { "team_id", "tournament_id" });

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
                name: "Id",
                table: "refresh_token",
                column: "id",
                unique: true);
        }
    }
}
