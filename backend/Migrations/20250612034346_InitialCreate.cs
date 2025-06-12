using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UID = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<string>(type: "TEXT", nullable: false),
                    SessionAdminUID = table.Column<string>(type: "TEXT", nullable: false),
                    SessionTopic = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_SessionAdminUID",
                        column: x => x.SessionAdminUID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<string>(type: "TEXT", nullable: false),
                    SessionID = table.Column<string>(type: "TEXT", nullable: false),
                    AskerUID = table.Column<string>(type: "TEXT", nullable: false),
                    Votes = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Users_AskerUID",
                        column: x => x.AskerUID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionAttendees",
                columns: table => new
                {
                    AttendedSessionsSessionID = table.Column<string>(type: "TEXT", nullable: false),
                    SessionAttendeesUID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAttendees", x => new { x.AttendedSessionsSessionID, x.SessionAttendeesUID });
                    table.ForeignKey(
                        name: "FK_SessionAttendees_Sessions_AttendedSessionsSessionID",
                        column: x => x.AttendedSessionsSessionID,
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionAttendees_Users_SessionAttendeesUID",
                        column: x => x.SessionAttendeesUID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionModerators",
                columns: table => new
                {
                    ModeratedSessionsSessionID = table.Column<string>(type: "TEXT", nullable: false),
                    SessionModeratorsUID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionModerators", x => new { x.ModeratedSessionsSessionID, x.SessionModeratorsUID });
                    table.ForeignKey(
                        name: "FK_SessionModerators_Sessions_ModeratedSessionsSessionID",
                        column: x => x.ModeratedSessionsSessionID,
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionModerators_Users_SessionModeratorsUID",
                        column: x => x.SessionModeratorsUID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AskerUID",
                table: "Questions",
                column: "AskerUID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SessionID",
                table: "Questions",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionAttendees_SessionAttendeesUID",
                table: "SessionAttendees",
                column: "SessionAttendeesUID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionModerators_SessionModeratorsUID",
                table: "SessionModerators",
                column: "SessionModeratorsUID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionAdminUID",
                table: "Sessions",
                column: "SessionAdminUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "SessionAttendees");

            migrationBuilder.DropTable(
                name: "SessionModerators");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
