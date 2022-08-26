using FinanceCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var UserList = _context.Users.ToList();
            return Ok(UserList);
        }

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
                data.Name = modifiedUser.Name;
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
