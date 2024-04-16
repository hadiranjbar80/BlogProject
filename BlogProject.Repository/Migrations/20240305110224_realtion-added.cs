using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Repository.Migrations
{
    /// <inheritdoc />
    public partial class realtionadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ArticleId",
                table: "Questions",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Articles_ArticleId",
                table: "Questions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Articles_ArticleId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ArticleId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Questions");
        }
    }
}
