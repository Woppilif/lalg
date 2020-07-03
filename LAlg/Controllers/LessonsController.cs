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
    public class LessonsController : Controller
    {
        private readonly BotAppContext _context;

        public LessonsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index(string sortOrder)
        {
            var botAppContext = _context.Lessons.Include(l => l.Group).Include(l => l.Pattern);

            ViewBag.GroupSortParm = String.IsNullOrEmpty(sortOrder) ? "Group" : "";
            ViewBag.LessonSortParm = sortOrder == "Lesson" ? "" : "Lesson";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "" : "Status";
            ViewBag.UrlSortParm = sortOrder == "Url" ? "" : "Url";
            ViewBag.PatternSortParm = sortOrder == "Pattern" ? "" : "Pattern";
            ViewBag.IsRepeatsSortParm = sortOrder == "IsRepeats" ? "" : "IsRepeats";

            switch (sortOrder)
            {
                case "Group":
                    var user = botAppContext.ToList().OrderBy(s => s.Group.Name);
                    return View(user.ToList());

                case "Lesson":
                    user = botAppContext.ToList().OrderBy(s => s.LessonAt);
                    return View(user.ToList());
                case "Status":
                    user = botAppContext.ToList().OrderBy(s => s.Status);
                    return View(user.ToList());
                case "Url":
                    user = botAppContext.ToList().OrderBy(s => s.Url);
                    return View(user.ToList());
                case "Pattern":
                    user = botAppContext.ToList().OrderBy(s => s.Pattern.Name);
                    return View(user.ToList());
                case "IsRepeats":
                    user = botAppContext.ToList().OrderBy(s => s.IsRepeats);
                    return View(user.ToList());
            }

                    return View(await botAppContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Group)
                .Include(l => l.Pattern)
                .FirstOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name");
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonId,GroupId,LessonAt,Status,Url,PatternId,IsRepeats")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                lesson.LessonId = Guid.NewGuid();
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", lesson.GroupId);
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name", lesson.PatternId);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", lesson.GroupId);
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name", lesson.PatternId);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LessonId,GroupId,LessonAt,Status,Url,PatternId,IsRepeats")] Lesson lesson)
        {
            if (id != lesson.LessonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.LessonId))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", lesson.GroupId);
            ViewData["PatternId"] = new SelectList(_context.Patterns, "PatternId", "Name", lesson.PatternId);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Group)
                .Include(l => l.Pattern)
                .FirstOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(Guid id)
        {
            return _context.Lessons.Any(e => e.LessonId == id);
        }
    }
}
