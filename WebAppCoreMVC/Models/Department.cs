using System.ComponentModel.DataAnnotations;

namespace WebAppCoreMVC.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        [Required(ErrorMessage ="Please Enter Department Name")]
        public string DepartmentName { get; set; } = default!;
        [Display(Name = "Department Code")]
        [Required(ErrorMessage = "Please Enter Department Code")]
        public string DepartmentCode { get; set;} = default!;
    }
}
