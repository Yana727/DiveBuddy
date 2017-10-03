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
            List<Guid> iamgeIds = _context.PhotosModel.Select(m => m.Id).ToList();
            return View(iamgeIds);
        }

        [HttpPost]
        public IActionResult UploadImage(IList<IFormFile> files)
        {
            IFormFile uploadedImage = files.FirstOrDefault();
            if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);

                  //  System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    // var imageEntity = new PhotosModel()
                    // {
                    //     Id = Guid.NewGuid(),
                    //     Name = uploadedImage.Name,
                    //     Data = ms.ToArray(),
                    //     Width = image.Width,
                    //     Height = image.Height,
                    //     ContentType = uploadedImage.ContentType
                    // };

                   // this._context.PhotosModel.Add(imageEntity);

                    _context.SaveChanges();
                
            }

            return RedirectToAction("Index");
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
