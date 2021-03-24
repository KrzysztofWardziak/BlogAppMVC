using System;
using System.Collections.Generic;
using System.Text;
using BlogAppMVC.Application.ViewModels.Admin.Blog;

namespace BlogAppMVC.Application.Interface
{
    public interface IBlogService
    {
        BlogDetailVm GetBlogDetail(int blogId);
    }
}
