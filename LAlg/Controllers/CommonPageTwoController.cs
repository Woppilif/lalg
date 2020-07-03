using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAlg.Controllers
{
    public class CommonPageTwoController : Controller
    {
        // GET: CommonPageTwoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommonPageTwoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommonPageTwoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommonPageTwoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommonPageTwoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommonPageTwoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommonPageTwoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommonPageTwoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
