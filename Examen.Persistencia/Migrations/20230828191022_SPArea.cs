using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Persistencia.Migrations
{
    public partial class SPArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE examen_sp_ListaAreas
                AS
                BEGIN
                SELECT Id,Nombre FROM examen_Areas;
                END
            ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE examen_sp_ListaEmpleados
                AS
                BEGIN
                SELECT * FROM examen_Empleados;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE examen_sp_inserccion_Area
                @nombre nvarchar(30),
                @id int OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                        INSERT INTO examen_Areas(Nombre)
                        VALUES(@nombre)
                    SELECT @id =  SCOPE_IDENTITY()
                END
            ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE examen_sp_actualizacion_Area
                @nombre nvarchar(30),
                @id int
                AS
                BEGIN
                        UPDATE  examen_Areas SET Nombre=@nombre WHERE Id=@id
                END
            ");


            migrationBuilder.Sql(@"
                CREATE PROCEDURE examen_sp_inserccion_Empleado
                @nombre nvarchar(30),
                @apellidoPat nvarchar(30),
                @apellidoMat nvarchar(30),
                @telefono nvarchar(13),
                @sexo nvarchar(1),
                @fechaNac datetime2(7),
                @area int,
                @id int OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                        INSERT INTO examen_Empleados(Nombre,ApellidoPaterno,ApellidoMaterno,Telefono,Sexo,FechaNacimiento,AreaId)
                        VALUES(@nombre, @apellidoPat,@apellidoMat,@telefono, @sexo,@fechaNac,@area)
                    SELECT @id =  SCOPE_IDENTITY()
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE examen_sp_actualizacion_Empleado
                @nombre nvarchar(30),
                @apellidoPat nvarchar(30),
                @apellidoMat nvarchar(30),
                @telefono nvarchar(13),
                @sexo nvarchar(1),
                @fechaNac datetime2(7),
                @area int,
                @id int
                AS
                BEGIN
                        UPDATE  examen_Empleados SET Nombre=@nombre,ApellidoPaterno=@apellidoPat,ApellidoMaterno=@apellidoMat,Telefono=@telefono,Sexo=@sexo,FechaNacimiento=@fechaNac,AreaId=@area
                        WHERE Id=@id
                END
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE examen_sp_ListaAreas");
            migrationBuilder.Sql(@"DROP PROCEDURE examen_sp_ListaEmpleados");
            migrationBuilder.Sql(@"DROP PROCEDURE examen_sp_inserccion_Area");
            migrationBuilder.Sql(@"DROP PROCEDURE examen_sp_actualizacion_Area");
            migrationBuilder.Sql(@"DROP PROCEDURE examen_sp_inserccion_Empleado");
            migrationBuilder.Sql(@"DROP PROCEDURE examen_sp_actualizacion_Empleado");
        }
    }
}
