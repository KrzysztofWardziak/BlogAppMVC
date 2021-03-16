using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAppMVC.Domain.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string FirstPhotoUrl { get; set; }
        public string SecondPhotoUrl { get; set; }
        public string ThirdPhotoUrl { get; set; }
        public string FourthPhotoUrl { get; set; }
        public string FifthPhotoUrl { get; set; }
        public string SixthPhotoUrl { get; set; }
        public string SeventhPhotoUrl { get; set; }
        public string EightPhotoUrl { get; set; }
        public string NinthPhotoUrl { get; set; }
        public string TenthPhotoUrl { get; set; }
        public int BlogId { get; set; }
    }
}
