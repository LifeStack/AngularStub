using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer; 

namespace AngularStub.Data
{
    public class ProductsConfiguration : DbConfiguration
    {
        public ProductsConfiguration() 
        {
            SetDatabaseInitializer(new CreateDatabaseIfNotExists<ProductsContext>());
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
            SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
        } 
    }
}
