using DiveBuddy.Data;
using Microsoft.AspNetCore.Mvc;

namespace DiveBuddy.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotosController(ApplicationDbContext context) 
        {
            _context = context;
        }
       // public async Task<IActionResult> Index()
      //  {
        //    return View(await _context.Images.ToListAsync());
      //  }
    }
}