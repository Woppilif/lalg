using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Authorize]
    public class AgesController : Controller
    {
        private readonly BotAppContext _context;

        public AgesController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Ages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ages.ToListAsync());
        }

        // GET: Ages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var age = await _context.Ages
                .FirstOrDefaultAsync(m => m.AgeId == id);
            if (age == null)
            {
                return NotFound();
            }

            return View(age);
        }

        // GET: Ages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgeId,Name,Order,IsShows")] Age age)
        {
            if (ModelState.IsValid)
            {
                _context.Add(age);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(age);
        }

        // GET: Ages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var age = await _context.Ages.FindAsync(id);
            if (age == null)
            {
                return NotFound();
            }
            return View(age);
        }

        // POST: Ages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgeId,Name,Order,IsShows")] Age age)
        {
            if (id != age.AgeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(age);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgeExists(age.AgeId))
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
            return View(age);
        }

        // GET: Ages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var age = await _context.Ages
                .FirstOrDefaultAsync(m => m.AgeId == id);
            if (age == null)
            {
                return NotFound();
            }

            return View(age);
        }

        // POST: Ages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var age = await _context.Ages.FindAsync(id);
            _context.Ages.Remove(age);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgeExists(int id)
        {
            return _context.Ages.Any(e => e.AgeId == id);
        }
    }
}
