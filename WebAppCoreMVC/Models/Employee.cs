using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCoreMVC.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Required")]
        public string FirstName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }

        public int DepartmentId {  get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; } = default!;
    }
}
