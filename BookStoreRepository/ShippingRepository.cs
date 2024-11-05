using BookShopBusiness;
using BookShopDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly ShippingDAO _shippingDAO;

        public ShippingRepository(BookDbContext context)
        {
            _shippingDAO = new ShippingDAO(context);
        }

        public IEnumerable<Shipping> GetAllShippings()
        {
            return _shippingDAO.GetAllShippings();
        }

        public Shipping GetShippingById(int id)
        {
            return _shippingDAO.GetShippingById(id);
        }

        public void AddShipping(Shipping shipping)
        {
            _shippingDAO.AddShipping(shipping);
        }

        public void UpdateShipping(Shipping shipping)
        {
            _shippingDAO.UpdateShipping(shipping);
        }

        public void DeleteShipping(int id)
        {
            _shippingDAO.DeleteShipping(id);
        }
    }

}
