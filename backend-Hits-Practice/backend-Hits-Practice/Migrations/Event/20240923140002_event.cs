using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_Hits_Practice.Migrations
{
    /// <inheritdoc />
    public partial class @event : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventDbModel_Users_EmployeeId",
                table: "EventDbModel");

            migrationBuilder.DropTable(
                name: "EventDbModelStudentDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventDbModel",
                table: "EventDbModel");

            migrationBuilder.RenameTable(
                name: "EventDbModel",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_EventDbModel_EmployeeId",
                table: "Events",
                newName: "IX_Events_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StudentEvent",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvent", x => new { x.EventId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEvent_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvent_StudentId",
                table: "StudentEvent",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_EmployeeId",
                table: "Events",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_EmployeeId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "StudentEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "EventDbModel");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EmployeeId",
                table: "EventDbModel",
                newName: "IX_EventDbModel_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventDbModel",
                table: "EventDbModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EventDbModelStudentDbModel",
                columns: table => new
                {
                    EventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDbModelStudentDbModel", x => new { x.EventsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_EventDbModelStudentDbModel_EventDbModel_EventsId",
                        column: x => x.EventsId,
                        principalTable: "EventDbModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventDbModelStudentDbModel_Users_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventDbModelStudentDbModel_StudentsId",
                table: "EventDbModelStudentDbModel",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventDbModel_Users_EmployeeId",
                table: "EventDbModel",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
