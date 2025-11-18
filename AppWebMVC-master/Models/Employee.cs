using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppProductsandCategories.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Contratación")]
        public DateTime? HireDate { get; set; }
    }
}
