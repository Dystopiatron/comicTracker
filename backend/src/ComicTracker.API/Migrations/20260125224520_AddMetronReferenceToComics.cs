using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddMetronReferenceToComics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetronId",
                table: "Comics",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MetronLastSync",
                table: "Comics",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68fc4e69-495f-4d8b-8c34-e031328eb7f8", new DateTime(2026, 1, 25, 22, 45, 20, 220, DateTimeKind.Utc).AddTicks(7400), "AQAAAAIAAYagAAAAEBGWcRJzE/M+o29oYV+4FsPzvLP3dmgGjTMozYwXlmuFC377+a3dx6NL1aaFzZeDrw==", "e59d0529-af07-426d-87ec-10e2250921c1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11a65192-39c4-4518-842e-28a941de42bf", new DateTime(2026, 1, 25, 22, 45, 20, 250, DateTimeKind.Utc).AddTicks(6800), "AQAAAAIAAYagAAAAEH28yw40mp83rCkNuiaWe2Gv3uUzFxD3uwaNKMBnqPBb9h08Lk5QAdfg58u1FWT7lQ==", "8702b657-953d-47a3-b306-ec1b41891bd6" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 26, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8420), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 31, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 5, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 10, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 15, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 10, 22, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 10, 27, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 11, 1, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 11, 6, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 11, 11, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 11, 16, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 11, 21, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 11, 26, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 1, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 6, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 11, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8460), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 21, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8460), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 26, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8460), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2025, 12, 31, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 5, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 10, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 15, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 20, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 21, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 22, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 23, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 24, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 25, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 25, 23, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 26, 0, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 26, 1, 45, 20, 281, DateTimeKind.Utc).AddTicks(8490), null, null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateAdded", "MetronId", "MetronLastSync" },
                values: new object[] { new DateTime(2026, 1, 26, 2, 45, 20, 281, DateTimeKind.Utc).AddTicks(8490), null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetronId",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "MetronLastSync",
                table: "Comics");

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
        }
    }
}
