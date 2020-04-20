using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_core_Assignment.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    courseId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.courseId);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    AuID = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: true),
                    email = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.AuID);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    AuID = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: true),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.AuID);
                    table.ForeignKey(
                        name: "FK_teachers_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "courseId");
                });

            migrationBuilder.CreateTable(
                name: "Attends_shadowtab",
                columns: table => new
                {
                    studentAuId = table.Column<int>(nullable: false),
                    courseId = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    semester = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attends_shadowtab", x => new { x.studentAuId, x.courseId });
                    table.ForeignKey(
                        name: "FK_Attends_shadowtab_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attends_shadowtab_students_studentAuId",
                        column: x => x.studentAuId,
                        principalTable: "students",
                        principalColumn: "AuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(nullable: false),
                    AssignmentName = table.Column<string>(nullable: true),
                    open = table.Column<bool>(nullable: false),
                    courseId = table.Column<int>(nullable: false),
                    teacherAuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_assignments_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignments_teachers_teacherAuId",
                        column: x => x.teacherAuId,
                        principalTable: "teachers",
                        principalColumn: "AuID");
                });

            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    number = table.Column<int>(nullable: false),
                    lecture = table.Column<string>(maxLength: 64, nullable: true),
                    help_where = table.Column<string>(maxLength: 128, nullable: true),
                    open = table.Column<bool>(nullable: false),
                    teacherAuId = table.Column<int>(nullable: false),
                    studentAuId = table.Column<int>(nullable: false),
                    courseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.number);
                    table.ForeignKey(
                        name: "FK_exercises_courses_courseID",
                        column: x => x.courseID,
                        principalTable: "courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercises_students_studentAuId",
                        column: x => x.studentAuId,
                        principalTable: "students",
                        principalColumn: "AuID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercises_teachers_teacherAuId",
                        column: x => x.teacherAuId,
                        principalTable: "teachers",
                        principalColumn: "AuID");
                });

            migrationBuilder.CreateTable(
                name: "HelpRequest_shadowtab",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    AssignmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpRequest_shadowtab", x => new { x.AssignmentId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_HelpRequest_shadowtab_assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HelpRequest_shadowtab_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "AuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignments_courseId",
                table: "assignments",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_assignments_teacherAuId",
                table: "assignments",
                column: "teacherAuId");

            migrationBuilder.CreateIndex(
                name: "IX_Attends_shadowtab_courseId",
                table: "Attends_shadowtab",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_exercises_courseID",
                table: "exercises",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_exercises_studentAuId",
                table: "exercises",
                column: "studentAuId");

            migrationBuilder.CreateIndex(
                name: "IX_exercises_teacherAuId",
                table: "exercises",
                column: "teacherAuId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequest_shadowtab_StudentId",
                table: "HelpRequest_shadowtab",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_CourseId",
                table: "teachers",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attends_shadowtab");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "HelpRequest_shadowtab");

            migrationBuilder.DropTable(
                name: "assignments");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
