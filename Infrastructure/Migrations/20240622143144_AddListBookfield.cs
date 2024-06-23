using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddListBookfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Carts_CartId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CartId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BookCart",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "INTEGER", nullable: false),
                    CartsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCart", x => new { x.BooksId, x.CartsId });
                    table.ForeignKey(
                        name: "FK_BookCart_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCart_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCart_CartsId",
                table: "BookCart",
                column: "CartsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Books",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CartId",
                table: "Books",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Carts_CartId",
                table: "Books",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
