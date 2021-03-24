using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogAppMVC.Application.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppMVC.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _host;

        public BlogController(IBlogService blogService, IWebHostEnvironment host)
        {
            _blogService = blogService;
            _host = host;
        }

        public IActionResult BlogDetails(int id)
        {

            var blog = _blogService.GetBlogDetail(id);
            var wwwRootPath = _host.WebRootPath;
            blog.GalleryImages = Directory.EnumerateFiles(wwwRootPath + "/Content/img/blog/" + id + "/Gallery/Thumbs")
                    .Select(fn => Path.GetFileName(fn));
            return View(blog);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
