using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AutoMapper;
using BlogAppMVC.Application.Mapping;
using BlogAppMVC.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BlogAppMVC.Application.ViewModels.Admin.Blog
{
    public class NewBlogVm : IMapFrom<BlogDetail>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PhotoPath { get; set; }
        [NotMapped]
        public List<IFormFile> Image { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<string> GalleryImages { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewBlogVm, BlogDetail>().ReverseMap();
        }
    }
}
