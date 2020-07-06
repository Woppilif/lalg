using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Authorize]
    public class PatternMessagesController : Controller
    {
        private readonly BotAppContext _context;

        public PatternMessagesController(BotAppContext context)
        {
            _context = context;
        }

        // GET: PatternMessages
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            var botAppContext = _context.PatternMessages.Include(p => p.Pattern);

            ViewBag.PatternSortParm = String.IsNullOrEmpty(sortOrder) ? "Pattern" : "";
            ViewBag.IsGreetingSortParm = sortOrder == "IsGreeting" ? "" : "IsGreeting";
            ViewBag.MessageSortParm = sortOrder == "Message" ? "" : "Message";
            ViewBag.AtTimeSortParm = sortOrder == "AtTime" ? "" : "AtTime";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "" : "Status";

            var user = from s in botAppContext
                       select s;

            if (!String.IsNullOrEmpty(search))
            {
                user = botAppContext.Where(s => s.Pattern.Name.ToUpper().Contains(search.ToUpper())
                                 || s.Message.ToUpper().Contains(search.ToUpper())
                                 || s.AtTime.ToString().Contains(search));

                return View(user.ToList());
            }

            switch (sortOrder)
            {
                case "Pattern":
                    //var user = botAppContext.ToList().OrderBy(s => s.Group.Name);
                    return View(user.ToList().OrderBy(s => s.Pattern.Name));
                case "IsGreeting":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.IsGreeting));
                case "Message":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.Message));
                case "AtTime":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.AtTime));
                case "Status":
                    //user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList().OrderBy(s => s.Status));
            }
                    return View(await botAppContext.ToListAsync());
        }

        // GET: PatternMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternMessage = await _context.PatternMessages
                .Include(p => p.Pattern)
                .FirstOrDefaultAsync(m => m.PatternMessageId == id);
            if (patternMessage == null)
            {
                return NotFound();
            }

            return View(patternMessage);
        }

        // GET: PatternMessages/Create
        public IActionResult Create()
        {
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name");
            return View();
        }

        // POST: PatternMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatternMessageId,PatternId,IsGreeting,Message,AtTime,Status")] PatternMessage patternMessage)
        {
            if (ModelState.IsValid)
            {
                patternMessage.PatternMessageId = Guid.NewGuid();
                _context.Add(patternMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name", patternMessage.PatternId);
            return View(patternMessage);
        }

        // GET: PatternMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternMessage = await _context.PatternMessages.FindAsync(id);
            if (patternMessage == null)
            {
                return NotFound();
            }
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name", patternMessage.PatternId);
            return View(patternMessage);
        }

        // POST: PatternMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PatternMessageId,PatternId,IsGreeting,Message,AtTime,Status")] PatternMessage patternMessage)
        {
            if (id != patternMessage.PatternMessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patternMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatternMessageExists(patternMessage.PatternMessageId))
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
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name", patternMessage.PatternId);
            return View(patternMessage);
        }

        // GET: PatternMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patternMessage = await _context.PatternMessages
                .Include(p => p.Pattern)
                .FirstOrDefaultAsync(m => m.PatternMessageId == id);
            if (patternMessage == null)
            {
                return NotFound();
            }

            return View(patternMessage);
        }

        // POST: PatternMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var patternMessage = await _context.PatternMessages.FindAsync(id);
            _context.PatternMessages.Remove(patternMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatternMessageExists(Guid id)
        {
            return _context.PatternMessages.Any(e => e.PatternMessageId == id);
        }
    }
}
