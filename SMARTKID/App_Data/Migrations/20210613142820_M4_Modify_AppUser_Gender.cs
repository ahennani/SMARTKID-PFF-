using Microsoft.EntityFrameworkCore.Migrations;

namespace SMARTKID.App_Data.Migrations
{
    public partial class M4_Modify_AppUser_Gender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_AppUser_Gendre",
                table: "AspNetUsers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AppUser_Gendre",
                table: "AspNetUsers",
                sql: "[Gendre] IN ('Female', 'Male', 'unset')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_AppUser_Gendre",
                table: "AspNetUsers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AppUser_Gendre",
                table: "AspNetUsers",
                sql: "[Gendre] IN ('Female', 'Male')");
        }
    }
}
