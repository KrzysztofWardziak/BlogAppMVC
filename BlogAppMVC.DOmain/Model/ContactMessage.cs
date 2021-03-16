using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAppMVC.Domain.Model
{
    public class ContactMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public bool IsImportant { get; set; }
    }
}
