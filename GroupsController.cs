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
    public class GroupsController : Controller
    {
        private readonly BotAppContext _context;

        public GroupsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index(string sortOrder)
        {
            var botAppContext = _context.Groups.Include(g => g.Age).Include(g => g.GroupType).Include(g => g.Product);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "" : "Age";
            ViewBag.GroupTypeSortParm = sortOrder == "GroupType" ? "" : "GroupType";
            ViewBag.CreatorSortParm = sortOrder == "Creator" ? "" : "Creator";
            ViewBag.ProductSortParm = sortOrder == "Product" ? "" : "Product";
            ViewBag.IsClosedSortParm = sortOrder == "IsClosed" ? "" : "IsClosed";
            ViewBag.IsCommonSortParm = sortOrder == "IsCommon" ? "" : "IsCommon";

            switch (sortOrder)
            {
                case "Name":
                    var user = botAppContext.ToList().OrderBy(s => s.Name);
                    return View(user.ToList());

                case "Age":
                    user = botAppContext.ToList().OrderBy(s => s.AgeId);
                    return View(user.ToList());
                case "GroupType":
                    user = botAppContext.ToList().OrderBy(s => s.GroupType.Name);
                    return View(user.ToList());
                case "Creator":
                    user = botAppContext.ToList().OrderBy(s => s.Creator);
                    return View(user.ToList());
                case "Product":
                    user = botAppContext.ToList().OrderBy(s => s.Product.Name);
                    return View(user.ToList());
                case "IsClosed":
                    user = botAppContext.ToList().OrderBy(s => s.IsClosed);
                    return View(user.ToList());
                case "IsCommon":
                    user = botAppContext.ToList().OrderBy(s => s.IsCommon);
                    return View(user.ToList());
            }

                    return View(await botAppContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .Include(g => g.Age)
                .Include(g => g.GroupType)
                .Include(g => g.Product)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "Name");
            ViewData["GroupTypeId"] = new SelectList(_context.GroupTypes, "GroupTypeId", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,Name,AgeId,GroupTypeId,Creator,ProductId,IsClosed,IsCommon")] Group @group)
        {
            if (ModelState.IsValid)
            {
                @group.GroupId = Guid.NewGuid();
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "Name", @group.AgeId);
            ViewData["GroupTypeId"] = new SelectList(_context.GroupTypes, "GroupTypeId", "Name", @group.GroupTypeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", @group.ProductId);
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "Name", @group.AgeId);
            ViewData["GroupTypeId"] = new SelectList(_context.GroupTypes, "GroupTypeId", "Name", @group.GroupTypeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", @group.ProductId);
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GroupId,Name,AgeId,GroupTypeId,Creator,ProductId,IsClosed,IsCommon")] Group @group)
        {
            if (id != @group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.GroupId))
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
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "Name", @group.AgeId);
            ViewData["GroupTypeId"] = new SelectList(_context.GroupTypes, "GroupTypeId", "Name", @group.GroupTypeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", @group.ProductId);
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .Include(g => g.Age)
                .Include(g => g.GroupType)
                .Include(g => g.Product)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(Guid id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }
    }
}
