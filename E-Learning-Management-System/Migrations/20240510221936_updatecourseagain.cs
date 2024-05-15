using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class updatecourseagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DurationInHours",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "DurationInHours",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Course");
        }
    }
}
