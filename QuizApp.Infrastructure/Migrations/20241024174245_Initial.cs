using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Initial = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    OptionChoice = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => new { x.QuestionId, x.OptionChoice });
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSchedules",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchedules", x => new { x.UserId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_UserSchedules_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSchedules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserScores",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScores", x => new { x.UserId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK_UserScores_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserScores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    ResponseId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 20, nullable: false),
                    OptionChoice = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_Responses_Options_QuestionId_OptionChoice",
                        columns: x => new { x.QuestionId, x.OptionChoice },
                        principalTable: "Options",
                        principalColumns: new[] { "QuestionId", "OptionChoice" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Responses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId");
                    table.ForeignKey(
                        name: "FK_Responses_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId");
                    table.ForeignKey(
                        name: "FK_Responses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "QuizId", "Title" },
                values: new object[] { new Guid("98dc1ade-7d39-42dc-ab8b-ee9ecedd590c"), "Mock Quiz" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "Initial", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("3f2aa04d-319f-4476-9884-2376dffa5bba"), "Admin", "admin", "dummypassword", "Admin" },
                    { new Guid("6dcadd85-1a13-44db-ac5e-935d37e27703"), "Daniel Adamlu", "NL23-2", "dummypassword", "User" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "ImageUrl", "QuestionText", "QuizId" },
                values: new object[] { new Guid("93bd883c-cd30-4aa1-8d22-ebe61f852cac"), null, "What is the capital of Indonesia?", new Guid("98dc1ade-7d39-42dc-ab8b-ee9ecedd590c") });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "EndDate", "QuizId", "StartDate" },
                values: new object[] { new Guid("e8af45fb-b512-467f-b404-577cda511471"), new DateTime(2024, 10, 26, 0, 0, 0, 0, DateTimeKind.Local), new Guid("98dc1ade-7d39-42dc-ab8b-ee9ecedd590c"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionChoice", "QuestionId", "ImageUrl", "IsCorrect", "OptionText" },
                values: new object[,]
                {
                    { "A", new Guid("93bd883c-cd30-4aa1-8d22-ebe61f852cac"), "", false, "Medan" },
                    { "B", new Guid("93bd883c-cd30-4aa1-8d22-ebe61f852cac"), "", false, "Bandung" },
                    { "C", new Guid("93bd883c-cd30-4aa1-8d22-ebe61f852cac"), "", true, "Jakarta" }
                });

            migrationBuilder.InsertData(
                table: "UserSchedules",
                columns: new[] { "ScheduleId", "UserId", "Status" },
                values: new object[] { new Guid("e8af45fb-b512-467f-b404-577cda511471"), new Guid("6dcadd85-1a13-44db-ac5e-935d37e27703"), "Incomplete" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_QuestionId",
                table: "Responses",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_QuestionId_OptionChoice",
                table: "Responses",
                columns: new[] { "QuestionId", "OptionChoice" });

            migrationBuilder.CreateIndex(
                name: "IX_Responses_ScheduleId",
                table: "Responses",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_UserId",
                table: "Responses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_QuizId",
                table: "Schedules",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedules_ScheduleId",
                table: "UserSchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserScores_ScheduleId",
                table: "UserScores",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "UserSchedules");

            migrationBuilder.DropTable(
                name: "UserScores");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
