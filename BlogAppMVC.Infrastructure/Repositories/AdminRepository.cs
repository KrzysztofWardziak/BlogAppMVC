using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using BlogAppMVC.Domain.Interface;
using BlogAppMVC.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogAppMVC.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly Context _context;
        private readonly IHostingEnvironment _host;

        public AdminRepository(Context context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;
        }
        public IQueryable<Category> GetAllCategories()
        {
            var categories = _context.Categories;
            return categories;
        }

        public int AddCategory(Category category)
        {
            if (_context.Categories.Any(x => x.Name == category.Name))
            {
                return 0;
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return category.Id;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public void EditCategory(Category category)
        {
            if (!_context.Categories.Any(x => x.Name == category.Name))
            {
                _context.Attach(category);
                _context.Entry(category).Property("Name").IsModified = true;
                _context.Entry(category).Property("Slug").IsModified = true;
                _context.Entry(category).Property("Sorting").IsModified = true;
                _context.SaveChanges();
            }
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            return category;
        }

        public IQueryable<BlogDetail> GetAllBlogs()
        {
            var blogs = _context.BlogDetails;
            return blogs;
        }

        public int AddBlog(BlogDetail blog)
        {
            if (_context.BlogDetails.Any(x => x.Title == blog.Title))
            {

            }

            SaveImage(blog, blog.Id);
            _context.BlogDetails.Add(blog);
            _context.SaveChanges();
            return blog.Id;
        }

        public void DeleteBlog(int blogId)
        {
            var blog = _context.BlogDetails.Find(blogId);

            if (blog != null)
            {
                _context.BlogDetails.Remove(blog);
                _context.SaveChanges();
            }
        }

        public void EditBlog(BlogDetail blog)
        {
                _context.Attach(blog);
                _context.Entry(blog).Property("Title").IsModified = true;
                _context.Entry(blog).Property("Text").IsModified = true;
                _context.Entry(blog).Property("Slug").IsModified = true;
                _context.Entry(blog).Property("CategoryId").IsModified = true;
                _context.Entry(blog).Property("ModifiedDate").IsModified = true;
                _context.Entry(blog).Property("PhotoPath").IsModified = true;
                _context.SaveChanges();
        }

        public BlogDetail GetBlogById(int blogId)
        {
            var blog = _context.BlogDetails.Find(blogId);
            return blog;
        }

        public BlogDetail SaveImage(BlogDetail blog, int id)
        {
            foreach (var file in blog.Image)
            {
                if (file != null && file.Length > 0)
                {
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string ext = Path.GetExtension(file.FileName);
                    blog.PhotoPath = fileName = fileName + ext;
                    string path = Path.Combine(wwwRootPath + "/Content/img/blog/" + id);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string pathString1 = Path.Combine(path + "/" + fileName);

                    using (var fileStream = new FileStream(pathString1, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            return blog;

        }
    }
}
