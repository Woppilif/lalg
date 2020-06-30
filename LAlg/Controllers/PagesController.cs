using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Route("[controller]/[action]")]
    public class PagesController : Controller
    {
        private readonly BotAppContext _context;

        public PagesController(BotAppContext context)
        {
            _context = context;
        }

        [HttpGet("{bot}")]
        public IActionResult Index(string bot)
        {
            if (bot == "Telegram")
            {
                ViewBag.Url = "tg://resolve?domain=langalgobot";
                ViewBag.Bot = "Telegram";
            }
            else if (bot == "Vk")
            {
                ViewBag.Url = "https://vk.com/im?sel=-186318251";
                ViewBag.Bot = "Vk";
            }
            else
            {
                ViewBag.Bot = "Ошибка";
                ViewBag.Url = "/";
            }
            return View();
        }

        [HttpGet("{groupId}")]
        public IActionResult Group(Guid groupId)
        {
            var group = _context.Groups.Find(groupId);
            if (group == null)
            {
                return NotFound();
            }
            ViewBag.GroupId = groupId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CreatedAt,GroupId,Platform,Firstname,Lastname,Phone,Registered,AgeId,IsAdmin,IsTeacher")] User user)
        {
            user.UserId = (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            user.CreatedAt = DateTime.Now;
            user.Platform = 3;
            user.Lastname = "";
            user.Registered = false;
            user.AgeId = 4;
            user.IsAdmin = false;
            user.IsTeacher = false;
            if (ModelState.IsValid)
            {
                _context.Add(user);
                string code = Guid.NewGuid().ToString().Substring(0, 5);
                _context.Referals.Add(new Referal { Code = code, 
                    UserId = user.UserId, 
                    IsActive = true, IsCommon = false, 
                    ReferalId = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                    Amount = 500.0m
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Code), new { code = code });
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Code(string code)
        {
            ViewBag.Code = code;
            return View();
        }
    }
}