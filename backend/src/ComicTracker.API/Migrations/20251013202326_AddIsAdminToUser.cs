using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAdminToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsAdmin", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2ece528-4e6c-4b84-b4e8-5a26ca03cfc9", new DateTime(2025, 10, 13, 20, 23, 26, 559, DateTimeKind.Utc).AddTicks(1220), false, "AQAAAAIAAYagAAAAEBUaTbyAzJjnDDaoPHtzoV0FtFoiCHKJel+LgplcVUQL2SOyCQcb9CmQKebf4bKAiw==", "b5633878-3bdb-4d46-82b2-93dc8f613148" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsAdmin", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddf4257b-77ce-4200-abda-8b8792e726a0", new DateTime(2025, 10, 13, 20, 23, 26, 590, DateTimeKind.Utc).AddTicks(5580), false, "AQAAAAIAAYagAAAAELRxRgj2C5XX8feV3hxAlQd+zfF6u8xGjNt+jqwN2A0BuPBXvkCvprNeONWe+0o0xg==", "62274109-45a1-488c-b7a2-e4cc5828fb6b" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2025, 9, 13, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2025, 9, 18, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2025, 9, 23, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2025, 9, 28, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2025, 10, 3, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2025, 7, 5, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2025, 7, 10, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2025, 7, 15, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2025, 7, 20, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateTime(2025, 7, 25, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateAdded",
                value: new DateTime(2025, 7, 30, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateAdded",
                value: new DateTime(2025, 8, 4, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateAdded",
                value: new DateTime(2025, 8, 9, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateAdded",
                value: new DateTime(2025, 8, 14, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateAdded",
                value: new DateTime(2025, 8, 19, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateAdded",
                value: new DateTime(2025, 8, 24, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateAdded",
                value: new DateTime(2025, 8, 29, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateAdded",
                value: new DateTime(2025, 9, 3, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateAdded",
                value: new DateTime(2025, 9, 8, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateAdded",
                value: new DateTime(2025, 9, 13, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateAdded",
                value: new DateTime(2025, 9, 18, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateAdded",
                value: new DateTime(2025, 9, 23, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateAdded",
                value: new DateTime(2025, 9, 28, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateAdded",
                value: new DateTime(2025, 10, 3, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateAdded",
                value: new DateTime(2025, 10, 8, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateAdded",
                value: new DateTime(2025, 10, 9, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateAdded",
                value: new DateTime(2025, 10, 10, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateAdded",
                value: new DateTime(2025, 10, 11, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateAdded",
                value: new DateTime(2025, 10, 12, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 20, 23, 26, 623, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 21, 23, 26, 623, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 22, 23, 26, 623, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 23, 23, 26, 623, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateAdded",
                value: new DateTime(2025, 10, 14, 0, 23, 26, 623, DateTimeKind.Utc).AddTicks(4070));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3c4a2fa-800d-44b6-994b-b15e9e8a9bf0", new DateTime(2025, 10, 13, 18, 32, 24, 9, DateTimeKind.Utc).AddTicks(5660), "AQAAAAIAAYagAAAAELV+5LKvl7raTb4sk+Fv8JHYK9pooYJQkMtXSoCxxLTlmBsVThXO3YXn842/DwzpuQ==", "07674ad2-4cb7-4047-aed5-bfd8d441ab4f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a199d23-c7d2-40b8-ba5b-a467cbb422ee", new DateTime(2025, 10, 13, 18, 32, 24, 41, DateTimeKind.Utc).AddTicks(3940), "AQAAAAIAAYagAAAAEFo6dY3Gi3XNqA65KmYvOtnoNECMGOTmomcx+6qvOVIDzsZAykYrg0BCq8q963OV6A==", "6a0a1f9f-29db-440e-85c3-e07b8e94a76c" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2025, 9, 13, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2620));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2025, 9, 18, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2620));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2025, 9, 23, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2620));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2025, 9, 28, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2025, 10, 3, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2025, 7, 5, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2025, 7, 10, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2025, 7, 15, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2025, 7, 20, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateTime(2025, 7, 25, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateAdded",
                value: new DateTime(2025, 7, 30, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateAdded",
                value: new DateTime(2025, 8, 4, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateAdded",
                value: new DateTime(2025, 8, 9, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateAdded",
                value: new DateTime(2025, 8, 14, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateAdded",
                value: new DateTime(2025, 8, 19, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateAdded",
                value: new DateTime(2025, 8, 24, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateAdded",
                value: new DateTime(2025, 8, 29, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateAdded",
                value: new DateTime(2025, 9, 3, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateAdded",
                value: new DateTime(2025, 9, 8, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateAdded",
                value: new DateTime(2025, 9, 13, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateAdded",
                value: new DateTime(2025, 9, 18, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateAdded",
                value: new DateTime(2025, 9, 23, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateAdded",
                value: new DateTime(2025, 9, 28, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateAdded",
                value: new DateTime(2025, 10, 3, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateAdded",
                value: new DateTime(2025, 10, 8, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateAdded",
                value: new DateTime(2025, 10, 9, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateAdded",
                value: new DateTime(2025, 10, 10, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateAdded",
                value: new DateTime(2025, 10, 11, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateAdded",
                value: new DateTime(2025, 10, 12, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 19, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 20, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 21, 32, 24, 73, DateTimeKind.Utc).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 22, 32, 24, 73, DateTimeKind.Utc).AddTicks(2700));
        }
    }
}
