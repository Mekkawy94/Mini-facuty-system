using Faculity_System.Data;
using Faculity_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Faculity_System.Controllers
{
    public class StudentsController : Controller
    {
        ApplicationDbContext context;
        IWebHostEnvironment webHostEnvironment;

        public StudentsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetIndexView()
        {
            return View("Index" , context.Students.ToList());
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.DeptSelectItems = new SelectList(context.Departments.ToList(), "Id", "Name");
            return View("Create");
        }
        [HttpPost]
        public IActionResult AddNewStudent(Students student , IFormFile? ImageFormFile)
        {

            if (ImageFormFile != null)
            {
                string imgExtension = Path.GetExtension(ImageFormFile.FileName);

                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                student.ImageUrl = imgUrl;

                string imgPath = webHostEnvironment.WebRootPath + imgUrl;

                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                ImageFormFile.CopyTo(imgStream);
                imgStream.Dispose();

            }
            else
            {
                student.ImageUrl = "\\images\\No_Image.png";
            }


            if (((student.JoinDate - student.BirthDate).Days / 365) < 16)
            {
                ModelState.AddModelError(string.Empty, "Illegal Age (Less than 16 years old).");
            }

            if (((student.JoinDate - student.BirthDate).Days / 365) > 30)
            {
                ModelState.AddModelError(string.Empty, "Illegal Age (More than 30 years old).");
            }

            if (student.GPA < 0 || student.GPA > 4)
            {
                ModelState.AddModelError(string.Empty, "GPA Must Be Between 0 and 4");
            }

            if (student.Level < 1 || student.Level > 4)
            {
                ModelState.AddModelError(string.Empty, "Level Must Be Between 1 and 4");
            }


            if (ModelState.IsValid == true)
            {
                context.Students.Add(student);
                context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }


            else
            {
                ViewBag.DeptSelectItems = new SelectList(context.Departments.ToList(), "Id", "Name");
                return View("Create");
            }
        }


        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Students student = context.Students.FirstOrDefault(e => e.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.DeptSelectItems = new SelectList(context.Departments.ToList(), "Id", "Name");
                return View("Edit", student);
            }
        }
        [HttpPost]
        public IActionResult EditCurrentStudent(Students student, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                if (student.ImageUrl != "\\images\\No_Image.png")
                {
                    string oldImgPath = webHostEnvironment.WebRootPath + student.ImageUrl;

                    if (System.IO.File.Exists(oldImgPath) == true)
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }


                string imgExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                student.ImageUrl = imgUrl;

                string imgPath = webHostEnvironment.WebRootPath + imgUrl;

                // FileStream 
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                imageFormFile.CopyTo(imgStream);
                imgStream.Dispose();
            }




            if (((student.JoinDate - student.BirthDate).Days / 365) < 16)
            {
                ModelState.AddModelError(string.Empty, "Illegal Age (Less than 16 years old).");
            }

            if (((student.JoinDate - student.BirthDate).Days / 365) > 30)
            {
                ModelState.AddModelError(string.Empty, "Illegal Age (More than 30 years old).");
            }

            if (student.GPA < 0 || student.GPA > 4)
            {
                ModelState.AddModelError(string.Empty, "GPA Must Be Between 0 and 4");
            }

            if (student.Level < 1 || student.Level > 4)
            {
                ModelState.AddModelError(string.Empty, "Level Must Be Between 1 and 4");
            }

            if (ModelState.IsValid == true)
            {
                context.Students.Update(student);
                context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.DeptSelectItems = new SelectList(context.Departments.ToList(), "Id", "Name");
                return View("Edit");
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Students student = context.Students.Include(e => e.Departments).FirstOrDefault(e => e.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", student);
            }
        }


        [HttpPost]
        public IActionResult DeleteCurrentStudent(int id)
        {
            Students student = context.Students.FirstOrDefault(e => e.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                if (student.ImageUrl != "\\images\\No_Image.png")
                {
                    string imgPath = webHostEnvironment.WebRootPath + student.ImageUrl;

                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }


                context.Students.Remove(student);
                context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Students student = context.Students.Include(e => e.Departments).FirstOrDefault(e => e.Id == id);

            return View("Details", student);
        }
    }
}
