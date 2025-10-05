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
        private readonly ProductDAL _productDAL = new ProductDAL();

        public List<Product> GetAll()
        {
            return _productDAL.GetAll();
        }

        public void Insert(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new Exception("El nombre del producto es obligatorio.");

            if (product.Price <= 0)
                throw new Exception("El precio debe ser mayor que cero.");

            _productDAL.Insert(product);
        }

        public void Update(Product product)
        {
            if (product.ProductID <= 0)
                throw new Exception("Debe seleccionar un producto válido.");

            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new Exception("El nombre del producto es obligatorio.");

            if (product.Price <= 0)
                throw new Exception("El precio debe ser mayor que cero.");

            _productDAL.Update(product);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new Exception("Debe seleccionar un producto válido para eliminar.");

            _productDAL.Delete(id);
        }
    }
}
