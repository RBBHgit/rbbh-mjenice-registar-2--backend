using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raiffeisen.RegistarMjenica.Api.Services.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMjenicaPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuarantorMjenicaName",
                table: "Mjenica",
                newName: "GuarantorMjenicaSerialNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuarantorMjenicaSerialNumber",
                table: "Mjenica",
                newName: "GuarantorMjenicaName");
        }
    }
}
