using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_core_Assignment.Migrations
{
    public partial class NinthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teachers_courses_CourseId",
                table: "teachers");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_courses_CourseId",
                table: "teachers",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "courseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teachers_courses_CourseId",
                table: "teachers");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_courses_CourseId",
                table: "teachers",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "courseId");
        }
    }
}
