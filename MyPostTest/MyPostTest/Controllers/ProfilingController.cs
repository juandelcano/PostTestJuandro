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
    public class ProfilingController : Controller
    {
        private readonly MyContext myContext;
        public ProfilingController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var university = myContext.Profilings.Include(e => e.Education);
            return View(university.ToList());
        }

        public ActionResult Create(Profiling profiling)
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            return View();
        }

        public ActionResult Delete(int? id)
        {
            return View();
        }
    }
}
