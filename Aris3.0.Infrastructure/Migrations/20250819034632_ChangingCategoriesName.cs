using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aris3._0.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingCategoriesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFilm_categories_CategoriesId",
                table: "CategoryFilm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292), new DateTime(2025, 8, 19, 3, 46, 32, 79, DateTimeKind.Utc).AddTicks(292) });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFilm_Categories_CategoriesId",
                table: "CategoryFilm",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryFilm_Categories_CategoriesId",
                table: "CategoryFilm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 8, 18, 6, 57, 48, 892, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryFilm_categories_CategoriesId",
                table: "CategoryFilm",
                column: "CategoriesId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
