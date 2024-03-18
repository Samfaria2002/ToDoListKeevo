using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoListKeevo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Prazo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "Id", "Nome", "Prazo", "Status", "Tipo" },
                values: new object[,]
                {
                    { 1, "Estudar C#", new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), 1, 0 },
                    { 2, "Estudar PHP", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 0 },
                    { 3, "Estudar Angular", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 0 },
                    { 4, "Comprar leite", new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Utc), 2, 3 },
                    { 5, "Limpar a casa", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 4 },
                    { 6, "Cinema com amigos", new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2 },
                    { 7, "Consertar o teclado", new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Utc), 0, 4 },
                    { 8, "Subir para o docker", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 9, "Fazer o almoço", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 4 },
                    { 10, "Aula quinta a noite", new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc), 0, 0 },
                    { 11, "Trocar a lente", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 4 },
                    { 12, "Comprar farinha", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");
        }
    }
}
