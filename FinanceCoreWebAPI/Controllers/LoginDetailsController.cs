using FinanceCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginDetailsController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public LoginDetailsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var data = _context.LoginDetails.ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<UserLoginViewModel>> Get(int id)
        {
            var user = _context.Users.FirstOrDefault(c => c.UserId == id);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post(LoginDetails newLoginDetails)
        {
            _context.LoginDetails.Add(newLoginDetails);
            _context.SaveChanges();

            return Ok();
            //return CreatedAtAction("Get", new { id = newLoginDetails.UserId }, newLoginDetails);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, LoginDetails modified)
        {
            var data = _context.LoginDetails.FirstOrDefault(c => c.UserId == id);
            if (data == null)
                return BadRequest();
            else
            {
                data.UserEmail = modified.UserEmail;
                data.Password = modified.Password;
                data.ConfirmPassword = modified.ConfirmPassword;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _context.LoginDetails.FirstOrDefault(l => l.UserId == id);
            if (data == null)
                return NotFound();
            else
            {
                _context.LoginDetails.Remove(data);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
