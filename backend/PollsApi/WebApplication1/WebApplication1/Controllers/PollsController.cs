
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollsApi.Models;
using PollsApi.Data;

public class PollsController : Controller
{
    private readonly AppDbContext _context;

    public PollsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: POLLS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Polls.ToListAsync());
    }

    // GET: POLLS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var poll = await _context.Polls
            .FirstOrDefaultAsync(m => m.Id == id);
        if (poll == null)
        {
            return NotFound();
        }

        return View(poll);
    }

    // GET: POLLS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: POLLS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,ReleaseDate,Genre,Price")] Poll poll)
    {
        if (ModelState.IsValid)
        {
            _context.Add(poll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(poll);
    }

    // GET: POLLS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var poll = await _context.Polls.FindAsync(id);
        if (poll == null)
        {
            return NotFound();
        }
        return View(poll);
    }

    // POST: POLLS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Poll poll)
    {
        if (id != poll.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(poll);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(poll.Id))
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
        return View(poll);
    }

    // GET: POLLS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var poll = await _context.Polls
            .FirstOrDefaultAsync(m => m.Id == id);
        if (poll == null)
        {
            return NotFound();
        }

        return View(poll);
    }

    // POST: POLLS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var poll = await _context.Polls.FindAsync(id);
        if (poll != null)
        {
            _context.Polls.Remove(poll);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PollExists(int? id)
    {
        return _context.Polls.Any(e => e.Id == id);
    }
}
