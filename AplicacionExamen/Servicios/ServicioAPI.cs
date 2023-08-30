using AplicacionExamen.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AplicacionExamen.Servicios
{
    public class ServicioAPI :IServiceAPI
    {
        private static string _usuario;
        private static string _clave;
        private static string _baseurl;
        private static string _token;

        public ServicioAPI()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:BaseUrl").Value;
        }



        public async Task<List<Empleado>> Listar()
        {
            List<Empleado> lista = new();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.GetAsync("api/empleados/Lista");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Empleado>>(json_respuesta);
                return resultado;
            }
            return lista;
        }

        public async Task<bool> Registrar(Empleado modelo)
        {
            bool respuesta = false;


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(modelo),Encoding.UTF8,"application/json");

            var response = await cliente.PostAsync($"api/empleados/Agregar/",content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Editar(Empleado modelo)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(modelo), Encoding.UTF8, "application/json");

            var response = await cliente.PatchAsync($"api/empleados/Actualiza/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

		public async Task<Empleado> Obtener(int Id)
		{
			Empleado empleado = new Empleado();
			var cliente = new HttpClient();
			cliente.BaseAddress = new Uri(_baseurl);
			var response = await cliente.GetAsync($"api/empleados/GetEmpleado/{Id}");

			if (response.IsSuccessStatusCode)
			{
				var json_respuesta = await response.Content.ReadAsStringAsync();
				var resultado = JsonConvert.DeserializeObject<Empleado>(json_respuesta);
				//empleado = resultado.Objeto;
				return resultado;
			}
			return empleado;
		}
	}
}
