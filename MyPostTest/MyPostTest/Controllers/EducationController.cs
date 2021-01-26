using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPostTest.Context;
using MyPostTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPostTest.Controllers
{
    public class EducationController : Controller
    {
        private readonly MyContext myContext;
        public EducationController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var university = myContext.Educations.Include(u => u.University);
            return View(university.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.UniversityId = new SelectList(myContext.Universities, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Education education)
        {
            if(ModelState.IsValid)
            {
                myContext.Educations.Add(education);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniversityId = new SelectList(myContext.Universities, "Id", "Name", education.UniversityID);
            return View(education);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            Education education = myContext.Educations.Find(Id);
            if (education == null)
            {
                return NotFound();
            }
            ViewBag.UniversityId = new SelectList(myContext.Universities, "Id", "Name", education.UniversityID);
            return View(education);

        }

        [HttpPost]
        public ActionResult Edit(Education education)
        {
            if (ModelState.IsValid)
            {
                myContext.Entry(education).State = EntityState.Modified;
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniversityId = new SelectList(myContext.Universities, "Id", "Name", education.UniversityID);
            return View(education);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var education = myContext.Educations.Include(e => e.University).FirstOrDefault(u => u.Id == id);
            if (education == null)
            {
                return NotFound();
            }
            return View(education);
        }

        [HttpPost, ActionName("Delete")][ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Education education = myContext.Educations.Find(id);
            myContext.Educations.Remove(education);
            myContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
