using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EventFinder.Infra.Data.Migrations
{
    public partial class AdicionarIngressoCmpradoIdCorreto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IngressosComprados",
                table: "IngressosComprados");

            migrationBuilder.DropIndex(
                name: "IX_IngressosComprados_ClienteId",
                table: "IngressosComprados");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IngressosComprados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngressosComprados",
                table: "IngressosComprados",
                columns: new[] { "ClienteId", "IngressoId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IngressosComprados",
                table: "IngressosComprados");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "IngressosComprados",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngressosComprados",
                table: "IngressosComprados",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IngressosComprados_ClienteId",
                table: "IngressosComprados",
                column: "ClienteId");
        }
    }
}
