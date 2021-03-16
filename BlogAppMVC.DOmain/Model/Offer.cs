using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAppMVC.Domain.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Date { get; set; }
    }
}
