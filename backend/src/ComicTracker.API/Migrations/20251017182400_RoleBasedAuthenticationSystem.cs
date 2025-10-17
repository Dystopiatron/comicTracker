using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class RoleBasedAuthenticationSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedReason = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "419703ff-e4d8-490d-bcfe-010f320d2ae6", new DateTime(2025, 10, 17, 18, 23, 59, 888, DateTimeKind.Utc).AddTicks(8360), "AQAAAAIAAYagAAAAEOF9Cp3aHooy653pWoKGwRqGhq2sYImh+WvazWV4n6aqigW6BpfC8JMtixLLu07xDA==", "User", "5080c7f9-a787-40f0-b445-3e67de5be4b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "3b8eeb08-6f5d-4568-922a-08085422b779", new DateTime(2025, 10, 17, 18, 23, 59, 920, DateTimeKind.Utc).AddTicks(9600), "AQAAAAIAAYagAAAAEHC8+FUaV05MyJkyw15HbZFdwFeeHd72pWSiIXAVO77gpf7NvZeO1NYYVsx2SMs/0w==", "Admin", "d9b9becc-dfa2-4b51-9b5f-99b2dee5151e" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2025, 9, 17, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2025, 9, 22, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2025, 9, 27, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2140));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2025, 10, 2, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2140));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2025, 10, 7, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2140));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2025, 7, 9, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2140));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2025, 7, 14, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2025, 7, 19, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2025, 7, 24, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateTime(2025, 7, 29, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateAdded",
                value: new DateTime(2025, 8, 3, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateAdded",
                value: new DateTime(2025, 8, 8, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateAdded",
                value: new DateTime(2025, 8, 13, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateAdded",
                value: new DateTime(2025, 8, 18, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateAdded",
                value: new DateTime(2025, 8, 23, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateAdded",
                value: new DateTime(2025, 8, 28, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateAdded",
                value: new DateTime(2025, 9, 2, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateAdded",
                value: new DateTime(2025, 9, 7, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateAdded",
                value: new DateTime(2025, 9, 12, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateAdded",
                value: new DateTime(2025, 9, 17, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateAdded",
                value: new DateTime(2025, 9, 22, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateAdded",
                value: new DateTime(2025, 9, 27, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateAdded",
                value: new DateTime(2025, 10, 2, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateAdded",
                value: new DateTime(2025, 10, 7, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateAdded",
                value: new DateTime(2025, 10, 12, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateAdded",
                value: new DateTime(2025, 10, 14, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateAdded",
                value: new DateTime(2025, 10, 15, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateAdded",
                value: new DateTime(2025, 10, 16, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateAdded",
                value: new DateTime(2025, 10, 17, 18, 23, 59, 954, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateAdded",
                value: new DateTime(2025, 10, 17, 19, 23, 59, 954, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateAdded",
                value: new DateTime(2025, 10, 17, 20, 23, 59, 954, DateTimeKind.Utc).AddTicks(2310));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateAdded",
                value: new DateTime(2025, 10, 17, 21, 23, 59, 954, DateTimeKind.Utc).AddTicks(2310));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateAdded",
                value: new DateTime(2025, 10, 17, 22, 23, 59, 954, DateTimeKind.Utc).AddTicks(2310));

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId_IsRevoked",
                table: "RefreshTokens",
                columns: new[] { "UserId", "IsRevoked" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsAdmin", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fa83748-5939-415f-9a09-203360e43f74", new DateTime(2025, 10, 15, 18, 10, 55, 725, DateTimeKind.Utc).AddTicks(8470), false, "AQAAAAIAAYagAAAAEBboDrFKqqKAID9Xb7ARRDmAVVIu2narJCMFsXVMY2b99r0xHXNJlogMeaPjXFzHyg==", "7a5434c0-1c23-439c-85f1-24a001c35a59" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsAdmin", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b803ce2c-6ca4-4a64-b6f4-0bbc80cf65e9", new DateTime(2025, 10, 15, 18, 10, 55, 757, DateTimeKind.Utc).AddTicks(4000), false, "AQAAAAIAAYagAAAAEP+guLaiu3wswHgs/0eeW50jai/kWotsYzR83GAsqeepaUUi/5/LJVQ7SHyrxd7pBA==", "05877fe9-48d9-41ea-af5a-fc347a820c5e" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2025, 9, 15, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8170));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2025, 9, 20, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2025, 9, 25, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2025, 9, 30, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2025, 10, 5, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2025, 7, 7, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2025, 7, 12, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2025, 7, 17, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2025, 7, 22, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8200));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateTime(2025, 7, 27, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8200));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateAdded",
                value: new DateTime(2025, 8, 1, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateAdded",
                value: new DateTime(2025, 8, 6, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateAdded",
                value: new DateTime(2025, 8, 11, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateAdded",
                value: new DateTime(2025, 8, 16, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateAdded",
                value: new DateTime(2025, 8, 21, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateAdded",
                value: new DateTime(2025, 8, 26, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateAdded",
                value: new DateTime(2025, 8, 31, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateAdded",
                value: new DateTime(2025, 9, 5, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateAdded",
                value: new DateTime(2025, 9, 10, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateAdded",
                value: new DateTime(2025, 9, 15, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateAdded",
                value: new DateTime(2025, 9, 20, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateAdded",
                value: new DateTime(2025, 9, 25, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateAdded",
                value: new DateTime(2025, 9, 30, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateAdded",
                value: new DateTime(2025, 10, 5, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateAdded",
                value: new DateTime(2025, 10, 10, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateAdded",
                value: new DateTime(2025, 10, 11, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateAdded",
                value: new DateTime(2025, 10, 12, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateAdded",
                value: new DateTime(2025, 10, 14, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateAdded",
                value: new DateTime(2025, 10, 15, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateAdded",
                value: new DateTime(2025, 10, 15, 19, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateAdded",
                value: new DateTime(2025, 10, 15, 20, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateAdded",
                value: new DateTime(2025, 10, 15, 21, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateAdded",
                value: new DateTime(2025, 10, 15, 22, 10, 55, 788, DateTimeKind.Utc).AddTicks(8300));
        }
    }
}
