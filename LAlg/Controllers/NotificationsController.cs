using BotAppData;
using BotAppData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAlg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly BotAppContext _context;

        public NotificationsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> Index()
        {
            var botAppContext = _context.Lessons.Where(l => l.Status == true).Include(l => l.Pattern);
            return await botAppContext.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Notifications
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
