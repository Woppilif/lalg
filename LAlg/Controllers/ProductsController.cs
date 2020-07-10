using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly BotAppContext _context;

        public ProductsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            var botAppContext = _context.Products.Include(p => p.ProductType);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "" : "Price";
            //ViewBag.AgeIdSortParm = sortOrder == "AgeId" ? "" : "AgeId";
            ViewBag.ProductTypeSortParm = sortOrder == "ProductType" ? "" : "ProductType";
            ViewBag.FreeTimesSortParm = sortOrder == "FreeTimes" ? "" : "FreeTimes";

            var user = from s in botAppContext
                       select s;

            if (!String.IsNullOrEmpty(search))
            {
                user = user.Where(s => s.Name.ToUpper().Contains(search.ToUpper())
                                 || s.Price.ToString().Contains(search)
                                 || s.ProductType.Name.ToUpper().Contains(search.ToUpper())
                                 //|| s.AgeId.ToString().ToUpper().Contains(search.ToUpper())
                                 || s.FreeTimes.ToString().Contains(search));

                return View(user.ToList());
            }
            //System.Reflection.MemberInfo property = typeof(BotAppData.Models.Age).GetProperty("Order");
            //var dd = property.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute)) as DisplayAttribute;

            switch (sortOrder)
            {
                case "Name":
                    //var user = botAppContext.ToList().OrderBy(s => s.Group.Name);
                    return View(user.ToList().OrderBy(s => s.Name));
                case "Price":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.Price));
                /*case "AgeId":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.AgeId.));*/
                case "ProductType":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.ProductType.Name));
                case "FreeTimes":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.FreeTimes));
            }

            return View(await botAppContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Price,AgeId,ProductTypeId,FreeTimes")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = Guid.NewGuid();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "Name", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "Name", product.ProductTypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,Name,Price,AgeId,ProductTypeId,FreeTimes")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "Name", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
