using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class RenameMetronToComicVine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetronId",
                table: "Comics");

            migrationBuilder.RenameColumn(
                name: "MetronLastSync",
                table: "Comics",
                newName: "ComicVineLastSync");

            migrationBuilder.AddColumn<int>(
                name: "ComicVineId",
                table: "Comics",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "308782e0-7a7b-4738-bf32-fc910bd60e70", new DateTime(2026, 1, 25, 23, 15, 37, 799, DateTimeKind.Utc).AddTicks(180), "AQAAAAIAAYagAAAAEPEmn7W8zvqRB391PEKZcYOEDNRfNmN+RbJhwHXrFQf/B1Ybf/XuarmObii0HRAG9Q==", "439496a9-b20e-4e3a-9e74-a89b7dc98811" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56d2134d-103b-445d-87e1-220b37935d21", new DateTime(2026, 1, 25, 23, 15, 37, 831, DateTimeKind.Utc).AddTicks(4820), "AQAAAAIAAYagAAAAEIuNgeOwoHKnlVFYwZXLnMTSpQKyUfa4h7o6QOuHobWQ8JSMdebmL1+6tlOhwkjHuw==", "580163a0-711b-4db9-8276-3566fb3e8e91" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 26, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(680) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 31, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(680) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 5, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(680) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 10, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 15, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 10, 17, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 10, 22, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 10, 27, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 11, 1, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 11, 6, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 11, 11, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 11, 16, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 11, 21, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 11, 26, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 1, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 6, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 11, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 16, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 21, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 26, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2025, 12, 31, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 5, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 10, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 15, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 20, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 21, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 22, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 23, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 24, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 25, 23, 15, 37, 864, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 26, 0, 15, 37, 864, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 26, 1, 15, 37, 864, DateTimeKind.Utc).AddTicks(740) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 26, 2, 15, 37, 864, DateTimeKind.Utc).AddTicks(740) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "ComicVineId", "DateAdded" },
                values: new object[] { null, new DateTime(2026, 1, 26, 3, 15, 37, 864, DateTimeKind.Utc).AddTicks(740) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComicVineId",
                table: "Comics");

            migrationBuilder.RenameColumn(
                name: "ComicVineLastSync",
                table: "Comics",
                newName: "MetronLastSync");

            migrationBuilder.AddColumn<string>(
                name: "MetronId",
                table: "Comics",
                type: "character varying(50)",
                maxLength: 50,
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
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 26, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8420), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 31, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 5, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 10, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 15, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8430), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 10, 17, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 10, 22, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 10, 27, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 11, 1, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 11, 6, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 11, 11, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8440), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 11, 16, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 11, 21, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 11, 26, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 1, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 6, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 11, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8450), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8460), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 21, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8460), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 26, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8460), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2025, 12, 31, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 5, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 10, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 15, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 20, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 21, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8470), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 22, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 23, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 24, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 25, 22, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 25, 23, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 26, 0, 45, 20, 281, DateTimeKind.Utc).AddTicks(8480), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 26, 1, 45, 20, 281, DateTimeKind.Utc).AddTicks(8490), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateAdded", "MetronId" },
                values: new object[] { new DateTime(2026, 1, 26, 2, 45, 20, 281, DateTimeKind.Utc).AddTicks(8490), null });
        }
    }
}
