using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogAppMVC.Application.Mapping;
using BlogAppMVC.Domain.Model;

namespace BlogAppMVC.Application.ViewModels.Admin.Category
{
    public class CategoryVm : IMapFrom<Domain.Model.Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }

        public CategoryVm()
        {
            
        }
        public CategoryVm(Domain.Model.Category row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Sorting = row.Sorting;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Category, CategoryVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Slug, opt => opt.MapFrom(s => s.Slug))
                .ForMember(d => d.Sorting, opt => opt.MapFrom(s => s.Sorting));
        }
    }
}
