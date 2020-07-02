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
    public class ReferalsController : Controller
    {
        private readonly BotAppContext _context;

        public ReferalsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Referals
        public async Task<IActionResult> Index()
        {
            var botAppContext = _context.Referals.Include(r => r.Users);
            return View(await botAppContext.ToListAsync());
        }

        // GET: Referals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referal = await _context.Referals
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.ReferalId == id);
            if (referal == null)
            {
                return NotFound();
            }

            return View(referal);
        }

        // GET: Referals/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Referals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferalId,UserId,Code,IsActive,IsCommon,Amount")] Referal referal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", referal.UserId);
            return View(referal);
        }

        // GET: Referals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referal = await _context.Referals.FindAsync(id);
            if (referal == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", referal.UserId);
            return View(referal);
        }

        // POST: Referals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReferalId,UserId,Code,IsActive,IsCommon,Amount")] Referal referal)
        {
            if (id != referal.ReferalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferalExists(referal.ReferalId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", referal.UserId);
            return View(referal);
        }

        // GET: Referals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referal = await _context.Referals
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.ReferalId == id);
            if (referal == null)
            {
                return NotFound();
            }

            return View(referal);
        }

        // POST: Referals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var referal = await _context.Referals.FindAsync(id);
            _context.Referals.Remove(referal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferalExists(int id)
        {
            return _context.Referals.Any(e => e.ReferalId == id);
        }
    }
}
