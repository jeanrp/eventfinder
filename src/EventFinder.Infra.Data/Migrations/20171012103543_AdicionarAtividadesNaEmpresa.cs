using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EventFinder.Infra.Data.Migrations
{
    public partial class AdicionarAtividadesNaEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Atividades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_EmpresaId",
                table: "Atividades",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Empresas_EmpresaId",
                table: "Atividades",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Empresas_EmpresaId",
                table: "Atividades");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_EmpresaId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Atividades");
        }
    }
}
