using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSessionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_SessionAdminUID",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "SessionAttendees");

            migrationBuilder.DropTable(
                name: "SessionModerators");

            migrationBuilder.CreateTable(
                name: "UserSession",
                columns: table => new
                {
                    UID = table.Column<string>(type: "TEXT", nullable: false),
                    SessionID = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => new { x.UID, x.SessionID });
                    table.ForeignKey(
                        name: "FK_UserSession_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSession_Users_UID",
                        column: x => x.UID,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_SessionID",
                table: "UserSession",
                column: "SessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_SessionAdminUID",
                table: "Sessions",
                column: "SessionAdminUID",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_SessionAdminUID",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "UserSession");

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
                name: "IX_SessionAttendees_SessionAttendeesUID",
                table: "SessionAttendees",
                column: "SessionAttendeesUID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionModerators_SessionModeratorsUID",
                table: "SessionModerators",
                column: "SessionModeratorsUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_SessionAdminUID",
                table: "Sessions",
                column: "SessionAdminUID",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
