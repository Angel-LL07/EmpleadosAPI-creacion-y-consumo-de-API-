using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Examen.Aplicacion.DTOs
{
    public class ExamenEmpleadoVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "30 caracteres Maximo")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido Paterno")]
        [MaxLength(30, ErrorMessage = "30 caracteres Maximo")]
        public string ApellidoPaterno { get; set; }
        [Required]
        [Display(Name = "Apellido Materno")]
        [MaxLength(30, ErrorMessage = "30 caracteres Maximo")]
        public string ApellidoMaterno { get; set; }
        [Required]
        [MaxLength(13, ErrorMessage = "13 caracteres Máximo")]
        public string Telefono { get; set; }
        [Required]
        [MaxLength(1,ErrorMessage ="Ingrese 'M' o 'F'")]
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int AreaId { get; set; }
    }
}
