using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raiffeisen.RegistarMjenica.Api.Services.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mjenica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientMjenicaSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientMjenicaValidated = table.Column<bool>(type: "bit", nullable: false),
                    MjenicaValidator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorMjenicaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MjenicaExempted = table.Column<bool>(type: "bit", nullable: true),
                    FreeTextField = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exemptionReason = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mjenica", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mjenica");
        }
    }
}
