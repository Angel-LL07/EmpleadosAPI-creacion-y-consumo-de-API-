using AplicacionExamen.Models;

namespace AplicacionExamen.Servicios
{
    public interface IServiceAPI
    {
        Task<List<Empleado>> Listar();
        Task<bool> Registrar(Empleado modelo);
        Task<Empleado> Obtener(int Id);
        Task<bool> Editar(Empleado modelo);
    }
}
