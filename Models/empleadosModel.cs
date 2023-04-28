using System.ComponentModel.DataAnnotations;

namespace TEST_CRUD.Models
{
    public class empleadosModel
    {
        public int empleadoId { get; set; }

        [Required(ErrorMessage ="El campo nombres es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo letras")]
        public string NPersonal { get; set; } = null!;
		
		[Required(ErrorMessage ="El campo apellidos es obligatorio")]
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo letras")]
        public string APersonal { get; set; } = null!;
        
        [Required(ErrorMessage ="El campo fecha de nacimiento es obligatorio")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime),"1/1/1938","1/1/2005", ErrorMessage = "La fecha de nacimiento no permitidad, la fecha deben estar en el rango de 1/1/1938 y 1/1/2005 ")]
        public DateTime FN { get; set; }

        [Required(ErrorMessage ="El campo fecha de ingreso es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime FI { get; set; }

        [Required(ErrorMessage = "El campo direccion es obligatorio")]
        public string afp { get; set; } = null!;
		
		[Required(ErrorMessage = "El campo DNI es obligatorio")]
		[RegularExpression(@"^[0-9]$", ErrorMessage = "DNI debe de tener solo(8) numeros")]
		public double sueldo { get; set; }
		
		
		
		[Required(ErrorMessage = "El campo Rol es obligatorio")]
		public int RolesId { get; set; }
		
		public string NRoles { get; set; }
		
		public string Descripcion { get; set; }
    }
}
		
