using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopBusiness
{
    public class Shipping
    {
        public int ShippingId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime DateShip { get; set; }
        public string LocationShip { get; set; }

        public int UserSubmitId { get; set; }
        public User UserSubmit { get; set; }

        public int UserApproveId { get; set; }
        public User UserApprove { get; set; }
    }
}
