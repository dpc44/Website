using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class Update3TablesagainIUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesWebsite_UsersWebiste_UserId",
                table: "ArticlesWebsite");

            migrationBuilder.DropIndex(
                name: "IX_ArticlesWebsite_UserId",
                table: "ArticlesWebsite");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ArticlesWebsite_UserId",
                table: "ArticlesWebsite",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesWebsite_UsersWebiste_UserId",
                table: "ArticlesWebsite",
                column: "UserId",
                principalTable: "UsersWebiste",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
