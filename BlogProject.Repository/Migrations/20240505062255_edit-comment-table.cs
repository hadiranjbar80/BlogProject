using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editcommenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserId",
                table: "ArticleComments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ArticleComments",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_UserId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_UserName");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ArticleComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserName",
                table: "ArticleComments",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserName",
                table: "ArticleComments");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ArticleComments");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "ArticleComments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_UserName",
                table: "ArticleComments",
                newName: "IX_ArticleComments_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserId",
                table: "ArticleComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
