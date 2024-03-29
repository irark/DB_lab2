﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DB_lab2;

namespace DB_lab2.Controllers
{
    public class ActorsController : Controller
    {
        private readonly DBFilmsContext _context;

        public ActorsController(DBFilmsContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actors.ToListAsync());
        }


        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Actor actor)
        {
            if (!IsDuplicate(actor))
            {

                if (ModelState.IsValid)
                {
                    _context.Add(actor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(actor);
            }
            else
                ModelState.AddModelError("Name", "Такий актор уже існує");

            return View(actor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }
            var model = _context.Actors.FirstOrDefault(a => a.Name.Equals(actor.Name) && a.Id != id);
            if (model == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(actor);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ActorExists(actor.Id))
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
                return View(actor);
            }
            else
                ModelState.AddModelError("Name", "Такий актор уже існує");

            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            var filmActorRelationships = _context.FilmActorRelationships.Where(r => r.ActorId == id).ToList();
            foreach (var item in filmActorRelationships)
            {
                _context.FilmActorRelationships.Remove(item);
                await _context.SaveChangesAsync();
            }
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
        private bool IsDuplicate(Actor model)
        {
            var actor = _context.Actors.FirstOrDefault(a => a.Name.Equals(model.Name));

            return actor == null ? false : true;
        }

    }
}
