using BotAppData;
//using BotAppData.CacheService;
using BotAppData.Models;
using BotAppData.RedisService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        //private ILogger<UsersController> logger;
        private readonly BotAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RedisService _redisService;
        //private Cache cache = new Cache();
        //private readonly IDistributedCache _distributedCache;

        public UsersController(BotAppContext context, UserManager<IdentityUser> userManager, RedisService redisService)//, IDistributedCache distributedCache
        {
            _context = context;
            _userManager = userManager;
            _redisService = redisService;
            //_distributedCache = distributedCache;
        }

        // GET: Users
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date = DateTime.UtcNow.ToString();
            await _redisService.Set("{AAAidAAA}", date);
            var definitely = await _redisService.Get("{AAAidAAA}");
            ViewBag.Cache = definitely;

            var botAppContext = _context.Users.Where(u => u.GroupId == id).Include(u => u.Age).Include(u => u.Group);
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
