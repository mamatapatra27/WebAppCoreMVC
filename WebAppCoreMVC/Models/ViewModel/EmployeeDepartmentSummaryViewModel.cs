using System.ComponentModel.DataAnnotations;

namespace WebAppCoreMVC.Models.ViewModel
{
	public class EmployeeDepartmentSummaryViewModel
	{
        [Display(Name = "Employee Id : ")]

        public int EmployeeId { get; set; }
        [Display(Name = "Department Id : ")]
        [Required(ErrorMessage ="Please select Department")]
        public int DepartmentId { get; set; }

		[Display(Name ="First Name : ")]
        [Required(ErrorMessage ="Please enter First name")]
		public string FirstName { get; set; } = default!;
        [Display(Name = "Middle Name : ")]
        public string? MiddleName { get; set; }
        [Display(Name = "Last Name : ")]
        [Required(ErrorMessage = "Please enter Last name")]
        public string? LastName { get; set; }
        // concatinate the name
        [Display(Name = "Name : ")]
        public string FullName
		{
			get
			{
				return FirstName + " " + MiddleName + " " + LastName;
			}
		}
        [Display(Name = "Gender : ")]
        [Required(ErrorMessage = "Please enter Gender")]
        public string? Gender { get; set; }
        [Display(Name = "Department : ")]
       
        public string DepartmentName { get; set; } = default!;
        [Display(Name = "Department Code : ")]
        public string DepartmentCode { get; set; } = default!;
	}
}
