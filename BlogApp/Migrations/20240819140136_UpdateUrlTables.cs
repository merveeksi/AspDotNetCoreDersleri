using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublisedOn",
                table: "Posts",
                newName: "PublishedOn");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tags",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Posts",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PublishedOn",
                table: "Posts",
                newName: "PublisedOn");
        }
    }
}
