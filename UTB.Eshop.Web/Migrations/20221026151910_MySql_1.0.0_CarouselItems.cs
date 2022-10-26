using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTB.Eshop.Web.Migrations
{
    public partial class MySql_100_CarouselItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarouselItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImageSrc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageAlt = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselItem", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CarouselItem",
                columns: new[] { "ID", "ImageAlt", "ImageSrc" },
                values: new object[] { 1, "First slide", "/img/carousel/information-technology-1.jpg" });

            migrationBuilder.InsertData(
                table: "CarouselItem",
                columns: new[] { "ID", "ImageAlt", "ImageSrc" },
                values: new object[] { 2, "Second slide", "/img/carousel/how-to-become-an-information-technology-specialist160684886950141.jpg" });

            migrationBuilder.InsertData(
                table: "CarouselItem",
                columns: new[] { "ID", "ImageAlt", "ImageSrc" },
                values: new object[] { 3, "Third slide", "/img/carousel/1581481407499.jpeg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarouselItem");
        }
    }
}
