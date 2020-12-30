using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CookingSystem.Data;
using CookingSystem.Data.Models;
using CookingSystem.Web.Models.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CookingSystem.Web.Controllers
{
    public class ImagesController : Controller
    {
        private readonly CookingSystemDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IMapper mapper;

        public ImagesController(CookingSystemDbContext context, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult All()
        {
            var images = this.context
                .Images
                .Select(x => new ImageModel
            {
                Name = x.Name,
            }).ToList();
            return this.View(images);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageFile")] ImageModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(imageModel);
            }

            //Save image to wwwroot/image
            string wwwRootPath = this.webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            imageModel.Name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/img/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageModel.ImageFile.CopyToAsync(fileStream);
            }
            //Insert record

            var image = this.mapper.Map<Image>(imageModel);
            this.context.Add(image);
            await this.context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
