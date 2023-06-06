using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndAuth.Persistance.Migrations
{
    public partial class AddPasswordResetTokenToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValidUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1362245b-0494-47ea-abae-912890c0cb46",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2dbde0f4-7034-45d4-be45-0931cad01460", "AQAAAAEAACcQAAAAENS0kOn/fF37zQHt+3fQgYGN9TW5NgHSNcm+XPUVw+/pueDucZHjqiJQReQ0Y1tXRg==", "71af7020-bb99-48f4-8a1c-c7462596bfc2" });
        }
    }
}
