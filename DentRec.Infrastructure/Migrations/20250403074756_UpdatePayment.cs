using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentRec.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PatientLogs_PatientProcedureId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PatientProcedureId",
                table: "Payments",
                newName: "PatientLogId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PatientProcedureId",
                table: "Payments",
                newName: "IX_Payments_PatientLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PatientLogs_PatientLogId",
                table: "Payments",
                column: "PatientLogId",
                principalTable: "PatientLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PatientLogs_PatientLogId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PatientLogId",
                table: "Payments",
                newName: "PatientProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PatientLogId",
                table: "Payments",
                newName: "IX_Payments_PatientProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PatientLogs_PatientProcedureId",
                table: "Payments",
                column: "PatientProcedureId",
                principalTable: "PatientLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
