using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogAppMVC.Application.Interface;
using BlogAppMVC.Application.ViewModels.Admin.Blog;
using BlogAppMVC.Application.ViewModels.Admin.Category;
using BlogAppMVC.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppMVC.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _host;
        private readonly Context _context;


        public BlogController(IBlogService blogService, IWebHostEnvironment host, Context context)
        {
            _blogService = blogService;
            _host = host;
            _context = context;
        }

        public IActionResult BlogDetails(int id)
        {

            var blog = _blogService.GetBlogDetail(id);
            var wwwRootPath = _host.WebRootPath;
            blog.GalleryImages = Directory.EnumerateFiles(wwwRootPath + "/Content/img/blog/" + id + "/Gallery/Thumbs")
                    .Select(fn => Path.GetFileName(fn));
            return View(blog);
        }

        public IActionResult CategoryMenuPartial()
        {
            List<CategoryVm> categoryVmList;

            categoryVmList = _context.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryVm(x))
                    .ToList();
            return PartialView(categoryVmList);
        }


        public ActionResult Category(string name)
        {
            List<BlogDetailVm> blogVmList;

            Domain.Model.Category category = _context.Categories.Where(x => x.Slug == name).FirstOrDefault();
            int catId = category.Id;

            blogVmList = _context.BlogDetails.ToArray().Where(x => x.CategoryId == catId)
                .Select(x => new BlogDetailVm(x))
                .ToList();

            //var blogCat = _context.BlogDetails.Where(x => x.CategoryId == catId).FirstOrDefault();

            return View(blogVmList);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
