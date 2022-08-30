using FinanceCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FinanceCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginViewModelController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public UserLoginViewModelController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Used to get the complete list of Registered Users 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var data = _context.UsersLogin.FromSqlInterpolated($"dbo.SP_CreateUserLoginDetailsView")
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Used to get the registered user depending upon the userId provided
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<UserLoginViewModel>> Get(int id)
        {
            var user = _context.Users.FirstOrDefault(c => c.UserId == id);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post(UserLoginViewModel userloginviewmodelobject)
        {
            var newuser = new User();
            newuser.Name = userloginviewmodelobject.Name;
            newuser.Phone = userloginviewmodelobject.Phone;
            newuser.Address = userloginviewmodelobject.Address;
            newuser.Account_number = userloginviewmodelobject.Account_number;
            newuser.Ifsc_code = userloginviewmodelobject.Ifsc_code;
            newuser.Is_verified = false;

            _context.Users.Add(newuser);
            _context.SaveChanges();

            var newusercreated = _context.Users.FirstOrDefault(
                us => us.Account_number == newuser.Account_number);

            var newuserlogindetail = new LoginDetails();
            newuserlogindetail.UserId = newusercreated.UserId;
            newuserlogindetail.UserEmail = userloginviewmodelobject.UserEmail;
            newuserlogindetail.Password = userloginviewmodelobject.Password;
            newuserlogindetail.ConfirmPassword = userloginviewmodelobject.ConfirmPassword;

            newuser.Account_number = userloginviewmodelobject.Account_number;
            _context.LoginDetails.Add(newuserlogindetail);
            _context.SaveChanges();
            return Ok();
            //return createdataction("get", new { id = newuserlogin.userid }, newuserlogin);
        }

    }
}
