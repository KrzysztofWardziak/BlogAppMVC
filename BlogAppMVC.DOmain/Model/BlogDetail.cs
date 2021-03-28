using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogAppMVC.Domain.Model
{
    public class BlogDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Slug { get; set; }
        public string PhotoPath { get; set; }
        [NotMapped]
        public List<IFormFile> Image { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public IEnumerable<Photo> GalleryImages { get; set; }
        //public IEnumerable<Category> Categories { get; set; }
    }
}
