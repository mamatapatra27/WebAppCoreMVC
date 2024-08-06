using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAppCoreMVC.Data;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext context;

        public DepartmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await context.Departments.ToListAsync();
            return View(data);
        }


        // for edit add parameter
        public IActionResult AddDepartment(int?  id)
        {
            // for Edit
            Department department = new Department();
            if(id != null && id != 0)
            {
                department = context.Departments.Find(id);
            }


            return View(department);
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            else
            {
                if(department.DepartmentId == 0)
                {
                    // add
                    context.Departments.Add(department);
                    context.SaveChanges();
                    TempData["success"] = "Department has been created!";
                }
                else
                {
                    // edit
                    context.Departments.Update(department);
                    context.SaveChanges();
                    TempData["success"] = "Department has been Updated!";
                }
            }
            return RedirectToAction("Index");
        }

        // Delete

        public IActionResult Delete(int id)
        {
            if(id != 0)
            {
                bool status = context.Employees.Any(x=>x.DepartmentId == id);
                if (status)
                {
                    TempData["warning"] = "Department is taken by another Employee, so can't delete this!";
                }
                else
                {
                    var department = context.Departments.Find(id);
                    if(department == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        context.Departments.Remove(department);
                        context.SaveChanges();
                        TempData["success"] = "Department has been Deleted Successfully!";
                    }
                }
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
    }
}
