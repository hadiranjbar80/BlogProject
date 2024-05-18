using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Repository.Migrations
{
    /// <inheritdoc />
    public partial class fixcommentrelationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserName",
                table: "ArticleComments");

            migrationBuilder.DropIndex(
                name: "IX_ArticleComments_UserName",
                table: "ArticleComments");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ArticleComments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ArticleComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_UserId",
                table: "ArticleComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserId",
                table: "ArticleComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserId",
                table: "ArticleComments");

            migrationBuilder.DropIndex(
                name: "IX_ArticleComments_UserId",
                table: "ArticleComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ArticleComments");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ArticleComments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_UserName",
                table: "ArticleComments",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_UserName",
                table: "ArticleComments",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
