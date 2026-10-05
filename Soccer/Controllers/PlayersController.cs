using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soccer.Models;

namespace Soccer.Controllers;

// Використовуємо первинний конструктор (C# 12)
public class PlayersController(SoccerContext context) : Controller
{
    // GET: Players
    public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
    {
        // Використовуємо var або явний тип без зайвого null-forgiving
        var players = context.Players.Include(x => x.Team).AsQueryable();

        ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
        ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
        ViewData["PositionSort"] = sortOrder == SortState.PositionAsc ? SortState.PositionDesc : SortState.PositionAsc;
        ViewData["TeamsSort"] = sortOrder == SortState.TeamAsc ? SortState.TeamDesc : SortState.TeamAsc;

        players = sortOrder switch
        {
            SortState.NameDesc => players.OrderByDescending(s => s.Name),
            SortState.AgeAsc => players.OrderBy(s => DateTime.Now.Year - s.BirthYear),
            SortState.AgeDesc => players.OrderByDescending(s => DateTime.Now.Year - s.BirthYear),
            SortState.PositionAsc => players.OrderBy(s => s.Position),
            SortState.PositionDesc => players.OrderByDescending(s => s.Position),
            SortState.TeamAsc => players.OrderBy(s => s.Team!.Name),
            SortState.TeamDesc => players.OrderByDescending(s => s.Team!.Name),
            _ => players.OrderBy(s => s.Name),
        };

        return View(await players.ToListAsync());
    }

    // GET: Players/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound(); // Використання is null замість == null

        var player = await context.Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(m => m.Id == id);

        return player is null ? NotFound() : View(player);
    }

    // GET: Players/Create
    public IActionResult Create()
    {
        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name");
        return View();
    }

    // POST: Players/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,BirthYear,Position,TeamId")] Player player)
    {
        // Переклад повідомлення українською
        if (DateTime.Now.Year - player.BirthYear <= 0)
            ModelState.AddModelError("BirthYear", "Вік має бути більшим за нуль");

        if (ModelState.IsValid)
        {
            context.Add(player);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name", player.TeamId);
        return View(player);
    }

    // GET: Players/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var player = await context.Players.FindAsync(id);
        if (player is null) return NotFound();

        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name", player.TeamId);
        return View(player);
    }

    // POST: Players/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BirthYear,Position,TeamId")] Player player)
    {
        if (id != player.Id) return NotFound();

        // Переклад повідомлення українською
        if (DateTime.Now.Year - player.BirthYear <= 0)
            ModelState.AddModelError("BirthYear", "Вік має бути більшим за нуль");

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(player);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(player.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["TeamId"] = new SelectList(context.Teams, "Id", "Name", player.TeamId);
        return View(player);
    }

    // GET: Players/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var player = await context.Players
            .Include(p => p.Team)
            .FirstOrDefaultAsync(m => m.Id == id);

        return player is null ? NotFound() : View(player);
    }

    // POST: Players/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var player = await context.Players.FindAsync(id);
        if (player is not null)
        {
            context.Players.Remove(player);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool PlayerExists(int id) => context.Players.Any(e => e.Id == id);
}