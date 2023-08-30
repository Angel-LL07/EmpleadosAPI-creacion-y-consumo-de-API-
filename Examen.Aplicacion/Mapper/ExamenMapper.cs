using AutoMapper;
using Examen.Aplicacion.DTOs;
using Examen.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Aplicacion.Mapper
{
    public class ExamenMapper:Profile
    {
        public ExamenMapper()
        {
            CreateMap<Examen_Area, Examen_AreaVM>().ReverseMap();
            CreateMap<Examen_Area, Examen_AreaAgregarVM>().ReverseMap();
            CreateMap<Examen_Empleado, ExamenEmpleadoVM>().ReverseMap();
            CreateMap<Examen_Empleado, ExamenEmpleadoAgregarVM>().ReverseMap();
        }
    }
}
