using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPostTest.Context;
using MyPostTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPostTest.Controllers
{
    public class UniversityController : Controller
    {
        private readonly MyContext myContext;

        public UniversityController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public ActionResult Index()
        {
            return View(myContext.Universities.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(University university)
        {
            myContext.Universities.Add(university);
            myContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(myContext.Universities.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(University university, int id)
        {
            try
            {
                myContext.Entry(university).State = EntityState.Modified;
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(myContext.Universities.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete (int id, University university)
        {
            try
            {
                University universityDelete = myContext.Universities.Find(id);
                myContext.Universities.Remove(universityDelete);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
