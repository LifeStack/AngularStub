using System;
using System.Collections.Generic;
using System.Linq;
using AngularStub.Domain;

namespace AngularStub.Data
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private ProductsContext _db = new ProductsContext();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public Product GetById(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public int Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product.Id; // "Yes, it's here." http://stackoverflow.com/a/5212787
        }

        public bool Update(Product product)
        {
            // Unnecessary in EF 6 http://stackoverflow.com/a/20451793/2496266
            // _db.Products.Attach(product);
            _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            return _db.SaveChanges() == 1;
        }

        public bool Update(Product[] products)
        {
            foreach (var product in products)
            {
                _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            }
            return _db.SaveChanges() == products.Length;
        }

        public bool Delete(int id)
        {
            var product = _db.Products.Find(id);
            _db.Products.Remove(product);
            return _db.SaveChanges() == 1;
        }

        public bool Delete(int[] ids)
        {
            foreach (var product in ids.Select(id => _db.Products.Find(id)))
            {
                _db.Products.Remove(product);
            }
            return _db.SaveChanges() == ids.Length;
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing || _db == null) return;
            _db.Dispose();
            _db = null;
        }
    }
}