using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogNET8.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnsweredMsj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Answered",
                table: "MsjContact",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answered",
                table: "MsjContact");
        }
    }
}
