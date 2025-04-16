using Microsoft.AspNetCore.Mvc;
using UserApi.Models;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private static int nextId = 1;

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            newUser.Id = nextId++;
            users.Add(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("User not found");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("User not found");

            users.Remove(user);
            return NoContent();
        }
    }
}
