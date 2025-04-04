﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentRec.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientAndProcedureTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedDuration",
                table: "Procedures");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Patients",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EstimatedDuration",
                table: "Procedures",
                type: "time",
                nullable: true);
        }
    }
}
