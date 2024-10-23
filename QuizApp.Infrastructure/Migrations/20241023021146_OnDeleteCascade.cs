using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Options_QuestionId_OptionChoice",
                table: "Responses");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Options_QuestionId_OptionChoice",
                table: "Responses",
                columns: new[] { "QuestionId", "OptionChoice" },
                principalTable: "Options",
                principalColumns: new[] { "QuestionId", "OptionChoice" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Options_QuestionId_OptionChoice",
                table: "Responses");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Options_QuestionId_OptionChoice",
                table: "Responses",
                columns: new[] { "QuestionId", "OptionChoice" },
                principalTable: "Options",
                principalColumns: new[] { "QuestionId", "OptionChoice" });
        }
    }
}
