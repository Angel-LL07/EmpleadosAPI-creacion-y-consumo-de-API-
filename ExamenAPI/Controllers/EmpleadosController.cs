using AutoMapper;
using Examen.Aplicacion.DTOs;
using Examen.Dominio;
using Examen.Persistencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExamenAPI.Controllers
{
    [Route("api/empleados")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private Api_context _context;
        private IMapper _mapper;

        public EmpleadosController(Api_context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("Lista")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Lista()
        {
            var empleado = _context.examen_Empleados
                .FromSqlInterpolated($"EXEC dbo.examen_sp_ListaEmpleados")
                .AsAsyncEnumerable();
            var lista = new List<ExamenEmpleadoVM>();
            await foreach (var item in empleado)
            {
                lista.Add(_mapper.Map<ExamenEmpleadoVM>(item));
            }
            return Ok(lista);
        }

		[HttpGet("GetEmpleado/{Id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> GetEmpleado(int Id)
		{
            var empleado = _context.examen_Empleados.Find(Id);
            if(empleado != null)
            {
				var muestraEmpleado = _mapper.Map<Examen_Empleado, ExamenEmpleadoVM>(empleado);
				return Ok(muestraEmpleado);
			}
            return NotFound();
		}

		[HttpPost("Agregar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Agregar([FromBody] ExamenEmpleadoAgregarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existe = await _context.examen_Empleados.FirstOrDefaultAsync(c => c.Nombre.ToLower() == model.Nombre.ToLower() 
                                                                            && c.FechaNacimiento==model.FechaNacimiento);
            if (existe != null)
            {
                ModelState.AddModelError("", $"La Empleado {model.Nombre} ya esta registrado");
                return BadRequest(ModelState);
            }
            var ParametroId = new SqlParameter("@id", SqlDbType.Int);
            ParametroId.Direction = ParameterDirection.Output;
            await _context.Database
                  .ExecuteSqlInterpolatedAsync($@"EXEC examen_sp_inserccion_Empleado
                                                        @nombre={model.Nombre},
                                                        @apellidoPat={model.ApellidoPaterno},
                                                        @apellidoMat={model.ApellidoPaterno},
                                                        @telefono ={model.Telefono},
                                                        @sexo ={model.Sexo},
                                                        @fechaNac ={model.FechaNacimiento},
                                                        @area ={model.AreaId},
                                                        @id = {ParametroId} OUTPUT");
            var id = (int)ParametroId.Value;
            return Ok(id);
        }

        [HttpPatch("Actualiza")]
        [ProducesResponseType(200, Type = typeof(ExamenEmpleadoAgregarVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Actualiza( [FromBody] ExamenEmpleadoAgregarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var area = _context.examen_Areas.Find(model.AreaId);
            if (area == null) { return StatusCode(404, "El Area no Existe"); }
            await _context.Database
                   .ExecuteSqlInterpolatedAsync($@"EXEC examen_sp_actualizacion_Empleado
                                                        @nombre={model.Nombre},
                                                        @apellidoPat={model.ApellidoPaterno},
                                                        @apellidoMat={model.ApellidoPaterno},
                                                        @telefono ={model.Telefono},
                                                        @sexo ={model.Sexo},
                                                        @fechaNac ={model.FechaNacimiento},
                                                        @area ={model.AreaId},
                                                        @id = {model.Id}");
            return Ok();
        }

    }
}
