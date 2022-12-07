using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skopei.DTO.Product;
using Skopei.Models;
using Skopei.Services;

namespace Skopei.Controllers
{
    /*
     * ProductController class
     *
     * Contains the Product endpoints and calls the ProductService class for further actions.
     * All the endpoints are asynchronous programmed to improve the responsiveness and performance.
     * Base route for this controller is set to: api/product
     */
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        /*
         * Get the ProductService with constructor based dependency injection.
         */
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        
        /*
         * Endpoint to get all the Products in a list.
         * Returns empty list if there are no Products.
         * Route for this endpoint is: api/product
         */
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _productService.GetProductsAsync();
        }

        /*
         * Endpoint to get a single Product by a given id.
         * Contains Product existence check by verifying that the retrieved Product is not null.
         * Route for this endpoint is: api/product/{id}
         */
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /*
         * Endpoint to create a Product with the given Product data.
         * Uses a ProductPostDTO which contains only the necessary data to create a Product.
         * Returns the created Product object by calling the GetProduct method.
         * Route for this endpoint is: api/product
         */
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductPostDTO productPostDTO)
        {
            var createdProduct = await _productService.CreateProductAsync(productPostDTO);
            return CreatedAtAction("GetProduct", new { id = createdProduct.Id }, createdProduct);
        }

        /*
         * Endpoint to update a Product with a given id and updated Product data.
         * Uses a ProductPutDTO which contains all only the necessary data to update a Product.
         * Checks if the given id matches the id in the given Product data.
         * Catches the DbUpdateConcurrencyException if the saving the data to the database fails.
         * Contains Product existence check by verifying that the retrieved Product is not null when it fails.
         * Returns 204 empty body success as response.
         * Route for this endpoint is: api/product/{id}
         */
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutProduct(int id, ProductPutDTO productPutDTO)
        {
            if (id != productPutDTO.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _productService.UpdateProductAsync(productPutDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _productService.ProductExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }
        
        /*
         * Endpoint to delete a Product by a given id.
         * Contains Product existence check by verifying that the retrieved Product is not null.
         * Returns 204 empty body success as response.
         * Route for this endpoint is: api/product/{id}
         */
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductsAsync(product);

            return NoContent();
        }
    }
}