using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogAppMVC.Application.Interface;
using BlogAppMVC.Application.ViewModels.Admin.Blog;
using BlogAppMVC.Application.ViewModels.Admin.Category;
using BlogAppMVC.Domain.Interface;

namespace BlogAppMVC.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public BlogDetailVm GetBlogDetail(int blogId)
        {
            var blog = _blogRepository.GetBlogById(blogId);
            var blogVm = _mapper.Map<BlogDetailVm>(blog);
            return blogVm;
        }
    }
}
