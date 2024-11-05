using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ShippingDTO
    {
        public int ShippingId { get; set; }
        public int BookId { get; set; }
        public DateTime DateShip { get; set; }
        public string LocationShip { get; set; }
        public int UserSubmitId { get; set; }
        public int UserApproveId { get; set; }
        public string BookName {  get; set; }

    }
}
