using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skopei.DTO;
using Skopei.Models;
using Skopei.Services;

namespace Skopei.Controllers
{
    /*
     * UserController class
     *
     * Contains the User endpoints and calls the UserService class for further actions.
     * All the endpoints are asynchronous programmed to improve the responsiveness and performance.
     * Base route for this controller is set to: api/user
     */
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        /*
         * Get the UserService with constructor based dependency injection.
         */
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        /*
         * Endpoint to get all the Users in a list.
         * Returns empty list if there are no Users.
         * Route for this endpoint is: api/user
         */
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _userService.GetUsersAsync();
        }

        /*
         * Endpoint to get a single User by a given id.
         * Contains User existence check by verifying that the retrieved User is not null.
         * Route for this endpoint is: api/user/{id}
         */
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        /*
         * Endpoint to create an User with the given User data.
         * Uses an UserPostDTO which contains only the necessary data to create an User.
         * Returns the created User object by calling the GetUser method.
         * Route for this endpoint is: api/user
         */
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserPostDTO userPostDTO)
        {
            var createdUser = await _userService.CreateUserAsync(userPostDTO);
            return CreatedAtAction("GetUser", new { id = createdUser.Id }, createdUser);
        }

        /*
         * Endpoint to update an User with a given id and updated User data.
         * Uses an UserPutDTO which contains all only the necessary data to update an User.
         * Checks if the given id matches the id in the given User data.
         * Catches the DbUpdateConcurrencyException if the saving the data to the database fails.
         * Contains User existence check by verifying that the retrieved User is not null when it fails.
         * Returns 204 empty body success as response.
         * Route for this endpoint is: api/user/{id}
         */
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutUser(int id, UserPutDTO userPutDTO)
        {
            if (id != userPutDTO.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _userService.UpdateUserAsync(userPutDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _userService.UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            
            return NoContent();
        }
        
        /*
         * Endpoint to delete an User by a given id.
         * Contains User existence check by verifying that the retrieved User is not null.
         * Returns 204 empty body success as response.
         * Route for this endpoint is: api/user/{id}
         */
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(user);

            return NoContent();
        }
    }
}