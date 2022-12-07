using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skopei.Database;
using Skopei.DTO.Product;
using Skopei.Models;

namespace Skopei.Services
{
    /*
     * ProductService class
     *
     * Contains all the CRUD actions and accesses the database with the ApplicationContext (DbContext).
     * All the actions are asynchronous programmed to improve the responsiveness and performance.
     */
    public class ProductService
    {
        private readonly ApplicationContext _context;
        
        /*
         * Get the ApplicationContext (DbContext) with constructor based dependency injection.
         */
        public ProductService(ApplicationContext context)
        {
            _context = context;
        }
        
        /*
         * Gets all the Products from the database in a list.
         */
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /*
         * Gets a Product by the given id from the database.
         */
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /*
         * Creates a Product in the database with the given Product data.
         * Uses a ProductPostDTO which contains only the necessary data to create a Product.
         * Makes a new Product object with the data and adds it to the Product table.
         * Returns the Product object upon successful creation.
         */
        public async Task<Product> CreateProductAsync(ProductPostDTO product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Quantity = product.Quantity,
                Price = (double) product.Price
            };
            
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return newProduct;
        }

        /*
         * Updates an existing Product with the given & updated data.
         * Uses a ProductPutDTO which contains all only the necessary data to update a Product.
         * Makes a new Product object with the updated data.
         * Sets the DateModified field to the current UTC time.
         * Uses the Update database call with the updated Product object.
         */
        public async Task UpdateProductAsync(ProductPutDTO product)
        {
            Product updatedProduct = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
                DateModified = DateTime.UtcNow,
                DateCreated = product.DateCreated,
                Deleted = product.Deleted
            };

            _context.Update(updatedProduct);
            await _context.SaveChangesAsync();
        }

        /*
         * Deletes a Product from the database.
         */
        public async Task DeleteProductsAsync(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        /*
         * Checks if a certain Product exists by checking if the id is present in the Product table.
         * Returns a boolean.
         */
        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }
    }
}