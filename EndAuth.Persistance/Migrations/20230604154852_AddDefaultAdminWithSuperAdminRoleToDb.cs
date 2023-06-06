﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndAuth.Persistance.Migrations
{
    public partial class AddDefaultAdminWithSuperAdminRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af39643c-25f5-4430-89b1-3d53548aac53", "af39643c-25f5-4430-89b1-3d53548aac53", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "07e3137a-1bf3-4724-9c68-efd6d6cf867a", 0, "f3efbb78-83b4-4327-9ee8-f7a3134cabef", "ApplicationUser", "DefaultAdmin@default.com", true, false, null, null, "DEFAULTADMIN", "AQAAAAEAACcQAAAAENeY0lTh6TWPHXpdc1+abfqZeyPzR7+j8pd+uQx/LFGvtXOETIEnWTdgml4VpMm+UA==", null, false, "f29ee80d-08a5-43e6-9656-e791c3b4404b", false, "DefaultAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "af39643c-25f5-4430-89b1-3d53548aac53", "07e3137a-1bf3-4724-9c68-efd6d6cf867a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af39643c-25f5-4430-89b1-3d53548aac53", "07e3137a-1bf3-4724-9c68-efd6d6cf867a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af39643c-25f5-4430-89b1-3d53548aac53");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "07e3137a-1bf3-4724-9c68-efd6d6cf867a");
        }
    }
}
