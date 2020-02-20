using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothsStore.DAL.Migrations
{
    public partial class addingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productId",
                table: "CartItem",
                newName: "productid");

            migrationBuilder.AlterColumn<int>(
                name: "productid",
                table: "CartItem",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "CartItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_productid",
                table: "CartItem",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_userid",
                table: "CartItem",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Product_productid",
                table: "CartItem",
                column: "productid",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_User_userid",
                table: "CartItem",
                column: "userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Product_productid",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_User_userid",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_productid",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_userid",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "CartItem",
                newName: "productId");

            migrationBuilder.AlterColumn<long>(
                name: "productId",
                table: "CartItem",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
