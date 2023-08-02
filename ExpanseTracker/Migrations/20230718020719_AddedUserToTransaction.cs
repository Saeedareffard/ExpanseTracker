using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpanseTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpanseTransaction_Category_Id",
                table: "ExpanseTransaction");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ExpanseTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExpanseTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpanseTransaction_CategoryId",
                table: "ExpanseTransaction",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpanseTransaction_Category_CategoryId",
                table: "ExpanseTransaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpanseTransaction_Category_CategoryId",
                table: "ExpanseTransaction");

            migrationBuilder.DropIndex(
                name: "IX_ExpanseTransaction_CategoryId",
                table: "ExpanseTransaction");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ExpanseTransaction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExpanseTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpanseTransaction_Category_Id",
                table: "ExpanseTransaction",
                column: "Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
