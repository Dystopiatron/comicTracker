using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddWishlistFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SeriesName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IssueNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DesiredCondition = table.Column<string>(type: "text", nullable: true),
                    TargetPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16e830a7-8979-4ea8-957e-949cac4c9f28", new DateTime(2025, 10, 20, 18, 13, 23, 51, DateTimeKind.Utc).AddTicks(3150), "AQAAAAIAAYagAAAAEMcwsASL6nd+tGWLFU5D0PMUZRmY9rfsCW5lz4LH3v4iMkNv8scpL7s2aJpQ18LXjA==", "6827ef9d-d4af-4e3c-9c7c-7570698309e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37efb8cc-0d8d-468c-92b4-16fdc7550056", new DateTime(2025, 10, 20, 18, 13, 23, 84, DateTimeKind.Utc).AddTicks(7000), "AQAAAAIAAYagAAAAEJbnWOujkJpkc2bvbUyKoig/ojsgjVVloxAHskJ92jsvT7g0SrBzRqBDUSiBZWRBzQ==", "95996cc7-2369-46dc-a0e4-e59ca9c2de4c" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2025, 9, 20, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2025, 9, 25, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2025, 9, 30, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2025, 10, 5, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2025, 10, 10, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2025, 7, 12, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2025, 7, 17, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2025, 7, 22, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2025, 7, 27, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateTime(2025, 8, 1, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateAdded",
                value: new DateTime(2025, 8, 6, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateAdded",
                value: new DateTime(2025, 8, 11, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateAdded",
                value: new DateTime(2025, 8, 16, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateAdded",
                value: new DateTime(2025, 8, 21, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateAdded",
                value: new DateTime(2025, 8, 26, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateAdded",
                value: new DateTime(2025, 8, 31, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateAdded",
                value: new DateTime(2025, 9, 5, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateAdded",
                value: new DateTime(2025, 9, 10, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateAdded",
                value: new DateTime(2025, 9, 15, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateAdded",
                value: new DateTime(2025, 9, 20, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateAdded",
                value: new DateTime(2025, 9, 25, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateAdded",
                value: new DateTime(2025, 9, 30, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateAdded",
                value: new DateTime(2025, 10, 5, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateAdded",
                value: new DateTime(2025, 10, 10, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateAdded",
                value: new DateTime(2025, 10, 15, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateAdded",
                value: new DateTime(2025, 10, 16, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateAdded",
                value: new DateTime(2025, 10, 17, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateAdded",
                value: new DateTime(2025, 10, 18, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateAdded",
                value: new DateTime(2025, 10, 19, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateAdded",
                value: new DateTime(2025, 10, 20, 18, 13, 23, 117, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateAdded",
                value: new DateTime(2025, 10, 20, 19, 13, 23, 117, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateAdded",
                value: new DateTime(2025, 10, 20, 20, 13, 23, 117, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateAdded",
                value: new DateTime(2025, 10, 20, 21, 13, 23, 117, DateTimeKind.Utc).AddTicks(6720));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateAdded",
                value: new DateTime(2025, 10, 20, 22, 13, 23, 117, DateTimeKind.Utc).AddTicks(6720));

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_Priority",
                table: "WishlistItems",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_SeriesName",
                table: "WishlistItems",
                column: "SeriesName");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId",
                table: "WishlistItems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "419703ff-e4d8-490d-bcfe-010f320d2ae6", new DateTime(2025, 10, 17, 18, 23, 59, 888, DateTimeKind.Utc).AddTicks(8360), "AQAAAAIAAYagAAAAEOF9Cp3aHooy653pWoKGwRqGhq2sYImh+WvazWV4n6aqigW6BpfC8JMtixLLu07xDA==", "5080c7f9-a787-40f0-b445-3e67de5be4b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b8eeb08-6f5d-4568-922a-08085422b779", new DateTime(2025, 10, 17, 18, 23, 59, 920, DateTimeKind.Utc).AddTicks(9600), "AQAAAAIAAYagAAAAEHC8+FUaV05MyJkyw15HbZFdwFeeHd72pWSiIXAVO77gpf7NvZeO1NYYVsx2SMs/0w==", "d9b9becc-dfa2-4b51-9b5f-99b2dee5151e" });

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
        }
    }
}
