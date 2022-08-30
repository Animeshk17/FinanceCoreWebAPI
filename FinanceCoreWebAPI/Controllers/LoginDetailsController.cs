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

        //[HttpGet("{id}")]
        //public ActionResult<IEnumerable<UserLoginViewModel>> Get(int id)
        //{
        //    var userLoginData = _context.LoginDetails.FirstOrDefault(c => c.UserId == id);
        //    return Ok(userLoginData);
        //}

        [HttpGet("{email}")]
        public ActionResult<IEnumerable<LoginDetails>> Get(string email)
        {
            var userLoginData = _context.LoginDetails.FirstOrDefault(c => c.UserEmail == email);
            return Ok(userLoginData);
        }

        /// <summary>
        /// The API powering the login page.
        /// </summary>
        /// <param name="loginDetails"> Logindetails object </param>
        /// <returns> ActionResult </returns>
        [HttpPost("{email}")]
        public ActionResult Post(LoginDetails loginDetails)
        {
            string userEnteredEmail = loginDetails.UserEmail;
            string userEnteredPassword = loginDetails.Password;
            var data = _context.LoginDetails.FirstOrDefault(c => c.UserEmail == userEnteredEmail);
            if(data == null)
            {
                return NotFound();
            }
            else if(userEnteredPassword == data.Password)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

            //return CreatedAtAction("Get", new { id = newLoginDetails.UserId }, newLoginDetails);
        }
        /// <summary>
        /// The API powering the Forgot Password Option
        /// </summary>
        /// <param name="email"> the email associated with the user </param>
        /// <param name="newLoginDetails"> new login details </param>
        /// <returns></returns>
        [HttpPut("{email}")]
        public ActionResult Put(LoginDetails newLoginDetails)
        {
            var email = newLoginDetails.UserEmail;
            var data = _context.LoginDetails.FirstOrDefault(c => c.UserEmail == email);
            if (data == null)
                return BadRequest();
            else
            {
                data.Password = newLoginDetails.Password;
                data.ConfirmPassword = newLoginDetails.ConfirmPassword;
                //TODO: Add try-catch block here
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
