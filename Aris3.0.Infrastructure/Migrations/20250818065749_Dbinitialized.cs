using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aris3._0.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Dbinitialized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Created",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Created", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modified",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modified", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountStat = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tmbd",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: true),
                    Vote_average = table.Column<float>(type: "real", nullable: false),
                    Vote_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tmbd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    AccountStat = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCopyright = table.Column<bool>(type: "bit", nullable: false),
                    SubDocquyen = table.Column<bool>(type: "bit", nullable: false),
                    ChieuRap = table.Column<bool>(type: "bit", nullable: false),
                    TrailerUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpisodeCurrent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpisodeTotal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notify = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Showtimes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    TmdbId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Createdid = table.Column<int>(type: "int", nullable: false),
                    Modifiedid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Films_Created_Createdid",
                        column: x => x.Createdid,
                        principalTable: "Created",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Films_Modified_Modifiedid",
                        column: x => x.Modifiedid,
                        principalTable: "Modified",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Films_Tmbd_TmdbId",
                        column: x => x.TmdbId,
                        principalTable: "Tmbd",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccounId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Otps_Accounts_AccounId",
                        column: x => x.AccounId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorFilm",
                columns: table => new
                {
                    Actorsid = table.Column<int>(type: "int", nullable: false),
                    FilmsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorFilm", x => new { x.Actorsid, x.FilmsId });
                    table.ForeignKey(
                        name: "FK_ActorFilm_Actors_Actorsid",
                        column: x => x.Actorsid,
                        principalTable: "Actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorFilm_Films_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFilm",
                columns: table => new
                {
                    CategoriesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilmsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFilm", x => new { x.CategoriesId, x.FilmsId });
                    table.ForeignKey(
                        name: "FK_CategoryFilm_Films_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFilm_categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryFilm",
                columns: table => new
                {
                    CountriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryFilm", x => new { x.CountriesId, x.FilmsId });
                    table.ForeignKey(
                        name: "FK_CountryFilm_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryFilm_Films_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectorFilm",
                columns: table => new
                {
                    DirectorsId = table.Column<int>(type: "int", nullable: false),
                    FilmsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorFilm", x => new { x.DirectorsId, x.FilmsId });
                    table.ForeignKey(
                        name: "FK_DirectorFilm_Directors_DirectorsId",
                        column: x => x.DirectorsId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorFilm_Films_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkEmbed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkM3U8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "id", "AccountStat", "City", "Country", "Created", "Email", "LastUpdated", "Name", "PhoneNumber", "Region", "Role", "State", "Zipcode" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), true, "Hanoi", "Vn", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "admin@gmail.com", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "Admin Person", "0123456789", "Sea", "Admin", "HN", "100000" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), true, "HCMC", "Vn", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "user1@gmail.com", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "User One", "0987654321", "Sea", "User", "HCM", "700000" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), true, "Danang", "Vn", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "user2@gmail.com", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "User Two", "0911222333", "Sea", "User", "DN", "550000" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate", "type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "Access to standard features and content.", "Basic Plan", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "Basic" },
                    { 2, new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "Includes HD streaming and exclusive content.", "Premium Plan", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "Premium" },
                    { 3, new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "For organizations with extended access and multiple accounts.", "Enterprise Plan", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "Enterprise" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountStat", "Created", "Email", "LastUpdated", "Password", "PersonId", "Role", "SubscriptionId", "UserName", "status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "admin@gmail.com", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "admin123", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Admin", 1, "admin", true },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "user1@gmail.com", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "user123", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "User", 2, "user1", true },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "user2@gmail.com", new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), "user123", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "User", 3, "user2", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonId",
                table: "Accounts",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_SubscriptionId",
                table: "Accounts",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorFilm_FilmsId",
                table: "ActorFilm",
                column: "FilmsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFilm_FilmsId",
                table: "CategoryFilm",
                column: "FilmsId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryFilm_FilmsId",
                table: "CountryFilm",
                column: "FilmsId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorFilm_FilmsId",
                table: "DirectorFilm",
                column: "FilmsId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_ServerId",
                table: "Episodes",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Createdid",
                table: "Films",
                column: "Createdid");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Modifiedid",
                table: "Films",
                column: "Modifiedid");

            migrationBuilder.CreateIndex(
                name: "IX_Films_TmdbId",
                table: "Films",
                column: "TmdbId");

            migrationBuilder.CreateIndex(
                name: "IX_Otps_AccounId",
                table: "Otps",
                column: "AccounId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_FilmId",
                table: "Servers",
                column: "FilmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorFilm");

            migrationBuilder.DropTable(
                name: "CategoryFilm");

            migrationBuilder.DropTable(
                name: "CountryFilm");

            migrationBuilder.DropTable(
                name: "DirectorFilm");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Created");

            migrationBuilder.DropTable(
                name: "Modified");

            migrationBuilder.DropTable(
                name: "Tmbd");
        }
    }
}
