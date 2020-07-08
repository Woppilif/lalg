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
    public class UsersController : Controller
    {
        private readonly BotAppContext _context;

        public UsersController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string sortOrder)
        {
            //ViewBag.
            var botAppContext = _context.Users.Include(u => u.Age).Include(u => u.Group);

            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "UserName" : "";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "" : "Phone";
            ViewBag.CreateSortParm = sortOrder == "CreatedAt" ? "" : "CreatedAt";
            //ViewBag.GroupSortParm = sortOrder == "Group" ? "" : "Group";//почему то null выдает
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "" : "LastName";
            ViewBag.RegSortParm = sortOrder == "Reg" ? "" : "Reg";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "" : "Age";
            ViewBag.IsAdminSortParm = sortOrder == "IsAdmin" ? "" : "IsAdmin";
            ViewBag.IsTeacherSortParm = sortOrder == "IsTeacher" ? "" : "IsTeacher";
            ViewBag.BalanceSortParm = sortOrder == "Balance" ? "" : "Balance";

            /*if (String.IsNullOrEmpty(sortOrder))
            {
                ViewBag.NameSortParm = "UserName";
            }
            else if (sortOrder == "Phone")
            {
                ViewBag.PhoneSortParm = sortOrder;
            }else if(sortOrder == "CreatedAt")
            {
                ViewBag.CreateSortParm = sortOrder;
            }
            else
            {
                sortOrder = "!!!";
            }*/


            //ViewBag.PhoneSortParm = sortOrder == "Phone" ? "" : "Phone"; 
            //ViewBag.CreateSortParm = sortOrder == "CreatedAt" ? "" : "CreatedAt"; 

            //ViewBag.PhoneSortParm = sortOrder == "UserName" ? "" : "UserName"; 
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            /*botAppContext.Where(s => s.Firstname.Contains(searchString)
                               || s.FirstMidName.Contains(searchString));*/

            switch (sortOrder)
            {
                case "UserName":
                    var user = botAppContext.ToList().OrderBy(s => s.Firstname);//.OrderBy(s => s.Firstname);
                    return View(user.ToList());

                case "Phone":
                    user = botAppContext.ToList().OrderBy(s => s.Phone);
                     return View(user.ToList());

                case "CreatedAt":
                    user = botAppContext.ToList().OrderBy(s => s.CreatedAt);
                    return View(user.ToList());

                case "Group":
                    user = botAppContext.ToList().OrderBy(s => s.Group.Name);
                    return View(user.ToList());

                case "LastName":
                    user = botAppContext.ToList().OrderBy(s => s.Lastname);
                    return View(user.ToList());
                case "Reg":
                    user = botAppContext.ToList().OrderBy(s => s.Registered);
                    return View(user.ToList());
                case "Age":
                    user = botAppContext.ToList().OrderBy(s => s.AgeId);//не уверен по чему надо сортировать
                    return View(user.ToList());
                case "IsAdmin":
                    user = botAppContext.ToList().OrderBy(s => s.IsAdmin);
                    return View(user.ToList());
                case "IsTeacher":
                    user = botAppContext.ToList().OrderBy(s => s.IsTeacher);
                    return View(user.ToList());
                case "Balance":
                    user = botAppContext.ToList().OrderBy(s => s.Balance);
                    return View(user.ToList());
            }
            //user.ToListAs
            return View(await botAppContext.ToListAsync()); //если sortOrder пуст, то никак не сортируется
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Age)
                .Include(u => u.Group)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "AgeId");
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CreatedAt,GroupId,Platform,Firstname,Lastname,Phone,Registered,AgeId,IsAdmin,IsTeacher,Balance")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "AgeId", user.AgeId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", user.GroupId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "AgeId", user.AgeId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", user.GroupId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserId,CreatedAt,GroupId,Platform,Firstname,Lastname,Phone,Registered,AgeId,IsAdmin,IsTeacher,Balance")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            ViewData["AgeId"] = new SelectList(_context.Ages, "AgeId", "AgeId", user.AgeId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", user.GroupId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Age)
                .Include(u => u.Group)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
