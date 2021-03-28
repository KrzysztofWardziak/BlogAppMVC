using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppMVC.Application.Interface;
using BlogAppMVC.Application.ViewModels.Admin;
using BlogAppMVC.Application.ViewModels.Admin.Blog;
using BlogAppMVC.Application.ViewModels.Admin.Category;
using BlogAppMVC.Domain.Interface;
using BlogAppMVC.Domain.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BlogAppMVC.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _host;

        public AdminService(IAdminRepository adminRepository, IMapper mapper, IHostingEnvironment host)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _host = host;
        }

        public ListCategoryForListVm GetAllCategories(int pageSize, int pageNo, string searchString)
        {
            var category = _adminRepository.GetAllCategories().Where(p => p.Name.StartsWith(searchString))
                .ProjectTo<CategoryVm>(_mapper.ConfigurationProvider).ToList();
            var categoryToShow = category.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            var categoryList = new ListCategoryForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                SearchString = searchString,
                Categories = categoryToShow,
                Count = category.Count
            };
            return categoryList;
        }

        public int AddCategory(NewCategoryVm category)
        {
            var cat = _mapper.Map<Category>(category);
            Category dto = new Category();
            dto.Name = category.Name;
            cat.Slug = category.Name.Replace(" ", "-").ToLower();
            var id = _adminRepository.AddCategory(cat);
            return id;
        }

        public void DeleteCategory(int categoryId)
        {
            _adminRepository.DeleteCategory(categoryId);
        }

        public void EditCategory(NewCategoryVm category)
        {
            var cat = _mapper.Map<Category>(category);
            Category dto = new Category();
            dto.Name = category.Name;
            cat.Slug = category.Name.Replace(" ", "-").ToLower();
            _adminRepository.EditCategory(cat);
        }

        public NewCategoryVm GetCategoryById(int id)
        {
            var cat = _adminRepository.GetCategoryById(id);
            var catVm = _mapper.Map<NewCategoryVm>(cat);
            return catVm;
        }

        public int AddBlog(NewBlogVm blog)
        {
            var date = DateTime.Now.ToString("F");
            blog.CreatedDate = date;

            var bl = _mapper.Map<BlogDetail>(blog);
            BlogDetail dto = new BlogDetail();
            dto.Title = blog.Title;
            bl.Slug = blog.Title.Replace(" ", "-").ToLower();
            _adminRepository.GetBlogById(blog.Id);
            //if (blog.Image != null)
            //    SaveImage(blog);
            var id = _adminRepository.AddBlog(bl);

            return id;
        }

        public BlogDetailVm GetBlogDetails(int blogId)
        {
            var blog = _adminRepository.GetBlogById(blogId);
            var blogVm = _mapper.Map<BlogDetailVm>(blog);

            return blogVm;
        }

        public void DeleteBlog(int blogId)
        {
            _adminRepository.DeleteBlog(blogId);
        }

        public ListBlogForListVm GetAllBlogsForList(int pageSize, int pageNo, string searchString)
        {
            var blogs = _adminRepository.GetAllBlogs().Where(p => p.Title.StartsWith(searchString))
                .ProjectTo<BlogForListVm>(_mapper.ConfigurationProvider).ToList();

            var blogsToShow = blogs.Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ToList();

            var blogList = new ListBlogForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                SearchString = searchString,
                Blogs = blogsToShow,
                Count = blogs.Count
            };
            return blogList;
        }

        public NewBlogVm GetBlogForEdit(int id)
        {
            var blog = _adminRepository.GetBlogById(id);
            var blogVm = _mapper.Map<NewBlogVm>(blog);

            return blogVm;
        }

        public void UpdateBlog(NewBlogVm blog)
        {
            var date = DateTime.Now.ToString("F");
            blog.ModifiedDate = date;
            
            var blogVm = _mapper.Map<BlogDetail>(blog);
            BlogDetail dto = new BlogDetail();
            blogVm.Slug = blog.Title.Replace(" ", "-").ToLower();
            _adminRepository.EditBlog(blogVm);
        }

        public NewBlogVm SaveImage(NewBlogVm model, int id)
        {
            foreach (var file in model.Image)
            {
                if (file != null && file.Length > 0)
                {
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string ext = Path.GetExtension(file.FileName);
                    model.PhotoPath = fileName = fileName + ext;
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

            return model;

        }
    }
}
