using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AutoMapper;
using BlogAppMVC.Application.Mapping;
using BlogAppMVC.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace BlogAppMVC.Application.ViewModels.Admin.Blog
{
    public class BlogForListVm : IMapFrom<BlogDetail>
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

        public IEnumerable<Domain.Model.Category> Categories { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.BlogDetail, BlogForListVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Text))
                .ForMember(d => d.PhotoPath, opt => opt.MapFrom(s => s.PhotoPath))
                .ForMember(d => d.Image, opt => opt.MapFrom(s => s.Image))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate))
                .ForMember(d => d.ModifiedDate, opt => opt.MapFrom(s => s.ModifiedDate))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId));
        }
    }
}
