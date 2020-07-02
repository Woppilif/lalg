﻿using System;
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
        public async Task<IActionResult> Index()
        {
            //ViewBag.
            var botAppContext = _context.Users.Include(u => u.Age).Include(u => u.Group);

            /*ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var user = from s in await botAppContext.ToListAsync()
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    user = user.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    user = user.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    user = user.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    user = user.OrderBy(s => s.LastName);
                    break;
            }*/

            return View(await botAppContext.ToListAsync());
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
