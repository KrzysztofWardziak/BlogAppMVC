using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogAppMVC.Domain.Model;

namespace BlogAppMVC.Domain.Interface
{
    public interface IAdminRepository
    {
        IQueryable<Category> GetAllCategories();
        int AddCategory(Category category);
        void DeleteCategory(int categoryId);
        void EditCategory(Category category);
        Category GetCategoryById(int id);

        IQueryable<BlogDetail> GetAllBlogs();
        int AddBlog(BlogDetail blog);
        void DeleteBlog(int blogId);
        void EditBlog(BlogDetail blog);
        BlogDetail GetBlogById(int blogId);
        BlogDetail SaveImage(BlogDetail blog, int id);
    }
}
