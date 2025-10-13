using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithTransmetro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AvatarUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeriesName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IssueNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Condition = table.Column<string>(type: "TEXT", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "e3c4a2fa-800d-44b6-994b-b15e9e8a9bf0", new DateTime(2025, 10, 13, 18, 32, 24, 9, DateTimeKind.Utc).AddTicks(5660), "demo@comictracker.com", true, "Demo", "User", false, null, "DEMO@COMICTRACKER.COM", "DEMOUSER", "AQAAAAIAAYagAAAAELV+5LKvl7raTb4sk+Fv8JHYK9pooYJQkMtXSoCxxLTlmBsVThXO3YXn842/DwzpuQ==", null, false, "07674ad2-4cb7-4047-aed5-bfd8d441ab4f", false, "demouser" },
                    { 2, 0, null, "4a199d23-c7d2-40b8-ba5b-a467cbb422ee", new DateTime(2025, 10, 13, 18, 32, 24, 41, DateTimeKind.Utc).AddTicks(3940), "fan@comictracker.com", true, "Comic", "Fan", false, null, "FAN@COMICTRACKER.COM", "COMICFAN", "AQAAAAIAAYagAAAAEFo6dY3Gi3XNqA65KmYvOtnoNECMGOTmomcx+6qvOVIDzsZAykYrg0BCq8q963OV6A==", null, false, "6a0a1f9f-29db-440e-85c3-e07b8e94a76c", false, "comicfan" }
                });

            migrationBuilder.InsertData(
                table: "Comics",
                columns: new[] { "Id", "Condition", "CoverImageUrl", "DateAdded", "DateModified", "IssueNumber", "Notes", "Publisher", "PurchasePrice", "SeriesName", "UserId" },
                values: new object[,]
                {
                    { 1, "NearMint", null, new DateTime(2025, 9, 13, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2620), null, "#300", "First appearance of Venom", "Marvel", 25.00m, "Amazing Spider-Man", 1 },
                    { 2, "VeryFine", null, new DateTime(2025, 9, 18, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2620), null, "#1", "Classic Batman issue", "DC", 50.00m, "Batman", 1 },
                    { 3, "Mint", null, new DateTime(2025, 9, 23, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2620), null, "#1", "First issue of the series", "Image", 100.00m, "The Walking Dead", 1 },
                    { 4, "Fine", null, new DateTime(2025, 9, 28, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630), null, "#1", "First X-Men comic", "Marvel", 75.00m, "X-Men", 1 },
                    { 5, "Good", null, new DateTime(2025, 10, 3, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630), null, "#1", "Man of Steel", "DC", 60.00m, "Superman", 1 },
                    { 6, "VeryFine", null, new DateTime(2025, 7, 5, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630), null, "#300", "Classic black suit issue", "Marvel", 50.00m, "Spider-Man", 2 },
                    { 7, "NearMint", null, new DateTime(2025, 7, 10, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630), null, "#1", "Iconic Joker story", "DC", 75.00m, "Batman: The Killing Joke", 2 },
                    { 8, "Mint", null, new DateTime(2025, 7, 15, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2630), null, "#1", "First issue of acclaimed series", "Image", 3.99m, "Saga", 2 },
                    { 9, "Good", null, new DateTime(2025, 7, 20, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640), null, "#1", "Seminal graphic novel", "DC", 1.50m, "Watchmen", 2 },
                    { 10, "Fine", null, new DateTime(2025, 7, 25, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640), null, "#1", "First zombie apocalypse issue", "Image", 2.95m, "The Walking Dead", 2 },
                    { 11, "Good", null, new DateTime(2025, 7, 30, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640), null, "#141", "Days of Future Past begins", "Marvel", 0.60m, "X-Men", 2 },
                    { 12, "Fine", null, new DateTime(2025, 8, 4, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640), null, "#1", "Hellboy's first appearance", "Dark Horse", 2.50m, "Hellboy: Seed of Destruction", 2 },
                    { 13, "Good", null, new DateTime(2025, 8, 9, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640), null, "#1", "First TMNT appearance", "Mirage Studios", 1.50m, "Teenage Mutant Ninja Turtles", 2 },
                    { 14, "VeryFine", null, new DateTime(2025, 8, 14, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2640), null, "#1", "Neil Gaiman masterpiece", "Vertigo", 2.00m, "Sandman", 2 },
                    { 15, "NearMint", null, new DateTime(2025, 8, 19, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650), null, "#1", "Post-apocalyptic drama", "Vertigo", 2.95m, "Y: The Last Man", 2 },
                    { 16, "Good", null, new DateTime(2025, 8, 24, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650), null, "#1", "Jeff Smith's epic fantasy", "Cartoon Books", 1.95m, "Bone", 2 },
                    { 17, "Good", null, new DateTime(2025, 8, 29, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650), null, "#1", "Pulitzer Prize winner", "Pantheon Books", 5.95m, "Maus", 2 },
                    { 18, "Fine", null, new DateTime(2025, 9, 3, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650), null, "#1", "Garth Ennis classic", "Vertigo", 2.50m, "Preacher", 2 },
                    { 19, "NearMint", null, new DateTime(2025, 9, 8, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2650), null, "#1", "Robert Kirkman superhero saga", "Image", 2.95m, "Invincible", 2 },
                    { 20, "Good", null, new DateTime(2025, 9, 13, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660), null, "#48", "First Silver Surfer", "Marvel", 0.12m, "Fantastic Four", 2 },
                    { 21, "Good", null, new DateTime(2025, 9, 18, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660), null, "#1", "Alan Moore dystopian tale", "Vertigo", 1.95m, "V for Vendetta", 2 },
                    { 22, "Fair", null, new DateTime(2025, 9, 23, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660), null, "#181", "First Wolverine appearance", "Marvel", 0.25m, "The Incredible Hulk", 2 },
                    { 23, "VeryFine", null, new DateTime(2025, 9, 28, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660), null, "#1", "Katsuhiro Otomo cyberpunk", "Marvel Epic", 1.95m, "Akira", 2 },
                    { 24, "NearMint", null, new DateTime(2025, 10, 3, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2660), null, "#1", "Todd McFarlane creation", "Image", 1.95m, "Spawn", 2 },
                    { 25, "Fine", null, new DateTime(2025, 10, 8, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2680), null, "#1", "Warren Ellis cyberpunk journalism", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 26, "Fine", null, new DateTime(2025, 10, 9, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2680), null, "#2", "On the Stump. Spider Jerusalem covers a political rally.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 27, "Fine", null, new DateTime(2025, 10, 10, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2680), null, "#3", "Wild in the Country. Spider investigates cryogenic revivals.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 28, "Fine", null, new DateTime(2025, 10, 11, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690), null, "#4", "New City. Spider explores the city's underbelly.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 29, "Fine", null, new DateTime(2025, 10, 12, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690), null, "#5", "What Spider Watches on TV. Media criticism and satire.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 30, "Fine", null, new DateTime(2025, 10, 13, 18, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690), null, "#6", "God Riding Shotgun. Spider investigates new religious movements.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 31, "Fine", null, new DateTime(2025, 10, 13, 19, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690), null, "#7", "My Boyfriend is a Virus. Exploration of transhumanism.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 32, "Fine", null, new DateTime(2025, 10, 13, 20, 32, 24, 73, DateTimeKind.Utc).AddTicks(2690), null, "#8", "Another Cold Morning. More on cryogenic revivals and societal change.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 33, "Fine", null, new DateTime(2025, 10, 13, 21, 32, 24, 73, DateTimeKind.Utc).AddTicks(2700), null, "#9", "Party Time. Spider attends a political convention.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 34, "Fine", null, new DateTime(2025, 10, 13, 22, 32, 24, 73, DateTimeKind.Utc).AddTicks(2700), null, "#10", "Freeze Me with Your Kiss. Exploration of future cryogenic technology.", "Vertigo", 2.50m, "Transmetropolitan", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comics_Publisher",
                table: "Comics",
                column: "Publisher");

            migrationBuilder.CreateIndex(
                name: "IX_Comics_SeriesName",
                table: "Comics",
                column: "SeriesName");

            migrationBuilder.CreateIndex(
                name: "IX_Comics_UserId",
                table: "Comics",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comics");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
