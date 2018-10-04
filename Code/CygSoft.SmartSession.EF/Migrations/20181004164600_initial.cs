using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.EF.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DifficultyRating = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    OptimalDuration = table.Column<int>(nullable: false),
                    PracticalityRating = table.Column<int>(nullable: false),
                    Scribed = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

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
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoalTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    MinutesPracticed = table.Column<int>(nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Weighting = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseKeyword",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false),
                    KeywordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseKeyword", x => new { x.ExerciseId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_ExerciseKeyword_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseKeyword_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "GoalKeyword",
                columns: table => new
                {
                    GoalId = table.Column<int>(nullable: false),
                    KeywordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalKeyword", x => new { x.GoalId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_GoalKeyword_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalKeyword_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalTaskKeyword",
                columns: table => new
                {
                    GoalTaskId = table.Column<int>(nullable: false),
                    KeywordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalTaskKeyword", x => new { x.GoalTaskId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_GoalTaskKeyword_GoalTasks_GoalTaskId",
                        column: x => x.GoalTaskId,
                        principalTable: "GoalTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalTaskKeyword_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseKeyword_KeywordId",
                table: "ExerciseKeyword",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachmentKeyword_KeywordId",
                table: "FileAttachmentKeyword",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalKeyword_KeywordId",
                table: "GoalKeyword",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalTaskKeyword_KeywordId",
                table: "GoalTaskKeyword",
                column: "KeywordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseKeyword");

            migrationBuilder.DropTable(
                name: "FileAttachmentKeyword");

            migrationBuilder.DropTable(
                name: "GoalKeyword");

            migrationBuilder.DropTable(
                name: "GoalTaskKeyword");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "GoalTasks");

            migrationBuilder.DropTable(
                name: "Keywords");
        }
    }
}
