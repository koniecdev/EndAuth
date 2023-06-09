using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndAuth.Persistance.Migrations
{
    public partial class MigrateOverDefaultAspNetCoreIdentityPasswordReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1362245b-0494-47ea-abae-912890c0cb46",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdfecae8-6a3d-4838-9842-ebf3be00a1db", "AQAAAAEAACcQAAAAEOhiqX9QmXnjddUFkDYla7BWkAMp1ErkGbfgDgqlW3j8UsYr4KWl+/T91PUDF1BeAw==", "16e440dd-4f98-462b-b545-e0ef9823abf8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    ValidUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1362245b-0494-47ea-abae-912890c0cb46",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "adbc1d95-ef05-4e43-8d16-39f06721b509", "AQAAAAEAACcQAAAAENwNswk1q7EP/QsnmXiHA8CLk8iLgCPHlePB9Zqsya8FB+1/Xxou7cLjZAHk6IMABw==", "1bfb4dea-d070-4ba9-9516-9aa8109def71" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_ApplicationUserId",
                table: "PasswordResetTokens",
                column: "ApplicationUserId");
        }
    }
}
