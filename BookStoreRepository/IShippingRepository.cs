using BookShopBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository
{
    public interface IShippingRepository
    {
        IEnumerable<Shipping> GetAllShippings();
        Shipping GetShippingById(int id);
        void AddShipping(Shipping shipping);
        void UpdateShipping(Shipping shipping);
        void DeleteShipping(int id);
    }

}
