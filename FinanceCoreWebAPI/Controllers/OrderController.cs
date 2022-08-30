using FinanceCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public AppDbContext _context { get; set; }
        public int OrderNumber = 1;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        //Method to get list of all the Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            var OrderList = _context.Orders.ToList();
            return Ok(OrderList);
        }

        //Method to get the Order related to a UserId
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Order>> Get(int id)
        {
            var user = _context.Orders.FirstOrDefault(c => c.UserId == id);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post(Order newOrder)
        {
            var OrderList = _context.Orders.ToList();
            Order lastOrder = OrderList.Last();
            newOrder.OrderId = lastOrder.OrderId+1;
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            //return Ok();
            return CreatedAtAction("Get", new { id = newOrder.UserId }, newOrder);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _context.Orders.FirstOrDefault(c => c.OrderId == id);
            if (data == null)
                return NotFound();
            else
            {
                _context.Orders.Remove(data);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
