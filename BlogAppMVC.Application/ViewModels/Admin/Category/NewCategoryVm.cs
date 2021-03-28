using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogAppMVC.Application.Mapping;

namespace BlogAppMVC.Application.ViewModels.Admin.Category
{
    public class NewCategoryVm : IMapFrom<Domain.Model.Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewCategoryVm, Domain.Model.Category>().ReverseMap();
        }
    }
}
