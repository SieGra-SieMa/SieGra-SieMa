using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SieGraSieMa.Migrations
{
    public partial class AddTwoMoreAttributesInMatchToBeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropPrimaryKey(
                name: "PK_match",
                table: "match");*/

            /*migrationBuilder.RenameColumn(
                name: "id",
                table: "match",
                newName: "match_id");
            */
            migrationBuilder.Sql(@"ALTER TABLE `match` CHANGE `id` `match_id` INT NOT NULL AUTO_INCREMENT;");

            migrationBuilder.AlterColumn<int>(
                name: "match_id",
                table: "match",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Phase",
                table: "match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"ALTER TABLE `match` DROP PRIMARY KEY, ADD PRIMARY KEY(`match_id`,`TournamentId`,`Phase`);");


            /*migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "match",
                columns: new[] { "TournamentId", "Phase", "match_id" });*/

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7adda3ad-444d-4b7d-aa42-1c9108963578");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "951819df-b7ee-4042-add7-0c330ac381b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e8b16a4b-4240-4028-9503-e59036bdfade");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25c96dca-7c9b-4e50-a1ea-dbf60e3b600c", "AQAAAAEAACcQAAAAENL9nThV0Zlq/ESgjRKKt08IWVzQIXRuCjZeNIRe5frXPUSUgeBKDcQ6xZ81bABTrw==", "e845e3e5-1518-46ea-a817-593464778588" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "700dce6f-af95-4df2-86f7-09e23fad2ef7", "AQAAAAEAACcQAAAAEJrZ2QzG1HxvtJICtXcutunOqI3iiDo9cZaTs3aEjGDOsyytHrxxEitVZRLYEvjbJA==", "52c62210-e897-4611-8a31-b79998d3a82b" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e36ec28-c099-4625-86d4-24f3b739d088", "AQAAAAEAACcQAAAAEC00oEdkoXNLts6ELSpqPZcmfKW73Bm13nBy7sZ9/dzFh2yrETyEJ2DSkVi7rDHPqQ==", "11918afe-cdeb-46eb-a8f5-29efdfefe712" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5439c69-dc97-4db7-a74b-7390dc0f4a15", "AQAAAAEAACcQAAAAELCpsk4c4TmMLYXfxAFwXgnJl/jFyLqPpUsi1Yiu9ur454zMNdQtRNECgLlQyP0GtA==", "43fcfc51-8036-4942-9806-07d074084474" });

            migrationBuilder.AddForeignKey(
                name: "tournament",
                table: "match",
                column: "TournamentId",
                principalTable: "tournament",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "tournament",
                table: "match");

            /*migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "match");*/

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "match");

            migrationBuilder.DropColumn(
                name: "Phase",
                table: "match");

            /*migrationBuilder.RenameColumn(
                name: "match_id",
                table: "match",
                newName: "id");*/

            migrationBuilder.Sql(@"ALTER TABLE `match` CHANGE `match_id` `id` INT NOT NULL AUTO_INCREMENT;");

            migrationBuilder.Sql(@"ALTER TABLE `match` DROP PRIMARY KEY, ADD PRIMARY KEY(`id`);");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "match",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_match",
                table: "match",
                column: "id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f549d8fa-2cfa-4e63-8ea8-ce3364853c16");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e391dd02-8088-4966-b5d0-dd045d33a956");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "19712254-d755-4df3-ad82-49fec4345003");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc91146b-851a-4504-a646-fc8e51b1447e", "AQAAAAEAACcQAAAAEMHbG20DStwYORajE6gTnmmHtH9+YI2YQJDnB6+NKh3I/2r4NuRscEyc4Ir01jEl7w==", "49496d3a-1241-4d5c-9026-4e3b8c006d09" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f849bf9f-bd2c-4b62-a450-ea8f07201522", "AQAAAAEAACcQAAAAEDsHoVArIC2hzzd7tH0Z60eMobXxYfTjYdaiOaKt2FHwrunBUzzNv3m/DqDUlz2ifQ==", "7a4de017-3129-4b02-b2e0-7af1afdac04f" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c09666b3-21f2-44f3-b626-2d2e47c4a31b", "AQAAAAEAACcQAAAAEEy1RrRP8IuK+F0i1R/TJiDBr9DPyId/qfJeDVORU5u5/8bbAfVebMLaCFVq6YwzEw==", "2678df16-fcd8-46d4-aefa-d644aff7e94b" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48c361cc-763c-49d7-952b-51221c3def18", "AQAAAAEAACcQAAAAEAYlelwpLiQ+Fvn2q13d0yglnNsr7e7j/TKqzI2zH+hzfhiyvpb+lgglZ//LbY60ng==", "028a25a2-ea63-4a26-ac2c-8b5c944a90f8" });
        }
    }
}
