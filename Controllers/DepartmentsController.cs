using Faculity_System.Data;
using Faculity_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Faculity_System.Controllers
{
    public class DepartmentsController : Controller
    {
        ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Departments department = context.Departments.FirstOrDefault(e => e.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", department);
            }
        }

        [HttpGet]
        public IActionResult GetIndexView()
        {
            return View("Index", context.Departments.ToList());
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNewDepartment(Departments dep, IFormFile? imageFormFile)
        {


            if (ModelState.IsValid)
            {
                context.Departments.Add(dep);
                context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }


            else
            {
                return View("Create");
            }
        }
        [HttpPost]
        public IActionResult EditCurrentDepartment(Departments dep)
        {
            if (ModelState.IsValid == true)
            {
                context.Departments.Update(dep);
                context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit");
            }
        }
        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Departments department = context.Departments.Include(d => d.Student).FirstOrDefault(e => e.Id == id);

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", department);
            }
        }


        [HttpPost]
        public IActionResult DeleteCurrentDepartment(int id)
        {
            Departments department = context.Departments.FirstOrDefault(e => e.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                context.Departments.Remove(department);
                context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Departments department = context.Departments.Include(d => d.Student).FirstOrDefault(e => e.Id == id);

            return View("Details", department);
        }
    }
}
