using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raiffeisen.RegistarMjenica.Api.Services.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttributeNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "exemptionReason",
                table: "Mjenica",
                newName: "ExemptionReason");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExemptionReason",
                table: "Mjenica",
                newName: "exemptionReason");
        }
    }
}
