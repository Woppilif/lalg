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
    public class PageTemplatesController : Controller
    {
        private readonly BotAppContext _context;

        public PageTemplatesController(BotAppContext context)
        {
            _context = context;
        }

        // GET: PageTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.PageTemplates.ToListAsync());
        }

        // GET: PageTemplates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageTemplate = await _context.PageTemplates
                .FirstOrDefaultAsync(m => m.PageTemplateId == id);
            if (pageTemplate == null)
            {
                return NotFound();
            }

            return View(pageTemplate);
        }

        // GET: PageTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageTemplateId,Name,Url")] PageTemplate pageTemplate)
        {
            if (ModelState.IsValid)
            {
                pageTemplate.PageTemplateId = Guid.NewGuid();
                _context.Add(pageTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageTemplate);
        }

        // GET: PageTemplates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageTemplate = await _context.PageTemplates.FindAsync(id);
            if (pageTemplate == null)
            {
                return NotFound();
            }
            return View(pageTemplate);
        }

        // POST: PageTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PageTemplateId,Name,Url")] PageTemplate pageTemplate)
        {
            if (id != pageTemplate.PageTemplateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageTemplateExists(pageTemplate.PageTemplateId))
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
            return View(pageTemplate);
        }

        // GET: PageTemplates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageTemplate = await _context.PageTemplates
                .FirstOrDefaultAsync(m => m.PageTemplateId == id);
            if (pageTemplate == null)
            {
                return NotFound();
            }

            return View(pageTemplate);
        }

        // POST: PageTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pageTemplate = await _context.PageTemplates.FindAsync(id);
            _context.PageTemplates.Remove(pageTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageTemplateExists(Guid id)
        {
            return _context.PageTemplates.Any(e => e.PageTemplateId == id);
        }
    }
}
