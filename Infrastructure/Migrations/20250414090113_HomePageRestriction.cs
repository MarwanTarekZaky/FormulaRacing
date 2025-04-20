using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HomePageRestriction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HomePageContents",
                columns: new[] { "Id", "BannerText", "ImageParagraph", "RaceParagraph", "VideoPath" },
                values: new object[] { 1, "BannerText is empty", "ImageParagraph is empty", "RaceParagraph is empty", "VideoPath is empty" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_SingleRecord",
                table: "HomePageContents",
                sql: "Id = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_SingleRecord",
                table: "HomePageContents");

            migrationBuilder.DeleteData(
                table: "HomePageContents",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
