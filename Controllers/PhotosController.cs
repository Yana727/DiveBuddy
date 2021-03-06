using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DiveBuddy.Data;
using DiveBuddy.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiveBuddy.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public PhotosController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            this._environment = appEnvironment;
        }
        public async Task<IActionResult> Create(int? id)
        {
            var pix = await _context.BuisnessModel//connects to db
            .SingleOrDefaultAsync(m => m.Id == id);
            if (pix == null)
            {
                return NotFound();
            }
            return View(pix);
        }
        // UPLOAD: the endpoint to accept the picture
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute]int id)
        {
            var image = new PhotosModel();
            image.BuisnessId = id;
            
            // UPLOAD: grabs the files from the incoming form
            var files = HttpContext.Request.Form.Files;
            Console.WriteLine($"{image.Id}");
            // UPLOAD: processes each file
            foreach (var _image in files)
            {
                if (_image != null && _image.Length > 0)
                {

                    var file = _image;
                    // UPLOAD: sets the path of the where the file is stored on the server
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads/images");

                    if (file.Length > 0)
                    {
                        // UPLOAD: creates a new unique file name to store in the uploads folder 
                        var fileName = Guid.NewGuid().ToString() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var _filePath = Path.Combine(uploads, fileName);


                        // UPLOAD: Saves file to local server
                        using (var fileStream = new FileStream(_filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);

                            // UPLOAD: sets propertoies on the new model
                            image.Url = fileName;
                            image.PicName = file.FileName;
                        }
                    }
                }
            }

            // UPLOAD: saves model to database
            _context.Add(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Reviews", new {id = id});
        }                         //action     controller   param 

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }

    }
}