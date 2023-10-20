using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P08_Videogames.Data;
using P08_Videogames.Models;

namespace P08_Videogames.Controllers
{
    public class GamezsController : Controller
    {
        private readonly GamezContext _context;

        public GamezsController(GamezContext context)
        {
            _context = context;
        }

        // GET: Gamezs
        public async Task<IActionResult> Index(string bTitle,string bRat, string bPlat, string bPlayers, string bGen)
        {
            //Recupera info de la DB
            var gam = from info in _context.LosGamez
                      select info;
            //Recupera todas las entradas definidas como Genre
            IQueryable<string> genquery = from info in _context.LosGamez
                                        orderby info.Genre
                                        select info.Genre;
            //Recupera todas las entradas definidas como Players
            IQueryable<string> playquery = from info in _context.LosGamez
                                          orderby info.Players
                                          select info.Players;
            //Recupera todas las entradas definidas como Platform
            IQueryable<string> platquery = from info in _context.LosGamez
                                          orderby info.Platform
                                          select info.Platform;
            //Recupera todas las entradas definidas como Rating
            IQueryable<string> ratquery = from info in _context.LosGamez
                                           orderby info.Rating
                                           select info.Rating;
            //Validar la cadena de busqueda para titulo
            if (!String.IsNullOrEmpty(bTitle))
            {
                gam=gam.Where(t=>t.Name.Contains(bTitle));
            }
            //Validar la cadena de busqueda para Genre
            if (!String.IsNullOrEmpty(bGen))
            {
                gam = gam.Where(g => g.Genre==bGen);
            }
            //Validar la cadena de busqueda para Players
            if (!String.IsNullOrEmpty(bPlayers))
            {
                gam = gam.Where(p => p.Players == bPlayers);
            }
            //Validar la cadena de busqueda para Platform
            if (!String.IsNullOrEmpty(bPlat))
            {
                gam = gam.Where(pl => pl.Platform == bPlat);
            }
            //Validar la cadena de busqueda para Rating
            if (!String.IsNullOrEmpty(bRat))
            {
                gam = gam.Where(r => r.Rating == bRat);
            }
            //Genera un recipiente de tipo BuscaVM para capturar todas las
            //posibles combinaciones de busquedas
            var buscarVM = new BuscaVM
            {
                Rati = new SelectList(await ratquery.Distinct().ToListAsync()),
                Plat = new SelectList(await platquery.Distinct().ToListAsync()),
                Players = new SelectList(await playquery.Distinct().ToListAsync()),
                Gen = new SelectList(await genquery.Distinct().ToListAsync()),
                Games = await gam.ToListAsync()
            };

            return View(buscarVM);

        }

        // GET: Gamezs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamez = await _context.LosGamez
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamez == null)
            {
                return NotFound();
            }

            return View(gamez);
        }

        // GET: Gamezs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gamezs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,Release,Publisher,Developer,Platform,Players,Rating,Price,Description")] Gamez gamez)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamez);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamez);
        }

        // GET: Gamezs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamez = await _context.LosGamez.FindAsync(id);
            if (gamez == null)
            {
                return NotFound();
            }
            return View(gamez);
        }

        // POST: Gamezs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,Release,Publisher,Developer,Platform,Players,Rating,Price,Description")] Gamez gamez)
        {
            if (id != gamez.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamez);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamezExists(gamez.Id))
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
            return View(gamez);
        }

        // GET: Gamezs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamez = await _context.LosGamez
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamez == null)
            {
                return NotFound();
            }

            return View(gamez);
        }

        // POST: Gamezs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamez = await _context.LosGamez.FindAsync(id);
            _context.LosGamez.Remove(gamez);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamezExists(int id)
        {
            return _context.LosGamez.Any(e => e.Id == id);
        }
    }
}
