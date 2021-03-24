using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogAppMVC.Domain.Interface;
using BlogAppMVC.Domain.Model;

namespace BlogAppMVC.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly Context _context;

        public BlogRepository(Context context)
        {
            _context = context;
        }


        public BlogDetail GetBlogById(int blogId)
        {
            var blog = _context.BlogDetails.FirstOrDefault(b => b.Id == blogId);
            return blog;
        }
    }
}
