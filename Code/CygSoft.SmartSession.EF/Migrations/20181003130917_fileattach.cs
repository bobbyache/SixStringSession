using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.EF.Migrations
{
    public partial class fileattach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    FileTitle = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachmentKeyword",
                columns: table => new
                {
                    FileAttachmentId = table.Column<int>(nullable: false),
                    KeywordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachmentKeyword", x => new { x.FileAttachmentId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_FileAttachmentKeyword_FileAttachments_FileAttachmentId",
                        column: x => x.FileAttachmentId,
                        principalTable: "FileAttachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileAttachmentKeyword_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachmentKeyword_KeywordId",
                table: "FileAttachmentKeyword",
                column: "KeywordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachmentKeyword");

            migrationBuilder.DropTable(
                name: "FileAttachments");
        }
    }
}
