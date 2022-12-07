using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skopei.Database;
using Skopei.DTO;
using Skopei.Models;

namespace Skopei.Services
{
    /*
     * UserService class
     *
     * Contains all the CRUD actions and accesses the database with the ApplicationContext (DbContext).
     * All the actions are asynchronous programmed to improve the responsiveness and performance.
     */
    public class UserService
    {
        private readonly ApplicationContext _context;
        
        /*
         * Get the ApplicationContext (DbContext) with constructor based dependency injection.
         */
        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        
        /*
         * Gets all the Users from the database in a list.
         */
        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        
        /*
         * Gets an User by the given id from the database.
         */
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        
        /*
         * Creates an User in the database with the given User data.
         * Uses an UserPostDTO which contains only the necessary data to create an User.
         * Makes a new User object with the data and adds it to the Users table.
         * Returns the User object upon successful creation.
         */
        public async Task<User> CreateUserAsync(UserPostDTO user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email
            };
            
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        /*
         * Updates an existing User with the given & updated data.
         * Uses an UserPutDTO which contains all only the necessary data to update an User.
         * Makes a new User object with the updated data.
         * Sets the DateModified field to the current UTC time.
         * Uses the Update database call with the updated User object.
         */
        public async Task UpdateUserAsync(UserPutDTO user)
        {
            User updatedUser = new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateModified = DateTime.UtcNow,
                DateCreated = user.DateCreated,
                Deleted = user.Deleted
            };

            _context.Update(updatedUser);
            await _context.SaveChangesAsync();
        }
        
        /*
         * Deletes an User from the database.
         */
        public async Task DeleteUserAsync(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        /*
         * Checks if a certain User exists by checking if the id is present in the User table.
         * Returns a boolean.
         */
        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }
    }
}