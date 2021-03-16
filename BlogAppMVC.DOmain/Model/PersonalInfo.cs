using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BlogAppMVC.Domain.Model
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        [NotMapped]
        public List<IFormFile> Image { get; set; }
        public string Text { get; set; }
        public string SecondText { get; set; }
    }
}
