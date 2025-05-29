using System;
using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevIO.Data.Migrations
{
    /// <inheritdoc />
    public partial class criacaotbloja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<BitArray>(
                name: "Ativo",
                table: "Produtos",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<BitArray>(
                name: "Ativo",
                table: "Fornecedores",
                type: "bit(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Centro = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Produtos",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(BitArray),
                oldType: "bit(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Fornecedores",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(BitArray),
                oldType: "bit(1)");
        }
    }
}
