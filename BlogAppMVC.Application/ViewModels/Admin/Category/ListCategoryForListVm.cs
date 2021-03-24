using System;
using System.Collections.Generic;
using System.Text;
using BlogAppMVC.Application.ViewModels.Admin.Category;

namespace BlogAppMVC.Application.ViewModels.Admin.Category
{
    public class ListCategoryForListVm
    {
        public List<CategoryVm> Categories { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
