using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstAngularAPIAssignment.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStringDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SkillExperience",
                table: "skill",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SkillExperience",
                table: "skill",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
