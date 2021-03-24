using System;
using System.Collections.Generic;
using System.Text;
using BlogAppMVC.Application.ViewModels.Admin;
using BlogAppMVC.Application.ViewModels.Admin.Blog;
using BlogAppMVC.Application.ViewModels.Admin.Category;
using Microsoft.AspNetCore.Http;

namespace BlogAppMVC.Application.Interface
{
    public interface IAdminService
    {
        ListCategoryForListVm GetAllCategories(int pageSize, int pageNo, string searchString);
        int AddCategory(NewCategoryVm category);
        void DeleteCategory(int categoryId);
        void EditCategory(NewCategoryVm category);
        NewCategoryVm GetCategoryById(int id);

        int AddBlog(NewBlogVm blog);
        BlogDetailVm GetBlogDetails(int blogId);
        void DeleteBlog(int blogId);
        ListBlogForListVm GetAllBlogsForList(int pageSize, int pageNo, string searchString);
        NewBlogVm GetBlogForEdit(int id);
        void UpdateBlog(NewBlogVm blog);
        NewBlogVm SaveImage(NewBlogVm model, int id);
    }
}
