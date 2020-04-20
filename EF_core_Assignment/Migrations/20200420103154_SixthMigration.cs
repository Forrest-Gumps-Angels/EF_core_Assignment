using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_core_Assignment.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignments_teachers_teacherAuId",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_exercises_teachers_teacherAuId",
                table: "exercises");

            migrationBuilder.AddForeignKey(
                name: "FK_assignments_teachers_teacherAuId",
                table: "assignments",
                column: "teacherAuId",
                principalTable: "teachers",
                principalColumn: "AuID");

            migrationBuilder.AddForeignKey(
                name: "FK_exercises_teachers_teacherAuId",
                table: "exercises",
                column: "teacherAuId",
                principalTable: "teachers",
                principalColumn: "AuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignments_teachers_teacherAuId",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_exercises_teachers_teacherAuId",
                table: "exercises");

            migrationBuilder.AddForeignKey(
                name: "FK_assignments_teachers_teacherAuId",
                table: "assignments",
                column: "teacherAuId",
                principalTable: "teachers",
                principalColumn: "AuID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_exercises_teachers_teacherAuId",
                table: "exercises",
                column: "teacherAuId",
                principalTable: "teachers",
                principalColumn: "AuID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
