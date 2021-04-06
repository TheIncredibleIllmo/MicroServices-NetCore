using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServicesStore.Api.Book.Migrations
{
    public partial class MigrationSqlServerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibraryMaterial",
                columns: table => new
                {
                    LibraryMaterialId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    BookAuthorGuid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryMaterial", x => x.LibraryMaterialId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryMaterial");
        }
    }
}
