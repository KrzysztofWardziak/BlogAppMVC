using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAppMVC.Application.Interface;
using BlogAppMVC.Application.ViewModels.Admin;
using BlogAppMVC.Application.ViewModels.Admin.Blog;
using BlogAppMVC.Application.ViewModels.Admin.Category;
using BlogAppMVC.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlogAppMVC.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly Context _context;
        private readonly IWebHostEnvironment _host;

        public AdminController(IAdminService adminService, Context context, IWebHostEnvironment host)
        {
            _adminService = adminService;
            _context = context;
            _host = host;
        }

        [HttpGet]
        public ActionResult Categories()
        {
            var model = _adminService.GetAllCategories(12, 1, "");
            return View(model);
        }

        [HttpPost]
        public ActionResult Categories(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
                pageNo = 1;

            if(searchString is null)
                searchString = String.Empty;

            var model = _adminService.GetAllCategories(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        [HttpGet]
        public ActionResult AddNewCategory()
        {
            return View(new NewCategoryVm());
        }

        [HttpPost]
        public ActionResult AddNewCategory(NewCategoryVm model)
        {
            if (ModelState.IsValid)
            {
               // model.Slug = model.Name.Replace("", "-").ToLower();
                model.Sorting = 1000;
                var id = _adminService.AddCategory(model);
                return RedirectToAction("Categories");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            _adminService.DeleteCategory(id);
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var blog = _adminService.GetCategoryById(id);
            return View(blog);
        }

        [HttpPost]
        public ActionResult EditCategory(NewCategoryVm model)
        {
            if (ModelState.IsValid)
            {
                _adminService.EditCategory(model);      
                return RedirectToAction("Categories");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AllBlogs()
        {
            var model = _adminService.GetAllBlogsForList(12, 1, "");
            return View(model);
        }
        [HttpPost]
        public ActionResult AllBlogs(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }

            if (searchString is null)
            {
                searchString = String.Empty;
            }

            var model = _adminService.GetAllBlogsForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        [HttpGet]
        public ActionResult AddBlog()
        {
            NewBlogVm model = new NewBlogVm();
            model.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBlog(NewBlogVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            model.Categories = new SelectList(_context.Categories.ToList(), "Id", "");

            
            var id = _adminService.AddBlog(model);
            #region Upload Image
            string wwwRootPath = _host.WebRootPath;

            var path = Path.Combine(wwwRootPath + "/Content/img/blog/");
            var path2 = Path.Combine(wwwRootPath + "/Content/img/blog/" + id);
            var path3 = Path.Combine(wwwRootPath + "/Content/img/blog/" + id + "/Gallery/");
            var path4 = Path.Combine(wwwRootPath + "/Content/img/blog/" + id + "/Gallery/Thumbs");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!Directory.Exists(path2))
                Directory.CreateDirectory(path2);
            if (!Directory.Exists(path3))
                Directory.CreateDirectory(path3);
            if (!Directory.Exists(path4))
                Directory.CreateDirectory(path4);

            _adminService.SaveImage(model, id);

            #endregion
            return RedirectToAction("AllBlogs");

        }

        [HttpGet]
        public ActionResult EditBlog(int id)
        {
            NewBlogVm model = new NewBlogVm();
            model.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            var list = model.Categories;
            ViewBag.CategoryId = list;
            var blog = _adminService.GetBlogForEdit(id);

            string wwwRootPath = _host.WebRootPath;

            blog.GalleryImages = Directory
                    .EnumerateFiles(wwwRootPath + "/Content/img/blog/" + id + "/Gallery/Thumbs")
                    .Select(fn => Path.GetFileName(fn));
            

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBlog(NewBlogVm model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ta nazwa bloga jest już zajęta");
                return View(model);
            }

            model.Categories = new SelectList(_context.Categories.ToList(), "Id", "");
            if (model.Image != null)
            {
                _adminService.SaveImage(model, model.Id);
            }

            _adminService.UpdateBlog(model);
            string wwwRootPath = _host.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/Content/img/blog/" + model.Id + "/Gallery/Thumbs");
            model.GalleryImages = Directory.EnumerateFiles(wwwRootPath + "/Content/img/blog/" + model.Id + "/Gallery/Thumbs")
                .Select(fn => Path.GetFileName(fn));
                
            return RedirectToAction("AllBlogs");
        }

        public IActionResult DeleteBlog(int id)
        {
            var imageModel = _adminService.GetBlogDetails(id);
            var imagePath = Path.Combine(_host.WebRootPath, "Content/img/blog", imageModel.PhotoPath);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _adminService.DeleteBlog(id);
            return RedirectToAction("AllBlogs");
        }

        [HttpPost]
        public IActionResult SaveGalleryImages(int id)
        {
            foreach (var file in Request.Form.Files)
            {
                if (file != null && file.Length > 0)
                {
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string ext = Path.GetExtension(file.FileName);
                    string path = Path.Combine(wwwRootPath + "/Content/img/blog/" + id.ToString() + "/Gallery");
                    string path2 = Path.Combine(wwwRootPath + "/Content/img/blog/" + id.ToString() + "/Gallery/Thumbs");


                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    if (!Directory.Exists(path2))
                        Directory.CreateDirectory(path2);

                    string pathString = Path.Combine(path + "/" + fileName + ext);
                    string pathString2 = Path.Combine(path2 + "/" + fileName + ext);


                    using (Stream fileStream = new FileStream(pathString, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    using var image = Image.Load(file.OpenReadStream());
                    image.Mutate(x => x.Resize(200,200));
                    image.Save(pathString2);
                }
            }
            return RedirectToAction("AllBlogs");

        }

        //[HttpPost]
        public void DeleteImage(int id, string imageName)
        {
            string fullPath1 = Path.Combine(_host.WebRootPath + "/Content/img/blog/" + id.ToString() + "/Gallery/" + imageName);
            string fullPath2 = Path.Combine(_host.WebRootPath + "/Content/img/blog/" + id.ToString() + "/Gallery/Thumbs/" + imageName);

            if (System.IO.File.Exists(fullPath1))
                System.IO.File.Delete(fullPath1);

            if (System.IO.File.Exists(fullPath2))
                System.IO.File.Delete(fullPath2);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
