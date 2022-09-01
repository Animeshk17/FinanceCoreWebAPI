using FinanceCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        //Method to get list of all the Orders
        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get()
        {
            var OrderList = _context.Transactions.ToList();
            return Ok(OrderList);
        }

        //Method to get the Order related to a UserId
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Transaction>> Get(int id)
        {
            var user = _context.Transactions.FirstOrDefault(c => c.UserId == id);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post(Transaction newTransaction)
        {
            var TransactionList = _context.Transactions.ToList();
            Transaction lastTransaction = TransactionList.Last();
            newTransaction.TransactionId = lastTransaction.TransactionId + 1;
            _context.Transactions.Add(newTransaction);
            _context.SaveChanges();

            //return Ok();
            return CreatedAtAction("Get", new { id = newTransaction.TransactionId }, newTransaction);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _context.Transactions.FirstOrDefault(c => c.TransactionId == id);
            if (data == null)
                return NotFound();
            else
            {
                _context.Transactions.Remove(data);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
