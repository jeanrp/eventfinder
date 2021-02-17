using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EventFinder.Infra.Data.Migrations
{
    public partial class ChavesEstrangeirasEnderecoEmpresaEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Empresas_EmpresaId",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Eventos_EventoId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_EmpresaId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_EventoId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "EventoId",
                table: "Enderecos");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_EnderecoId",
                table: "Eventos",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_EnderecoId",
                table: "Empresas",
                column: "EnderecoId",
                unique: true,
                filter: "[EnderecoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Enderecos_EnderecoId",
                table: "Empresas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Enderecos_EnderecoId",
                table: "Eventos",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Enderecos_EnderecoId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Enderecos_EnderecoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_EnderecoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_EnderecoId",
                table: "Empresas");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Enderecos",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventoId",
                table: "Enderecos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_EmpresaId",
                table: "Enderecos",
                column: "EmpresaId",
                unique: true,
                filter: "[EmpresaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_EventoId",
                table: "Enderecos",
                column: "EventoId",
                unique: true,
                filter: "[EventoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Empresas_EmpresaId",
                table: "Enderecos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Eventos_EventoId",
                table: "Enderecos",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
