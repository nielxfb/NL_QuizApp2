using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testing2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_Question_QuestionId",
                table: "Option");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quiz_QuizId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Response_Option_QuestionId_OptionChoice",
                table: "Response");

            migrationBuilder.DropForeignKey(
                name: "FK_Response_Question_QuestionId",
                table: "Response");

            migrationBuilder.DropForeignKey(
                name: "FK_Response_Quiz_QuizId",
                table: "Response");

            migrationBuilder.DropForeignKey(
                name: "FK_Response_User_UserId",
                table: "Response");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Quiz_QuizId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_Schedule_ScheduleId",
                table: "UserSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedule_User_UserId",
                table: "UserSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchedule",
                table: "UserSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Response",
                table: "Response");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quiz",
                table: "Quiz");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.RenameTable(
                name: "UserSchedule",
                newName: "UserSchedules");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "Schedules");

            migrationBuilder.RenameTable(
                name: "Response",
                newName: "Responses");

            migrationBuilder.RenameTable(
                name: "Quiz",
                newName: "Quizzes");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "Options");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedule_ScheduleId",
                table: "UserSchedules",
                newName: "IX_UserSchedules_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_QuizId",
                table: "Schedules",
                newName: "IX_Schedules_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Response_UserId",
                table: "Responses",
                newName: "IX_Responses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Response_QuizId",
                table: "Responses",
                newName: "IX_Responses_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Response_QuestionId_OptionChoice",
                table: "Responses",
                newName: "IX_Responses_QuestionId_OptionChoice");

            migrationBuilder.RenameIndex(
                name: "IX_Response_QuestionId",
                table: "Responses",
                newName: "IX_Responses_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuizId",
                table: "Questions",
                newName: "IX_Questions_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchedules",
                table: "UserSchedules",
                columns: new[] { "UserId", "ScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responses",
                table: "Responses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                columns: new[] { "QuestionId", "OptionChoice" });

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Options_QuestionId_OptionChoice",
                table: "Responses",
                columns: new[] { "QuestionId", "OptionChoice" },
                principalTable: "Options",
                principalColumns: new[] { "QuestionId", "OptionChoice" });

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Questions_QuestionId",
                table: "Responses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Quizzes_QuizId",
                table: "Responses",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Users_UserId",
                table: "Responses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Quizzes_QuizId",
                table: "Schedules",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedules_Schedules_ScheduleId",
                table: "UserSchedules",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedules_Users_UserId",
                table: "UserSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Options_QuestionId_OptionChoice",
                table: "Responses");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Questions_QuestionId",
                table: "Responses");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Quizzes_QuizId",
                table: "Responses");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Users_UserId",
                table: "Responses");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Quizzes_QuizId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedules_Schedules_ScheduleId",
                table: "UserSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchedules_Users_UserId",
                table: "UserSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchedules",
                table: "UserSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responses",
                table: "Responses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.RenameTable(
                name: "UserSchedules",
                newName: "UserSchedule");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Schedule");

            migrationBuilder.RenameTable(
                name: "Responses",
                newName: "Response");

            migrationBuilder.RenameTable(
                name: "Quizzes",
                newName: "Quiz");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "Option");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchedules_ScheduleId",
                table: "UserSchedule",
                newName: "IX_UserSchedule_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_QuizId",
                table: "Schedule",
                newName: "IX_Schedule_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_UserId",
                table: "Response",
                newName: "IX_Response_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_QuizId",
                table: "Response",
                newName: "IX_Response_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_QuestionId_OptionChoice",
                table: "Response",
                newName: "IX_Response_QuestionId_OptionChoice");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_QuestionId",
                table: "Response",
                newName: "IX_Response_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizId",
                table: "Question",
                newName: "IX_Question_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchedule",
                table: "UserSchedule",
                columns: new[] { "UserId", "ScheduleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Response",
                table: "Response",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quiz",
                table: "Quiz",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                columns: new[] { "QuestionId", "OptionChoice" });

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Question_QuestionId",
                table: "Option",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quiz_QuizId",
                table: "Question",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Response_Option_QuestionId_OptionChoice",
                table: "Response",
                columns: new[] { "QuestionId", "OptionChoice" },
                principalTable: "Option",
                principalColumns: new[] { "QuestionId", "OptionChoice" });

            migrationBuilder.AddForeignKey(
                name: "FK_Response_Question_QuestionId",
                table: "Response",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Response_Quiz_QuizId",
                table: "Response",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Response_User_UserId",
                table: "Response",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Quiz_QuizId",
                table: "Schedule",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_Schedule_ScheduleId",
                table: "UserSchedule",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchedule_User_UserId",
                table: "UserSchedule",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
