using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndAuth.Persistance.Migrations
{
    public partial class migrateToDateTimeOffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9a7ccc32-d4e6-4c61-8b66-a841e3cc1f77", "4bb0cdc6-2113-4efa-8dd0-2fbd2ced42fb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a7ccc32-d4e6-4c61-8b66-a841e3cc1f77");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4bb0cdc6-2113-4efa-8dd0-2fbd2ced42fb");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Expires",
                table: "RefreshTokens",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "RefreshTokens",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "978e4744-ffe0-4b3a-bdcd-e805ecb64359", "978e4744-ffe0-4b3a-bdcd-e805ecb64359", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1362245b-0494-47ea-abae-912890c0cb46", 0, "2dbde0f4-7034-45d4-be45-0931cad01460", "ApplicationUser", "DefaultAdmin@default.com", true, false, null, "DEFAULTADMIN@DEFAULT.COM", "DEFAULTADMIN", "AQAAAAEAACcQAAAAENS0kOn/fF37zQHt+3fQgYGN9TW5NgHSNcm+XPUVw+/pueDucZHjqiJQReQ0Y1tXRg==", null, false, "71af7020-bb99-48f4-8a1c-c7462596bfc2", false, "DefaultAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "978e4744-ffe0-4b3a-bdcd-e805ecb64359", "1362245b-0494-47ea-abae-912890c0cb46" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "978e4744-ffe0-4b3a-bdcd-e805ecb64359", "1362245b-0494-47ea-abae-912890c0cb46" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "978e4744-ffe0-4b3a-bdcd-e805ecb64359");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1362245b-0494-47ea-abae-912890c0cb46");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a7ccc32-d4e6-4c61-8b66-a841e3cc1f77", "9a7ccc32-d4e6-4c61-8b66-a841e3cc1f77", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4bb0cdc6-2113-4efa-8dd0-2fbd2ced42fb", 0, "31286c80-7173-43cd-8760-b08cb697a52d", "ApplicationUser", "DefaultAdmin@default.com", true, false, null, "DEFAULTADMIN@DEFAULT.COM", "DEFAULTADMIN", "AQAAAAEAACcQAAAAEHPdLbFVuCzflHAYX8l6cHglhfBr/55hBHnBABRENL/YP4lnEOftBl+SmFZqdSW8+g==", null, false, "3402184d-2a88-4ec6-a801-8227471613ba", false, "DefaultAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9a7ccc32-d4e6-4c61-8b66-a841e3cc1f77", "4bb0cdc6-2113-4efa-8dd0-2fbd2ced42fb" });
        }
    }
}
