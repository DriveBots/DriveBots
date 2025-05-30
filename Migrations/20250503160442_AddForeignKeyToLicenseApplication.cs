﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveBots.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToLicenseApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LicenseApplications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseApplications_UserId",
                table: "LicenseApplications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseApplications_AspNetUsers_UserId",
                table: "LicenseApplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseApplications_AspNetUsers_UserId",
                table: "LicenseApplications");

            migrationBuilder.DropIndex(
                name: "IX_LicenseApplications_UserId",
                table: "LicenseApplications");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LicenseApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
