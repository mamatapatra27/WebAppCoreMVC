using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebAppCoreMVC.Data;
using WebAppCoreMVC.Models;
using WebAppCoreMVC.Models.ViewModel;

namespace WebAppCoreMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext context;

        public EmployeeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // method for First Letter in UpperCase
        private static string EveryFirstCharacterCapital(string input)
        {
            if (string.IsNullOrEmpty(input))
	    {
    		return input;
	    }

	    StringBuilder sb = new StringBuilder();

	    var data = input.Split(' ');
	    for(int i=0; i<data.Length; i++)
            {
    		if (!string.IsNullOrEmpty(data[i]))
    		{
        	     sb.Append(data[i].First().ToString().ToUpper() + data[i].Substring(1));
    		}
    		if(i < data.Length - 1)
    		{
        	     sb.Append(" ");
    		}
    		//sb.Remove(sb.Length - 1, 1);
	    }
	    return sb.ToString();
        }

       
        public IActionResult Index()
        {
            //USING MERGE MODEL

            //EmployeeDepartmentListViewModel emp = new EmployeeDepartmentListViewModel();

            //var empData = context.Employees.ToList();
            //var deptData = context.Departments.ToList();
            //emp.Employees = empData;
            //emp.Departments = deptData;

            //return View(emp);


            //USING JOIN MODEL
            var data = (from e in context.Employees
                       join d in context.Departments
                       on e.DepartmentId equals d.DepartmentId
                       select new EmployeeDepartmentSummaryViewModel
                       {
                           EmployeeId = e.EmployeeId,
						   //FirstName = e.FirstName,
						   //MiddleName = e.MiddleName,
						   //LastName = e.LastName,
						   //Gender = e.Gender,
						   //DepartmentName = d.DepartmentName,
						   //DepartmentCode = d.DepartmentCode,

						   // Replace that with EveryFirstCharacterCapital Method
						   FirstName = EveryFirstCharacterCapital(e.FirstName),
                           MiddleName = EveryFirstCharacterCapital(e.MiddleName),
                           LastName = EveryFirstCharacterCapital(e.LastName),
                           Gender = EveryFirstCharacterCapital(e.Gender),
                           DepartmentCode = d.DepartmentCode.ToUpper(),
                           DepartmentName = EveryFirstCharacterCapital(d.DepartmentName)
                           
                       }).ToList();


            return View(data);
        }


        // Add Get
        public IActionResult AddEmployee(int id)
        {
            
            ViewBag.department = context.Departments.ToList();
            EmployeeDepartmentSummaryViewModel employeeDepartment = new EmployeeDepartmentSummaryViewModel();
            try
            {
                if (id == 0)
                {
                    return View(employeeDepartment);
                }
                else
                {
                    employeeDepartment = (from e in context.Employees.Where(e => e.EmployeeId == id)
                                          join d in context.Departments
                                          on e.DepartmentId equals d.DepartmentId
                                          select new EmployeeDepartmentSummaryViewModel
                                          {
                                              EmployeeId = e.EmployeeId,
                                              //FirstName = e.FirstName,
                                              //MiddleName = e.MiddleName,
                                              //LastName = e.LastName,
                                              //Gender = e.Gender,
                                              //DepartmentName = d.DepartmentName,
                                              //DepartmentCode = d.DepartmentCode,

                                              // Replace that with EveryFirstCharacterCapital Method
                                              FirstName = EveryFirstCharacterCapital(e.FirstName),
                                              MiddleName = EveryFirstCharacterCapital(e.MiddleName),
                                              LastName = EveryFirstCharacterCapital(e.LastName),
                                              Gender = EveryFirstCharacterCapital(e.Gender),
                                              DepartmentId = d.DepartmentId,
                                              DepartmentCode = d.DepartmentCode.ToUpper(),
                                              DepartmentName = EveryFirstCharacterCapital(d.DepartmentName)

                                          }).First(); // first() for single Record
                    if (employeeDepartment == null)
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(employeeDepartment);
        }


        //Add Post
        [HttpPost]
        public IActionResult AddEmployee(EmployeeDepartmentSummaryViewModel empDep)
        {
            ViewBag.department = context.Departments.ToList();
            try
            {
                ModelState.Remove("DepartmentName");
                ModelState.Remove("DepartmentCode");
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Please enter Valid Data!");
                    return View(empDep);
                }
                else
                {
                    if(empDep.EmployeeId == 0)
                    {
                        var data = new Employee()
                        {
                            FirstName = empDep.FirstName,
                            MiddleName = empDep.MiddleName,
                            LastName = empDep.LastName,
                            Gender = empDep.Gender,
                            DepartmentId = empDep.DepartmentId
                        };
                        context.Employees.Add(data);
                        context.SaveChanges();
                        TempData["success"] = "Record has been Inserted!";
                    }
                    else
                    {
                        var data = new Employee()
                        {
                            EmployeeId = empDep.EmployeeId,
                            FirstName = empDep.FirstName,
                            MiddleName = empDep.MiddleName,
                            LastName = empDep.LastName,
                            Gender = empDep.Gender,
                            DepartmentId = empDep.DepartmentId
                        };
                        context.Employees.Update(data);
                        context.SaveChanges();
                        TempData["success"] = "Record has been Updated!";
                    }
                }
            }
            catch (Exception) 
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        //Delete
        public IActionResult DeleteEmployee(int id) 
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var data = context.Employees.Find(id);
                    if(data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        context.Employees.Remove(data);
                        context.SaveChanges();
                        TempData["success"] = "Record has been Successfully Deleted!";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        public IActionResult DetailsEmployee(int id)
        {
            EmployeeDepartmentSummaryViewModel employeeDepartment = new EmployeeDepartmentSummaryViewModel();
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    employeeDepartment = (from e in context.Employees.Where(e=>e.EmployeeId == id)
                                        join d in context.Departments
                                        on e.DepartmentId equals d.DepartmentId
                                        select new EmployeeDepartmentSummaryViewModel
                                        {
                                            EmployeeId = e.EmployeeId,
                                            //FirstName = e.FirstName,
                                            //MiddleName = e.MiddleName,
                                            //LastName = e.LastName,
                                            //Gender = e.Gender,
                                            //DepartmentName = d.DepartmentName,
                                            //DepartmentCode = d.DepartmentCode,

                                            // Replace that with EveryFirstCharacterCapital Method
                                            FirstName = EveryFirstCharacterCapital(e.FirstName),
                                            MiddleName = EveryFirstCharacterCapital(e.MiddleName),
                                            LastName = EveryFirstCharacterCapital(e.LastName),
                                            Gender = EveryFirstCharacterCapital(e.Gender),
                                            DepartmentId = d.DepartmentId,
                                            DepartmentCode = d.DepartmentCode.ToUpper(),
                                            DepartmentName = EveryFirstCharacterCapital(d.DepartmentName)

                                        }).First(); // first() for single Record
                    if(employeeDepartment == null)
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(employeeDepartment);
        }


        // Delete Single Or Multiple record
        public IActionResult DeleteSingleOrMultiple(int[] ids)
        {
            //take a string variable for reload page in Js file
            string result = string.Empty;
            try
            {
                if(ids.Count() > 0)
                {
                    foreach(int id in ids)
                    {
                        var data = context.Employees.Where(e=>e.EmployeeId == id).FirstOrDefault();
                        if(data != null)
                        {
                            context.Employees.Remove(data);
                        }
                    }
                    context.SaveChanges();
                    TempData["success"] = "Record has been Successfully Deleted!";
                    result = "success";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new JsonResult(result);
        }
	
     }
}
