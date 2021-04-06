using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServicesStore.Api.CartService.Migrations
{
    public partial class MigrationMySQLInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartSession",
                columns: table => new
                {
                    CartSessionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSession", x => x.CartSessionId);
                });

            migrationBuilder.CreateTable(
                name: "CartSessionDetail",
                columns: table => new
                {
                    CartSessionDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    BookGuid = table.Column<string>(nullable: true),
                    CartSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSessionDetail", x => x.CartSessionDetailId);
                    table.ForeignKey(
                        name: "FK_CartSessionDetail_CartSession_CartSessionId",
                        column: x => x.CartSessionId,
                        principalTable: "CartSession",
                        principalColumn: "CartSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartSessionDetail_CartSessionId",
                table: "CartSessionDetail",
                column: "CartSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartSessionDetail");

            migrationBuilder.DropTable(
                name: "CartSession");
        }
    }
}
