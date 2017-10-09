using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiveBuddy;
using DiveBuddy.Data;

namespace DiveBuddy.Controllers
{
    public class BuisnessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuisnessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Buisness
        public async Task<IActionResult> Index()
        {
            return View(await _context.BuisnessModel.ToListAsync());
        }

        // GET: Buisness/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buisnessModel = await _context.BuisnessModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (buisnessModel == null)
            {
                return NotFound();
            }

            return View(buisnessModel);
        }

        // GET: Buisness/Create
        public IActionResult Create()
        {
            return View();
        }
        

        // POST: Buisness/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Address,PhoneNbr, Website, Type")] BuisnessModel buisnessModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buisnessModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buisnessModel);
        }

        // GET: Buisness/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buisnessModel = await _context.BuisnessModel.SingleOrDefaultAsync(m => m.Id == id);
            if (buisnessModel == null)
            {
                return NotFound();
            }
            return View(buisnessModel);
        }

        // POST: Buisness/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Address,PhoneNbr, Website")] BuisnessModel buisnessModel)
        {
            if (id != buisnessModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buisnessModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuisnessModelExists(buisnessModel.Id))
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
            return View(buisnessModel);
        }

        // GET: Buisness/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buisnessModel = await _context.BuisnessModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (buisnessModel == null)
            {
                return NotFound();
            }

            return View(buisnessModel);
        }

        // POST: Buisness/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buisnessModel = await _context.BuisnessModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.BuisnessModel.Remove(buisnessModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuisnessModelExists(int id)
        {
            return _context.BuisnessModel.Any(e => e.Id == id);
        }
    }
}
