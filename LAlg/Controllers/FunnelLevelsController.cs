using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Authorize]
    public class FunnelLevelsController : Controller
    {
        private readonly BotAppContext _context;

        public FunnelLevelsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: FunnelLevels
        public async Task<IActionResult> Index()//Guid? id
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //ViewData["FunnelId"] = id;
            var botAppContext = _context.FunnelLevels.Include(f => f.Funnel).Include(f => f.Group).Include(f => f.Product);//.Where(f => f.FunnelId == id)
            return View(await botAppContext.ToListAsync());
        }

        // GET: FunnelLevels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funnelLevel = await _context.FunnelLevels
                .Include(f => f.Funnel)
                .Include(f => f.Group)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.FunnelLevelId == id);
            if (funnelLevel == null)
            {
                return NotFound();
            }
            ViewData["FunnelId"] = funnelLevel.FunnelId;
            return View(funnelLevel);
        }

        // GET: FunnelLevels/Create
        public IActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["FunnelId"] = id;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: FunnelLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FunnelLevelId,FunnelId,GroupId,ProductId,FunnelId")] FunnelLevel funnelLevel)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var product = await _context.Products.FindAsync(funnelLevel.ProductId);
                funnelLevel.Group = new Group { Name = $"Группа {product.Name}", IsCommon = true, Creator = userId, ProductId = funnelLevel.ProductId };
                funnelLevel.FunnelLevelId = Guid.NewGuid();
                _context.Add(funnelLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = funnelLevel.FunnelId });
            }
            ViewData["FunnelId"] = funnelLevel.FunnelId;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", funnelLevel.ProductId);
            return View(funnelLevel);
        }

        // GET: FunnelLevels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funnelLevel = await _context.FunnelLevels.FindAsync(id);
            if (funnelLevel == null)
            {
                return NotFound();
            }
            ViewData["FunnelId"] = funnelLevel.FunnelId;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", funnelLevel.ProductId);
            return View(funnelLevel);
        }

        // POST: FunnelLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FunnelLevelId,FunnelId,GroupId,ProductId")] FunnelLevel funnelLevel)
        {
            if (id != funnelLevel.FunnelLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funnelLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunnelLevelExists(funnelLevel.FunnelLevelId))
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
            ViewData["FunnelId"] = funnelLevel.FunnelId;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", funnelLevel.ProductId);
            return View(funnelLevel);
        }

        // GET: FunnelLevels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funnelLevel = await _context.FunnelLevels
                .Include(f => f.Funnel)
                .Include(f => f.Group)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.FunnelLevelId == id);
            if (funnelLevel == null)
            {
                return NotFound();
            }
            ViewData["FunnelId"] = funnelLevel.FunnelId;
            return View(funnelLevel);
        }

        // POST: FunnelLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var funnelLevel = await _context.FunnelLevels.FindAsync(id);
            _context.FunnelLevels.Remove(funnelLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = funnelLevel.FunnelId });
        }

        private bool FunnelLevelExists(Guid id)
        {
            return _context.FunnelLevels.Any(e => e.FunnelLevelId == id);
        }
    }
}
