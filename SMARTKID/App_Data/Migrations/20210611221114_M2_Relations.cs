using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMARTKID.App_Data.Migrations
{
    public partial class M2_Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Teacher",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Kid",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Gendre",
                table: "AspNetUsers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "unset",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CIN",
                table: "AspNetUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Inscription",
                columns: table => new
                {
                    KidID = table.Column<int>(type: "int", nullable: false),
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InscriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscription_AppUser_Kid", x => new { x.KidID, x.AppUserID });
                    table.ForeignKey(
                        name: "FK_Inscription_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscription_Kid_KidID",
                        column: x => x.KidID,
                        principalTable: "Kid",
                        principalColumn: "KidID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherKid",
                columns: table => new
                {
                    KidID = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherKid", x => new { x.KidID, x.TeacherID });
                    table.ForeignKey(
                        name: "FK_TeacherKid_Kid_KidID",
                        column: x => x.KidID,
                        principalTable: "Kid",
                        principalColumn: "KidID");
                    table.ForeignKey(
                        name: "FK_TeacherKid_Teacher_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kid_AppUserId",
                table: "Kid",
                column: "AppUserId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AppUser_Gendre",
                table: "AspNetUsers",
                sql: "[Gendre] IN ('Female', 'Male')");

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_AppUserID",
                table: "Inscription",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherKid_TeacherID",
                table: "TeacherKid",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kid_AspNetUsers_AppUserId",
                table: "Kid",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kid_AspNetUsers_AppUserId",
                table: "Kid");

            migrationBuilder.DropTable(
                name: "Inscription");

            migrationBuilder.DropTable(
                name: "TeacherKid");

            migrationBuilder.DropIndex(
                name: "IX_Kid_AppUserId",
                table: "Kid");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AppUser_Gendre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Kid");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Teacher",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Gendre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldDefaultValue: "unset");

            migrationBuilder.AlterColumn<string>(
                name: "CIN",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);
        }
    }
}
