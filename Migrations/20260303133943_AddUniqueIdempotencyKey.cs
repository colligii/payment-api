using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payment_api.Migrations
{
    public partial class AddUniqueIdempotencyKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Payment",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Payment",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "paymentType",
                table: "Payment",
                newName: "PaymentType");

            migrationBuilder.RenameColumn(
                name: "idempotencyKey",
                table: "Payment",
                newName: "IdempotencyKey");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdempotencyKey",
                table: "Payment",
                column: "IdempotencyKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payment_IdempotencyKey",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payment",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Payment",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Payment",
                newName: "paymentType");

            migrationBuilder.RenameColumn(
                name: "IdempotencyKey",
                table: "Payment",
                newName: "idempotencyKey");
        }
    }
}
