using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EventFinder.Infra.Data.Migrations
{
    public partial class adicionandoSubDescricaoComoOpcional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Empresas_EmpresaId",
                table: "Atividades");

            migrationBuilder.AlterColumn<string>(
                name: "SubDescricao",
                table: "Eventos",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Empresas_EmpresaId",
                table: "Atividades",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Empresas_EmpresaId",
                table: "Atividades");

            migrationBuilder.AlterColumn<string>(
                name: "SubDescricao",
                table: "Eventos",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Empresas_EmpresaId",
                table: "Atividades",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
