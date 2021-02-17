using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EventFinder.Infra.Data.Migrations
{
    public partial class RemovendoIdClassesRelacionamentoNparaN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Publicacoes",
                table: "Publicacoes");

            migrationBuilder.DropIndex(
                name: "IX_Publicacoes_ClienteId",
                table: "Publicacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissoesUsuariosFuncoes",
                table: "PermissoesUsuariosFuncoes");

            migrationBuilder.DropIndex(
                name: "IX_PermissoesUsuariosFuncoes_FuncaoId",
                table: "PermissoesUsuariosFuncoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MensagensOrganizacoesEventos",
                table: "MensagensOrganizacoesEventos");

            migrationBuilder.DropIndex(
                name: "IX_MensagensOrganizacoesEventos_ClienteId",
                table: "MensagensOrganizacoesEventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_ClienteId",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Publicacoes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PermissoesUsuariosFuncoes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MensagensOrganizacoesEventos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Avaliacoes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publicacoes",
                table: "Publicacoes",
                columns: new[] { "ClienteId", "EventoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissoesUsuariosFuncoes",
                table: "PermissoesUsuariosFuncoes",
                columns: new[] { "FuncaoId", "UsuarioId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MensagensOrganizacoesEventos",
                table: "MensagensOrganizacoesEventos",
                columns: new[] { "ClienteId", "EmpresaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes",
                columns: new[] { "ClienteId", "EventoId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Publicacoes",
                table: "Publicacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissoesUsuariosFuncoes",
                table: "PermissoesUsuariosFuncoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MensagensOrganizacoesEventos",
                table: "MensagensOrganizacoesEventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Publicacoes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PermissoesUsuariosFuncoes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MensagensOrganizacoesEventos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Avaliacoes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publicacoes",
                table: "Publicacoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissoesUsuariosFuncoes",
                table: "PermissoesUsuariosFuncoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MensagensOrganizacoesEventos",
                table: "MensagensOrganizacoesEventos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacoes_ClienteId",
                table: "Publicacoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissoesUsuariosFuncoes_FuncaoId",
                table: "PermissoesUsuariosFuncoes",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagensOrganizacoesEventos_ClienteId",
                table: "MensagensOrganizacoesEventos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_ClienteId",
                table: "Avaliacoes",
                column: "ClienteId");
        }
    }
}
