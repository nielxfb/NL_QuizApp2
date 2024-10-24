using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "QuizId", "Title" },
                values: new object[] { new Guid("03239d06-f552-4108-b578-ac532d7a2453"), "Mock Quiz" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "Initial", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("4dfd4467-8c09-48a8-bc1c-12c44903a128"), "Admin", "admin", "dummypassword", "Admin" },
                    { new Guid("56c5278c-cb5f-43d0-9aea-9a8aed27e7e2"), "Daniel Adamlu", "NL23-2", "dummypassword", "User" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "ImageUrl", "QuestionText", "QuizId" },
                values: new object[] { new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c"), null, "What is the capital of Indonesia?", new Guid("03239d06-f552-4108-b578-ac532d7a2453") });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "EndDate", "QuizId", "StartDate" },
                values: new object[] { new Guid("8f672633-6556-4c8e-b48e-09853fc9ad7e"), new DateTime(2024, 10, 26, 0, 0, 0, 0, DateTimeKind.Local), new Guid("03239d06-f552-4108-b578-ac532d7a2453"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionChoice", "QuestionId", "ImageUrl", "IsCorrect", "OptionText" },
                values: new object[,]
                {
                    { "A", new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c"), "", false, "Medan" },
                    { "B", new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c"), "", false, "Bandung" },
                    { "C", new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c"), "", true, "Jakarta" }
                });

            migrationBuilder.InsertData(
                table: "UserSchedules",
                columns: new[] { "ScheduleId", "UserId", "Status" },
                values: new object[] { new Guid("8f672633-6556-4c8e-b48e-09853fc9ad7e"), new Guid("56c5278c-cb5f-43d0-9aea-9a8aed27e7e2"), "Incomplete" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "OptionChoice", "QuestionId" },
                keyValues: new object[] { "A", new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c") });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "OptionChoice", "QuestionId" },
                keyValues: new object[] { "B", new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c") });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "OptionChoice", "QuestionId" },
                keyValues: new object[] { "C", new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c") });

            migrationBuilder.DeleteData(
                table: "UserSchedules",
                keyColumns: new[] { "ScheduleId", "UserId" },
                keyValues: new object[] { new Guid("8f672633-6556-4c8e-b48e-09853fc9ad7e"), new Guid("56c5278c-cb5f-43d0-9aea-9a8aed27e7e2") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("4dfd4467-8c09-48a8-bc1c-12c44903a128"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("bb75dce2-bfae-4f84-b24a-4c3ecbdb5c1c"));

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: new Guid("8f672633-6556-4c8e-b48e-09853fc9ad7e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("56c5278c-cb5f-43d0-9aea-9a8aed27e7e2"));

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizId",
                keyValue: new Guid("03239d06-f552-4108-b578-ac532d7a2453"));
        }
    }
}
