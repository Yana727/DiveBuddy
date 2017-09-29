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
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReviewsModel.Include(r => r.ApplicationUser).Include(r => r.Buisness);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // this is for the mock purposes
            return View(new ReviewsModel{
                
            });

            if (id == null)
            {
                return NotFound();
            }

            var reviewsModel = await _context.ReviewsModel
                .Include(r => r.ApplicationUser)
                .Include(r => r.Buisness)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reviewsModel == null)
            {
                return NotFound();
            }

            return View(reviewsModel);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BuisnessId"] = new SelectList(_context.BuisnessModel, "Id", "Id");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating,Review,CreatedAt,ApplicationUserID,BuisnessId")] ReviewsModel reviewsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviewsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", reviewsModel.ApplicationUserID);
            ViewData["BuisnessId"] = new SelectList(_context.BuisnessModel, "Id", "Id", reviewsModel.BuisnessId);
            return View(reviewsModel);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewsModel = await _context.ReviewsModel.SingleOrDefaultAsync(m => m.Id == id);
            if (reviewsModel == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", reviewsModel.ApplicationUserID);
            ViewData["BuisnessId"] = new SelectList(_context.BuisnessModel, "Id", "Id", reviewsModel.BuisnessId);
            return View(reviewsModel);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,Review,CreatedAt,ApplicationUserID,BuisnessId")] ReviewsModel reviewsModel)
        {
            if (id != reviewsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviewsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsModelExists(reviewsModel.Id))
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
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", reviewsModel.ApplicationUserID);
            ViewData["BuisnessId"] = new SelectList(_context.BuisnessModel, "Id", "Id", reviewsModel.BuisnessId);
            return View(reviewsModel);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewsModel = await _context.ReviewsModel
                .Include(r => r.ApplicationUser)
                .Include(r => r.Buisness)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reviewsModel == null)
            {
                return NotFound();
            }

            return View(reviewsModel);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviewsModel = await _context.ReviewsModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.ReviewsModel.Remove(reviewsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsModelExists(int id)
        {
            return _context.ReviewsModel.Any(e => e.Id == id);
        }
    }
}
