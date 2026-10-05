using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Models;

namespace Soccer.Controllers;

public class TeamsController(SoccerContext context) : Controller
{
    // GET: Teams
    public async Task<IActionResult> Index()
    {
        // DbSet гарантовано не null у сучасному EF Core
        return View(await context.Teams.ToListAsync());
    }

    // GET: Teams/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var team = await context.Teams.FirstOrDefaultAsync(m => m.Id == id);
        return team is null ? NotFound() : View(team);
    }

    // GET: Teams/Create
    public IActionResult Create() => View();

    // POST: Teams/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Coach")] Team team)
    {
        if (!ModelState.IsValid) return View(team);

        context.Add(team);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Teams/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var team = await context.Teams.FindAsync(id);
        return team is null ? NotFound() : View(team);
    }

    // POST: Teams/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Coach")] Team team)
    {
        if (id != team.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(team);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(team);
    }

    // GET: Teams/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var team = await context.Teams.FirstOrDefaultAsync(m => m.Id == id);
        return team is null ? NotFound() : View(team);
    }

    // POST: Teams/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var team = await context.Teams.FindAsync(id);
        if (team is not null)
        {
            context.Teams.Remove(team);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool TeamExists(int id) => context.Teams.Any(e => e.Id == id);
}