using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Products;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, IDeleteProductService deleteProductService, IGetProductService getProductService, IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var validationErrors = _updateProductService.ValidateModelInputs(model.Name, model.Price, model.Description);
            if (validationErrors.Any())
            {
                return BadRequestResponse(string.Join(",", validationErrors));
            }
            var existingProduct = _getProductService.GetProduct(productId);
            if (existingProduct != null)
            {
                return BadRequestResponse("Record with the same ID already exists");
            }
            var product = _createProductService.Create(productId, model.Name, model.Price, model.Description);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }

            var validationErrors = _updateProductService.ValidateModelInputs(model.Name, model.Price, model.Description);
            if (validationErrors.Any())
            {
                return BadRequestResponse(string.Join(",", validationErrors)); 
            }
            _updateProductService.Update(product, model.Name, model.Price, model.Description);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            _deleteProductService.Delete(product);
            return Found();
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            return Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take, string name = null, decimal? price = null, string description = null)
        {
            var products = _getProductService.GetProducts(name, price, description)
                                       .Skip(skip).Take(take)
                                       .Select(q => new ProductData(q))
                                       .ToList();
            return Found(products);
        }
    }
}