using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAppMVC.Application.ViewModels.Admin.Blog
{
    public class ListBlogForListVm
    {
        public List<BlogForListVm> Blogs { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
