using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiveBuddy;
using DiveBuddy.Data;
using DiveBuddy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DiveBuddy.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly UserManager<ApplicationUser> _userManager; 
        public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser>um)
        {
            _context = context;
            _userManager = um; 
        }

        // GET: Reviews
        public async Task<IActionResult> Index(BusinessEnum selectType)//Helps to connect to the DB
        {
            Console.WriteLine("Selected " + selectType);
         // we want to pass all the locations from the controller to the view
            var locations = await _context.BuisnessModel.Where(w => w.Type == selectType).ToListAsync();
            return View(locations);                     //^ to help filer the types

        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewsModel = await _context.BuisnessModel //connects to db
                .Include(i => i.Reviews) //bc it's the views
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reviewsModel == null)
            {
                return NotFound();
            }

            return View(reviewsModel);
        }

        // GET: Reviews/Create
        public async Task<IActionResult> Create(int id)// connects to Db
        {   
            Console.WriteLine(id);
                    // we want to pass ONE location from the controller to the view
            var spot = await _context.BuisnessModel.FirstOrDefaultAsync(where => where.Id == id);
            return View(spot);                     //^ to help filer the types
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(int id, [Bind("Id,Rating,Review,CreatedAt,ApplicationUserID,BuisnessId")] ReviewsModel reviewsModel)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = await _userManager.GetUserAsync(HttpContext.User); //for the cookies to get user object
        //         reviewsModel.ApplicationUserID = user.Id; // get object, set user id
        //         _context.Add(reviewsModel);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", reviewsModel.ApplicationUserID);
        //     ViewData["BuisnessId"] = new SelectList(_context.BuisnessModel, "Id", "Id", reviewsModel.BuisnessId);
        //     return View(reviewsModel); 
        // }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View(new ReviewsModel{
                
            });
        
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
        [Authorize]
        //here's the create post from a user

        public async Task<IActionResult> Create([FromRoute]int id, [FromForm]string review) //review 
        {
            if (ModelState.IsValid)
            {
               var user = await _userManager.GetUserAsync(HttpContext.User);
                var newReview = new ReviewsModel
                {
                     Review = review, //comes from the model 
                     ApplicationUserID = user.Id,
                     BuisnessId = id, 
                };
                _context.ReviewsModel.Add(newReview);
                Console.WriteLine($"{newReview.Review}, {newReview.ApplicationUserID}");
                  // ^ writes a review, review shows up, userID shows up
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Reviews", new { id = newReview.Review});
            }
            return View(review);
        }
        // ^ ends here

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
