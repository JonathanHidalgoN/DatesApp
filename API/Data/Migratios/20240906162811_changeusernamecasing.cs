using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migratios
{
    /// <inheritdoc />
    public partial class changeusernamecasing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Users",
                newName: "username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "userName");
        }
    }
}
