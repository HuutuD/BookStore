using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopBusiness
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } 

        public ICollection<Shipping>? Shippings { get; set; }
    }
}
