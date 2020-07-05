using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BotAppData;
using BotAppData.Models;

namespace LAlg.Controllers
{
    public class FunnelsController : Controller
    {
        private readonly BotAppContext _context;

        public FunnelsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Funnels
        public async Task<IActionResult> Index()
        {
            var botAppContext = _context.Funnels.Include(f => f.PageTemplate);
            return View(await botAppContext.ToListAsync());
        }

        // GET: Funnels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funnel = await _context.Funnels
                .Include(f => f.PageTemplate)
                .FirstOrDefaultAsync(m => m.FunnelId == id);
            if (funnel == null)
            {
                return NotFound();
            }

            return View(funnel);
        }

        // GET: Funnels/Create
        public IActionResult Create()
        {
            ViewData["PageTemplateId"] = new SelectList(_context.PageTemplates, "PageTemplateId", "PageTemplateId");
            return View();
        }

        // POST: Funnels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FunnelId,Name,PageTemplateId,BonusAmount,IsActive,CreatorId")] Funnel funnel)
        {
            if (ModelState.IsValid)
            {
                funnel.FunnelId = Guid.NewGuid();
                _context.Add(funnel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PageTemplateId"] = new SelectList(_context.PageTemplates, "PageTemplateId", "PageTemplateId", funnel.PageTemplateId);
            return View(funnel);
        }

        // GET: Funnels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funnel = await _context.Funnels.FindAsync(id);
            if (funnel == null)
            {
                return NotFound();
            }
            ViewData["PageTemplateId"] = new SelectList(_context.PageTemplates, "PageTemplateId", "PageTemplateId", funnel.PageTemplateId);
            return View(funnel);
        }

        // POST: Funnels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FunnelId,Name,PageTemplateId,BonusAmount,IsActive,CreatorId")] Funnel funnel)
        {
            if (id != funnel.FunnelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funnel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunnelExists(funnel.FunnelId))
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
            ViewData["PageTemplateId"] = new SelectList(_context.PageTemplates, "PageTemplateId", "PageTemplateId", funnel.PageTemplateId);
            return View(funnel);
        }

        // GET: Funnels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funnel = await _context.Funnels
                .Include(f => f.PageTemplate)
                .FirstOrDefaultAsync(m => m.FunnelId == id);
            if (funnel == null)
            {
                return NotFound();
            }

            return View(funnel);
        }

        // POST: Funnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var funnel = await _context.Funnels.FindAsync(id);
            _context.Funnels.Remove(funnel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunnelExists(Guid id)
        {
            return _context.Funnels.Any(e => e.FunnelId == id);
        }
    }
}
