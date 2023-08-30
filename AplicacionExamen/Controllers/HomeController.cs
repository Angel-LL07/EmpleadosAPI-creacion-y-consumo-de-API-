using AplicacionExamen.Models;
using AplicacionExamen.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AplicacionExamen.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceAPI _serviceAPI;

        public HomeController(IServiceAPI serviceAPI)
        {
            _serviceAPI = serviceAPI;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           List<Empleado> lista = await _serviceAPI.Listar();
            return View(lista);
        }

        [HttpGet]
		public async Task<IActionResult> Empleado(int id)
		{
            ViewBag.Accion = "Nuevo Empleado";
            Empleado modelo= new Empleado();
            if(id !=0) 
            {
                modelo = await _serviceAPI.Obtener(id);
				ViewBag.Accion = "Editar Empleado";
                
                return View(modelo);
			}
			return View(modelo);
		}

        [HttpPost]
		public async Task<IActionResult> Empleado( Empleado model)
        {
            if(!ModelState.IsValid)
            {
				ViewBag.Accion = "Nuevo Empleado";

				return View(model);
            }
            bool respuesta;
            if (model.Id == 0 || model.Id ==null)
            {
                respuesta = await _serviceAPI.Registrar(model);
            }
            else
            {
                respuesta = await _serviceAPI.Editar(model);
            }

            if (respuesta)
                return RedirectToAction("Index");


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}