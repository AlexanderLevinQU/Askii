using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionVoteAndAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerID = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionID = table.Column<string>(type: "TEXT", nullable: false),
                    ResponderUID = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Votes = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Users_ResponderUID",
                        column: x => x.ResponderUID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionVotes",
                columns: table => new
                {
                    VoteID = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionID = table.Column<string>(type: "TEXT", nullable: false),
                    UserID = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionVotes", x => x.VoteID);
                    table.ForeignKey(
                        name: "FK_QuestionVotes_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionVotes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ResponderUID",
                table: "Answers",
                column: "ResponderUID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionVotes_QuestionID",
                table: "QuestionVotes",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionVotes_UserID_QuestionID",
                table: "QuestionVotes",
                columns: new[] { "UserID", "QuestionID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "QuestionVotes");

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
