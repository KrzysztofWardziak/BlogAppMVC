using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogAppMVC.Application.Mapping;

namespace BlogAppMVC.Application.ViewModels.Admin.Category
{
    public class CategoryForListVm : IMapFrom<Domain.Model.Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sorting { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Category, CategoryForListVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Sorting, opt => opt.MapFrom(s => s.Sorting));
        }
    }
}
