using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Persistencia.Migrations
{
    public partial class tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "examen_Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examen_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "examen_Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examen_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_examen_Empleados_examen_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "examen_Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_examen_Empleados_AreaId",
                table: "examen_Empleados",
                column: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "examen_Empleados");

            migrationBuilder.DropTable(
                name: "examen_Areas");
        }
    }
}
