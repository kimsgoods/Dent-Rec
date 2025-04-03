using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentRec.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCostColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Procedures",
                newName: "Fee");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "PatientProcedures",
                newName: "Fee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fee",
                table: "Procedures",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Fee",
                table: "PatientProcedures",
                newName: "Cost");
        }
    }
}
