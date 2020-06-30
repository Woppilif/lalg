using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkSpyersController : ControllerBase
    {
        private readonly BotAppContext _context;

        public LinkSpyersController(BotAppContext context)
        {
            _context = context;
        }

        // GET: api/LinkSpyers/5
        [HttpGet("{lessonId}")]
        public async Task<IActionResult> GetLinkSpyer(Guid lessonId)
        {
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson == null)
            {
                return NotFound();
            }
            var linkSpyer = new LinkSpyer() { Lesson = lesson };
            _context.LinkSpyers.Add(linkSpyer);
            await _context.SaveChangesAsync();
            return Redirect(lesson.Url);
        }

        [HttpGet("{lessonId}/{chatid}")]
        public async Task<IActionResult> GetLinkSpyerChatId(Guid lessonId, long chatid)
        {
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson == null)
            {
                return NotFound();
            }
            var linkSpyer = new LinkSpyer() { Lesson = lesson, UserId = chatid };
            _context.LinkSpyers.Add(linkSpyer);
            await _context.SaveChangesAsync();
            return Redirect(lesson.Url);
        }

        [HttpGet]
        public IActionResult OpenTelegram()
        {
            return Redirect("tg://resolve?domain=langalgobot");
        }
    }
}
