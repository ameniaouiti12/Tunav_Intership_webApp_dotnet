using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Tunav.Migrations
{
    /// <inheritdoc />
    public partial class t2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiensComptes");

            migrationBuilder.AddColumn<string>(
                name: "Lien",
                table: "Comptes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Comptes",
                keyColumn: "CompteId",
                keyValue: 1,
                column: "Lien",
                value: "GPS1");

            migrationBuilder.UpdateData(
                table: "Comptes",
                keyColumn: "CompteId",
                keyValue: 2,
                column: "Lien",
                value: "GPS1");

            migrationBuilder.UpdateData(
                table: "Comptes",
                keyColumn: "CompteId",
                keyValue: 3,
                column: "Lien",
                value: "GPS1");

            migrationBuilder.UpdateData(
                table: "Comptes",
                keyColumn: "CompteId",
                keyValue: 6,
                column: "Lien",
                value: "GPS1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lien",
                table: "Comptes");

            migrationBuilder.CreateTable(
                name: "LiensComptes",
                columns: table => new
                {
                    LienCompteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompteFK = table.Column<int>(type: "int", nullable: false),
                    DateClicked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lien = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiensComptes", x => x.LienCompteId);
                    table.ForeignKey(
                        name: "FK_LiensComptes_Comptes_CompteFK",
                        column: x => x.CompteFK,
                        principalTable: "Comptes",
                        principalColumn: "CompteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiensComptes_CompteFK",
                table: "LiensComptes",
                column: "CompteFK");
        }
    }
}
