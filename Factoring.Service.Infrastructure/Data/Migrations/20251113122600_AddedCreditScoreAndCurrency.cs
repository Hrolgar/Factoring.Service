using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factoring.Service.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreditScoreAndCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Invoices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditScore",
                table: "Customers",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreditScore",
                table: "Customers");
        }
    }
}
