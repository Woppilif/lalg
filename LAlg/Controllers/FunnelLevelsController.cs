using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BotAppData;
using BotAppData.Models;
using System.Security.Claims;

namespace LAlg.Controllers
{
    public class FunnelLevelsController : Controller
    {
        private readonly BotAppContext _context;

        public FunnelLevelsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: FunnelLevels
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var botAppContext = _context.FunnelLevels.Where(f => f.FunnelId == id).Include(f => f.Funnel).Include(f => f.Group).Include(f => f.Product);
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

            return View(funnelLevel);
        }

        // GET: FunnelLevels/Create
        public IActionResult Create()
        {
            ViewData["FunnelId"] = new SelectList(_context.Funnels, "FunnelId", "FunnelId");
            //ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: FunnelLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FunnelLevelId,FunnelId,GroupId,ProductId")] FunnelLevel funnelLevel)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                funnelLevel.Group = new Group { Name = $"Группа от {DateTime.Now.ToString("dd-MM")}", IsCommon = true, Creator = userId };
                funnelLevel.FunnelLevelId = Guid.NewGuid();
                _context.Add(funnelLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FunnelId"] = new SelectList(_context.Funnels, "FunnelId", "FunnelId", funnelLevel.FunnelId);
            //ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", funnelLevel.GroupId);
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
            ViewData["FunnelId"] = new SelectList(_context.Funnels, "FunnelId", "FunnelId", funnelLevel.FunnelId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", funnelLevel.GroupId);
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
            ViewData["FunnelId"] = new SelectList(_context.Funnels, "FunnelId", "FunnelId", funnelLevel.FunnelId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", funnelLevel.GroupId);
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
            return RedirectToAction(nameof(Index));
        }

        private bool FunnelLevelExists(Guid id)
        {
            return _context.FunnelLevels.Any(e => e.FunnelLevelId == id);
        }
    }
}
