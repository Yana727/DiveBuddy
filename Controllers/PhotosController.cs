using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiveBuddy;
using DiveBuddy.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing; 

namespace DiveBuddy.Controllers
{
    public class PhotosController : Controller
    {
        public readonly ApplicationDbContext _context;
        public PhotosController(ApplicationDbContext context)
        {
            this._context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name")]PhotosModel photos)
        {
             if (ModelState.IsValid)
        {
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {

                    var file = Image;
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads\\img\\photosModel");

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse
                            (file.ContentDisposition).FileName.Trim('"');

                        System.Console.WriteLine(fileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            PhotosModel.Name = file.FileName;
                        }


                    }
                }
            }
                    _context.SaveChanges();
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index"); 
                
         }

        else
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
        }
        return View(model: PhotosModel);
    }

        [HttpGet]
        public FileStreamResult ViewImage(Guid id)
        {
           
            var image = _context.PhotosModel.FirstOrDefault(m => m.Id == id);

            MemoryStream ms = new MemoryStream(image.Data);

            return new FileStreamResult(ms, image.ContentType);
           
        }
    }
}
