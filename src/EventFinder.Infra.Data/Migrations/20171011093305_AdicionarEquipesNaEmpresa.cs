using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EventFinder.Infra.Data.Migrations
{
    public partial class AdicionarEquipesNaEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Equipes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_EmpresaId",
                table: "Equipes",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_Empresas_EmpresaId",
                table: "Equipes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipes_Empresas_EmpresaId",
                table: "Equipes");

            migrationBuilder.DropIndex(
                name: "IX_Equipes_EmpresaId",
                table: "Equipes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Equipes");
        }
    }
}
