using Examen.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Persistencia
{
    public class Api_context :DbContext
    {
        public Api_context(DbContextOptions<Api_context>options):base(options)
        {
        }

        public DbSet<Examen_Area> examen_Areas { get; set; }
        public DbSet<Examen_Empleado> examen_Empleados { get; set; }
    }
}
