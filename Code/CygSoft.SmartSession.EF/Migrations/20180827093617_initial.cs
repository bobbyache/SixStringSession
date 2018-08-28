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
                    DifficultyRating = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    OptimalDuration = table.Column<int>(nullable: false),
                    PracticalityRating = table.Column<int>(nullable: false),
                    Scribed = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TargetCompletionDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionPracticeTaskDuration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Minutes = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPracticeTaskDuration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionPracticeTaskManualProgress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Progress = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPracticeTaskManualProgress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionPracticeTaskMetronome",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComfortableSpeed = table.Column<int>(nullable: false),
                    EndSpeed = table.Column<int>(nullable: false),
                    StartSpeed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPracticeTaskMetronome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ExerciseId = table.Column<int>(nullable: true),
                    GoalTaskType = table.Column<int>(nullable: false),
                    TargetPracticeDuration = table.Column<int>(nullable: false),
                    TargetSpeed = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoalPracticeTask",
                columns: table => new
                {
                    GoalId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalPracticeTask", x => new { x.GoalId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_GoalPracticeTask_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalPracticeTask_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionPracticeTask",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false),
                    PracticeTaskId = table.Column<int>(nullable: false),
                    DurationId = table.Column<int>(nullable: true),
                    ManualProgressEstimateId = table.Column<int>(nullable: true),
                    MetronomeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPracticeTask", x => new { x.SessionId, x.PracticeTaskId });
                    table.ForeignKey(
                        name: "FK_SessionPracticeTask_SessionPracticeTaskDuration_DurationId",
                        column: x => x.DurationId,
                        principalTable: "SessionPracticeTaskDuration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionPracticeTask_SessionPracticeTaskManualProgress_ManualProgressEstimateId",
                        column: x => x.ManualProgressEstimateId,
                        principalTable: "SessionPracticeTaskManualProgress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionPracticeTask_SessionPracticeTaskMetronome_MetronomeId",
                        column: x => x.MetronomeId,
                        principalTable: "SessionPracticeTaskMetronome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionPracticeTask_Tasks_PracticeTaskId",
                        column: x => x.PracticeTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionPracticeTask_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalPracticeTask_TaskId",
                table: "GoalPracticeTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPracticeTask_DurationId",
                table: "SessionPracticeTask",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPracticeTask_ManualProgressEstimateId",
                table: "SessionPracticeTask",
                column: "ManualProgressEstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPracticeTask_MetronomeId",
                table: "SessionPracticeTask",
                column: "MetronomeId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPracticeTask_PracticeTaskId",
                table: "SessionPracticeTask",
                column: "PracticeTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ExerciseId",
                table: "Tasks",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalPracticeTask");

            migrationBuilder.DropTable(
                name: "SessionPracticeTask");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "SessionPracticeTaskDuration");

            migrationBuilder.DropTable(
                name: "SessionPracticeTaskManualProgress");

            migrationBuilder.DropTable(
                name: "SessionPracticeTaskMetronome");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
