using FinanceCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCoreWebAPI.Controllers
{
    /// <summary>
    /// This controller is used to Perform CRUD options on the User Table
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        //Method to get list of all the users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var UserList = _context.Users.ToList();
            return Ok(UserList);
        }

        //Method to get the user based on the id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<User>> Get(int id) {
            var user = _context.Users.FirstOrDefault(c => c.UserId == id);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //return Ok();
            return CreatedAtAction("Get", new { id = newUser.UserId }, newUser);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, User modifiedUser)
        {
            var data = _context.Users.FirstOrDefault(c => c.UserId == id);
            if (data == null)
                return BadRequest();
            else
            {
                data.Is_verified = modifiedUser.Is_verified;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var data = _context.Users.FirstOrDefault(c => c.UserId == id);
            if (data == null)
                return NotFound();
            else
            {
                _context.Users.Remove(data);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
