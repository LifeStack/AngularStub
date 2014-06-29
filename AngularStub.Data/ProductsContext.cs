using System.Data.Entity;
using AngularStub.Domain;

namespace AngularStub.Data
{
    [DbConfigurationType(typeof(ProductsConfiguration))]
    public class ProductsContext : DbContext
    {
        public ProductsContext()
            : base("name=ProductsContext")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}