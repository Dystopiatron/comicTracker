using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace comicTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AvatarUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeriesName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IssueNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "FirstName", "IsAdmin", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "4fa83748-5939-415f-9a09-203360e43f74", new DateTime(2025, 10, 15, 18, 10, 55, 725, DateTimeKind.Utc).AddTicks(8470), "demo@comictracker.com", true, "Demo", false, "User", false, null, "DEMO@COMICTRACKER.COM", "DEMOUSER", "AQAAAAIAAYagAAAAEBboDrFKqqKAID9Xb7ARRDmAVVIu2narJCMFsXVMY2b99r0xHXNJlogMeaPjXFzHyg==", null, false, "7a5434c0-1c23-439c-85f1-24a001c35a59", false, "demouser" },
                    { 2, 0, null, "b803ce2c-6ca4-4a64-b6f4-0bbc80cf65e9", new DateTime(2025, 10, 15, 18, 10, 55, 757, DateTimeKind.Utc).AddTicks(4000), "fan@comictracker.com", true, "Comic", false, "Fan", false, null, "FAN@COMICTRACKER.COM", "COMICFAN", "AQAAAAIAAYagAAAAEP+guLaiu3wswHgs/0eeW50jai/kWotsYzR83GAsqeepaUUi/5/LJVQ7SHyrxd7pBA==", null, false, "05877fe9-48d9-41ea-af5a-fc347a820c5e", false, "comicfan" }
                });

            migrationBuilder.InsertData(
                table: "Comics",
                columns: new[] { "Id", "Condition", "CoverImageUrl", "DateAdded", "DateModified", "IssueNumber", "Notes", "Publisher", "PurchasePrice", "SeriesName", "UserId" },
                values: new object[,]
                {
                    { 1, "NearMint", null, new DateTime(2025, 9, 15, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8170), null, "#300", "First appearance of Venom", "Marvel", 25.00m, "Amazing Spider-Man", 1 },
                    { 2, "VeryFine", null, new DateTime(2025, 9, 20, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8180), null, "#1", "Classic Batman issue", "DC", 50.00m, "Batman", 1 },
                    { 3, "Mint", null, new DateTime(2025, 9, 25, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8180), null, "#1", "First issue of the series", "Image", 100.00m, "The Walking Dead", 1 },
                    { 4, "Fine", null, new DateTime(2025, 9, 30, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8180), null, "#1", "First X-Men comic", "Marvel", 75.00m, "X-Men", 1 },
                    { 5, "Good", null, new DateTime(2025, 10, 5, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190), null, "#1", "Man of Steel", "DC", 60.00m, "Superman", 1 },
                    { 6, "VeryFine", null, new DateTime(2025, 7, 7, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190), null, "#300", "Classic black suit issue", "Marvel", 50.00m, "Spider-Man", 2 },
                    { 7, "NearMint", null, new DateTime(2025, 7, 12, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190), null, "#1", "Iconic Joker story", "DC", 75.00m, "Batman: The Killing Joke", 2 },
                    { 8, "Mint", null, new DateTime(2025, 7, 17, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8190), null, "#1", "First issue of acclaimed series", "Image", 3.99m, "Saga", 2 },
                    { 9, "Good", null, new DateTime(2025, 7, 22, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8200), null, "#1", "Seminal graphic novel", "DC", 1.50m, "Watchmen", 2 },
                    { 10, "Fine", null, new DateTime(2025, 7, 27, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8200), null, "#1", "First zombie apocalypse issue", "Image", 2.95m, "The Walking Dead", 2 },
                    { 11, "Good", null, new DateTime(2025, 8, 1, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260), null, "#141", "Days of Future Past begins", "Marvel", 0.60m, "X-Men", 2 },
                    { 12, "Fine", null, new DateTime(2025, 8, 6, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260), null, "#1", "Hellboy's first appearance", "Dark Horse", 2.50m, "Hellboy: Seed of Destruction", 2 },
                    { 13, "Good", null, new DateTime(2025, 8, 11, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260), null, "#1", "First TMNT appearance", "Mirage Studios", 1.50m, "Teenage Mutant Ninja Turtles", 2 },
                    { 14, "VeryFine", null, new DateTime(2025, 8, 16, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260), null, "#1", "Neil Gaiman masterpiece", "Vertigo", 2.00m, "Sandman", 2 },
                    { 15, "NearMint", null, new DateTime(2025, 8, 21, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8260), null, "#1", "Post-apocalyptic drama", "Vertigo", 2.95m, "Y: The Last Man", 2 },
                    { 16, "Good", null, new DateTime(2025, 8, 26, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270), null, "#1", "Jeff Smith's epic fantasy", "Cartoon Books", 1.95m, "Bone", 2 },
                    { 17, "Good", null, new DateTime(2025, 8, 31, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270), null, "#1", "Pulitzer Prize winner", "Pantheon Books", 5.95m, "Maus", 2 },
                    { 18, "Fine", null, new DateTime(2025, 9, 5, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270), null, "#1", "Garth Ennis classic", "Vertigo", 2.50m, "Preacher", 2 },
                    { 19, "NearMint", null, new DateTime(2025, 9, 10, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270), null, "#1", "Robert Kirkman superhero saga", "Image", 2.95m, "Invincible", 2 },
                    { 20, "Good", null, new DateTime(2025, 9, 15, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270), null, "#48", "First Silver Surfer", "Marvel", 0.12m, "Fantastic Four", 2 },
                    { 21, "Good", null, new DateTime(2025, 9, 20, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270), null, "#1", "Alan Moore dystopian tale", "Vertigo", 1.95m, "V for Vendetta", 2 },
                    { 22, "Fair", null, new DateTime(2025, 9, 25, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8270), null, "#181", "First Wolverine appearance", "Marvel", 0.25m, "The Incredible Hulk", 2 },
                    { 23, "VeryFine", null, new DateTime(2025, 9, 30, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280), null, "#1", "Katsuhiro Otomo cyberpunk", "Marvel Epic", 1.95m, "Akira", 2 },
                    { 24, "NearMint", null, new DateTime(2025, 10, 5, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280), null, "#1", "Todd McFarlane creation", "Image", 1.95m, "Spawn", 2 },
                    { 25, "Fine", null, new DateTime(2025, 10, 10, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280), null, "#1", "Warren Ellis cyberpunk journalism", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 26, "Fine", null, new DateTime(2025, 10, 11, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280), null, "#2", "On the Stump. Spider Jerusalem covers a political rally.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 27, "Fine", null, new DateTime(2025, 10, 12, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8280), null, "#3", "Wild in the Country. Spider investigates cryogenic revivals.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 28, "Fine", null, new DateTime(2025, 10, 13, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290), null, "#4", "New City. Spider explores the city's underbelly.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 29, "Fine", null, new DateTime(2025, 10, 14, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290), null, "#5", "What Spider Watches on TV. Media criticism and satire.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 30, "Fine", null, new DateTime(2025, 10, 15, 18, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290), null, "#6", "God Riding Shotgun. Spider investigates new religious movements.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 31, "Fine", null, new DateTime(2025, 10, 15, 19, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290), null, "#7", "My Boyfriend is a Virus. Exploration of transhumanism.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 32, "Fine", null, new DateTime(2025, 10, 15, 20, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290), null, "#8", "Another Cold Morning. More on cryogenic revivals and societal change.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 33, "Fine", null, new DateTime(2025, 10, 15, 21, 10, 55, 788, DateTimeKind.Utc).AddTicks(8290), null, "#9", "Party Time. Spider attends a political convention.", "Vertigo", 2.50m, "Transmetropolitan", 2 },
                    { 34, "Fine", null, new DateTime(2025, 10, 15, 22, 10, 55, 788, DateTimeKind.Utc).AddTicks(8300), null, "#10", "Freeze Me with Your Kiss. Exploration of future cryogenic technology.", "Vertigo", 2.50m, "Transmetropolitan", 2 }
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
