using System.Collections.Generic;
using AngularStub.Domain;

namespace AngularStub.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        int Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
        // TODO Update/Delete multiple records (already implemented)
    }
}