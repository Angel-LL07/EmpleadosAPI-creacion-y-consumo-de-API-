using AutoMapper;
using Examen.Aplicacion.DTOs;
using Examen.Persistencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExamenAPI.Controllers
{
    [Route("api/area")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private Api_context _context;
        private IMapper _mapper;
        public AreasController(Api_context context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ListaAreas()
        {
            var categorias = _context.examen_Areas
                .FromSqlInterpolated($"EXEC dbo.examen_sp_ListaAreas")
                .AsAsyncEnumerable();
            var lista = new List<Examen_AreaVM>();
            await foreach (var item in categorias)
            {
                lista.Add(_mapper.Map<Examen_AreaVM>(item));
            }
            return Ok(lista);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AgregarArea([FromBody] Examen_AreaAgregarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existe = await _context.examen_Areas.FirstOrDefaultAsync(c => c.Nombre.ToLower() == model.Nombre.ToLower());
            if (existe != null)
            {
                ModelState.AddModelError("", $"La Area {model.Nombre} ya existe");
                return BadRequest(ModelState);
            }
            var ParametroId = new SqlParameter("@id", SqlDbType.Int);
            ParametroId.Direction = ParameterDirection.Output;
            await _context.Database
                  .ExecuteSqlInterpolatedAsync($@"EXEC examen_sp_inserccion_Area
                                                        @nombre={model.Nombre},@id = {ParametroId} OUTPUT");
            var id = (int)ParametroId.Value;
            return Ok(id);
        }

        [HttpPatch("{AreaId:int}", Name = "ActualizaArea")]
        [ProducesResponseType(200, Type = typeof(Examen_AreaAgregarVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ActualizaArea(int AreaId, [FromBody] Examen_AreaAgregarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Database
                .ExecuteSqlInterpolatedAsync($"EXEC examen_sp_actualizacion_Area @id = {AreaId},@nombre={model.Nombre}");
            return Ok();
        }
    }
}
