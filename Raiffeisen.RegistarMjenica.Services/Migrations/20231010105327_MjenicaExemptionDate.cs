using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raiffeisen.RegistarMjenica.Api.Services.Migrations
{
    /// <inheritdoc />
    public partial class MjenicaExemptionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExemptionDate",
                table: "Mjenica",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExemptionDate",
                table: "Mjenica");
        }
    }
}
