using System;
using System.Collections.Generic;
using System.Text;
using BlogAppMVC.Domain.Model;

namespace BlogAppMVC.Domain.Interface
{
    public interface IBlogRepository
    {
        BlogDetail GetBlogById(int blogId);
    }
}
