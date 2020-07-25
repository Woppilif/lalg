using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Authorize]
    public class PatternsController : Controller
    {
        private readonly BotAppContext _context;

        public PatternsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Patterns
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            var botAppContext = _context.Patterns;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";

            var user = from s in botAppContext
                       select s;

            if (!String.IsNullOrEmpty(search))
            {
                user = user.Where(s => s.Name.ToUpper().Contains(search.ToUpper()));
                return View(user.ToList());
            }

            switch (sortOrder)
            {
                case "Name":
                    return View(user.ToList().OrderBy(s => s.Name));
            }

            return View(await _context.Patterns.ToListAsync());
        }

        // GET: Patterns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pattern = await _context.Patterns
                .FirstOrDefaultAsync(m => m.PatternId == id);
            if (pattern == null)
            {
                return NotFound();
            }

            return View(pattern);
        }

        // GET: Patterns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patterns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatternId,Name")] Pattern pattern)
        {
            if (ModelState.IsValid)
            {
                pattern.PatternId = Guid.NewGuid();
                _context.Add(pattern);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pattern);
        }

        // GET: Patterns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pattern = await _context.Patterns.FindAsync(id);
            if (pattern == null)
            {
                return NotFound();
            }
            return View(pattern);
        }

        // POST: Patterns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PatternId,Name")] Pattern pattern)
        {
            if (id != pattern.PatternId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pattern);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatternExists(pattern.PatternId))
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
            return View(pattern);
        }

        // GET: Patterns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pattern = await _context.Patterns
                .FirstOrDefaultAsync(m => m.PatternId == id);
            if (pattern == null)
            {
                return NotFound();
            }

            return View(pattern);
        }

        // POST: Patterns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pattern = await _context.Patterns.FindAsync(id);
            _context.Patterns.Remove(pattern);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatternExists(Guid id)
        {
            return _context.Patterns.Any(e => e.PatternId == id);
        }
    }
}
