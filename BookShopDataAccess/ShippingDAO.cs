using BookShopBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopDataAccess
{
    public class ShippingDAO
    {
        private readonly BookDbContext _context;

        public ShippingDAO(BookDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Shipping> GetAllShippings()
        {
            return _context.Shippings.ToList();
        }

        public Shipping GetShippingById(int id)
        {
            return _context.Shippings.Find(id);
        }

        public void AddShipping(Shipping shipping)
        {
            _context.Shippings.Add(shipping);
            _context.SaveChanges();
        }

        public void UpdateShipping(Shipping shipping)
        {
            _context.Shippings.Update(shipping);
            _context.SaveChanges();
        }

        public void DeleteShipping(int id)
        {
            var shipping = _context.Shippings.Find(id);
            if (shipping != null)
            {
                _context.Shippings.Remove(shipping);
                _context.SaveChanges();
            }
        }
    }

}
