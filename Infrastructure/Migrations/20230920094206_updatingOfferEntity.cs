using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updatingOfferEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_VendorId",
                table: "Offers",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Vendors_VendorId",
                table: "Offers",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Vendors_VendorId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_VendorId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Offers");
        }
    }
}
