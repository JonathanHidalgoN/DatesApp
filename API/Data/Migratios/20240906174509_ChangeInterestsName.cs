using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migratios
{
    /// <inheritdoc />
    public partial class ChangeInterestsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "interest",
                table: "Users",
                newName: "interests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "interests",
                table: "Users",
                newName: "interest");
        }
    }
}
