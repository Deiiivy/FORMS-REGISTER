using AplicacionFormRegistros.DAL.DAL;
using AplicacionFormRegistros.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionFormRegistros.BLL
{
    public class ProductBLL
    {
        private ProductDAL productDAL = new ProductDAL();

        public List<Product> GetAll()
        {
            return productDAL.GetAll();
        }

        public void Insert(Product product)
        {
            productDAL.Insert(product);
        }

        public void Update(Product product)
        {
            productDAL.Update(product);
        }

        public void Delete(int id)
        {
            productDAL.Delete(id);
        }
    }
}
