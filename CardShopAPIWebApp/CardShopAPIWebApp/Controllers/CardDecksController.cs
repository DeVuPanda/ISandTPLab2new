using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CardShopAPIWebApp.Model;

namespace CardShopAPIWebApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CardDecksController : Controller
    {
        private readonly DbcardsShopContext _context;

        public CardDecksController(DbcardsShopContext context)
        {
            _context = context;
        }

        // GET: CardDecks
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dbcardsShopContext = _context.CardDecks.Include(c => c.Firm);
            return View(await dbcardsShopContext.ToListAsync());
        }

        // GET: CardDecks/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardDeck = await _context.CardDecks
                .Include(c => c.Firm)
                .FirstOrDefaultAsync(m => m.DeckId == id);
            if (cardDeck == null)
            {
                return NotFound();
            }

            return View(cardDeck);
        }

        // GET: CardDecks/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId");
            return View();
        }

        // POST: CardDecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeckId,NumberOfCards,Description,DeckName,DeckPrice,FirmId")] CardDeck cardDeck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardDeck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId", cardDeck.FirmId);
            return View(cardDeck);
        }

        // GET: CardDecks/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardDeck = await _context.CardDecks.FindAsync(id);
            if (cardDeck == null)
            {
                return NotFound();
            }
            ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId", cardDeck.FirmId);
            return View(cardDeck);
        }

        // POST: CardDecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeckId,NumberOfCards,Description,DeckName,DeckPrice,FirmId")] CardDeck cardDeck)
        {
            if (id != cardDeck.DeckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardDeck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardDeckExists(cardDeck.DeckId))
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
            ViewData["FirmId"] = new SelectList(_context.Firms, "FirmId", "FirmId", cardDeck.FirmId);
            return View(cardDeck);
        }

        // GET: CardDecks/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardDeck = await _context.CardDecks
                .Include(c => c.Firm)
                .FirstOrDefaultAsync(m => m.DeckId == id);
            if (cardDeck == null)
            {
                return NotFound();
            }

            return View(cardDeck);
        }

        // POST: CardDecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardDeck = await _context.CardDecks.FindAsync(id);
            if (cardDeck != null)
            {
                _context.CardDecks.Remove(cardDeck);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardDeckExists(int id)
        {
            return _context.CardDecks.Any(e => e.DeckId == id);
        }
    }
}
