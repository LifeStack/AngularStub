using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AngularStub.Data;
using AngularStub.Domain;

namespace AngularStub.Web.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _pRepo;

        public ProductsController(IProductRepository pRepo)
        {
            _pRepo = pRepo;
        }

        // GET api/values
        public IEnumerable<Product> Get()
        {
            return _pRepo.GetAll();
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            var product = _pRepo.GetById(id);
            return product == null ? (IHttpActionResult) NotFound() : Ok(product);
        }

        // POST api/values
        public IHttpActionResult Post(Product product)
        {
            try
            {
                var id = _pRepo.Add(product);
                product = _pRepo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new HttpException(HttpStatusCode.InternalServerError.ToString(), ex);
            }
            return Ok(product);
        }

        // PUT api/values/5
        public IHttpActionResult Put(int id, Product product)
        {
            product.Id = id;
            if (!_pRepo.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotModified);
            }
            return Ok(product);
        }

        // DELETE api/values/5
        public HttpStatusCode Delete(int id)
        {
            return _pRepo.Delete(id)
                ? HttpStatusCode.NoContent
                : HttpStatusCode.NotModified;
        }

        // TODO http://www.codeproject.com/Tips/710116/Passing-Object-Collections-to-ASP-NET-Web-API
        // TODO (Update/Delete multiple records)
    }
}