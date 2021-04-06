using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ServicesStore.Api.Author.Migrations
{
    public partial class MigrationPostgresInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    BookAuthorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    BookAuthorGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => x.BookAuthorId);
                });

            migrationBuilder.CreateTable(
                name: "AcademicDegree",
                columns: table => new
                {
                    AcademicDegreeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    AcademicCenter = table.Column<string>(nullable: true),
                    DegreeDate = table.Column<DateTime>(nullable: true),
                    BookAuthorId = table.Column<int>(nullable: false),
                    AcademicDegreeGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDegree", x => x.AcademicDegreeId);
                    table.ForeignKey(
                        name: "FK_AcademicDegree_BookAuthor_BookAuthorId",
                        column: x => x.BookAuthorId,
                        principalTable: "BookAuthor",
                        principalColumn: "BookAuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicDegree_BookAuthorId",
                table: "AcademicDegree",
                column: "BookAuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicDegree");

            migrationBuilder.DropTable(
                name: "BookAuthor");
        }
    }
}
