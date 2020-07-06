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
    public class PaymentsController : Controller
    {
        private readonly BotAppContext _context;

        public PaymentsController(BotAppContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            var botAppContext = _context.Payments.Include(p => p.Subscription).Include(p => p.Users);

            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.CreatedAtgSortParm = sortOrder == "CreatedAt" ? "" : "CreatedAt";
            ViewBag.CapturedAtSortParm = sortOrder == "CapturedAt" ? "" : "CapturedAt";
            ViewBag.IsPayedSortParm = sortOrder == "IsPayed" ? "" : "IsPayed";
            ViewBag.SubscriptionSortParm = sortOrder == "Subscription" ? "" : "Subscription";
            ViewBag.PaymentIdSortParm = sortOrder == "PaymentId" ? "" : "PaymentId";
            ViewBag.IsExtendsSortParm = sortOrder == "IsExtends" ? "" : "IsExtends";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "" : "Amount";

            var user = from s in botAppContext
                       select s;

            if (!String.IsNullOrEmpty(search))
            {
                user = botAppContext.Where(s => s.Users.Firstname.ToUpper().Contains(search.ToUpper())
                                 || s.Amount.ToString().Contains(search.ToUpper())
                                 || s.CreatedAt.ToString().Contains(search)
                                 || s.Subscription.SubscriptionId.ToString().Contains(search)
                                 || s.CapturedAt.ToString().Contains(search));

                return View(user.ToList());
            }

            switch (sortOrder)
            {
                case "Name":
                    return View(user.ToList().OrderBy(s => s.Users.Firstname));
                case "CreatedAt":
                    return View(user.ToList().OrderBy(s => s.CreatedAt));
                case "CapturedAt":
                    return View(user.ToList().OrderBy(s => s.CapturedAt));
                case "IsPayed":
                    return View(user.ToList().OrderBy(s => s.IsPayed));
                case "Subscription":
                    return View(user.ToList().OrderBy(s => s.Subscription.SubscriptionId));
                case "PaymentId":
                    return View(user.ToList().OrderBy(s => s.PaymentId));
                case "IsExtends":
                    return View(user.ToList().OrderBy(s => s.IsExtends));
                case "Amount":
                    return View(user.ToList().OrderBy(s => s.Amount));
            }
            return View(await botAppContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Subscription)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CreatedAt,CapturedAt,IsPayed,SubscriptionId,Amount,PaymentId,IsExtends")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.Id = Guid.NewGuid();
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionId", payment.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", payment.UserId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionId", payment.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", payment.UserId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,CreatedAt,CapturedAt,IsPayed,SubscriptionId,Amount,PaymentId,IsExtends")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
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
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "SubscriptionId", "SubscriptionId", payment.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", payment.UserId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Subscription)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(Guid id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
