using FinanceCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        public AppDbContext _context { get; }
        public DashboardController(AppDbContext context)
        {
            _context = context;//public property, will be responsible to do crud operations
        }

        /// <summary>
        /// Returns card details of a user's Card by userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            Card cardDetail = _context.Cards.FirstOrDefault(e => e.UserId == id);
            return Ok(cardDetail);
        }

        /// <summary>
        /// Returns recent product purchased by user via EMI using userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("productsPurchasedByUser")]
        public IActionResult ProductsPurchasedByUser(int id)
        {
            List<Order> OrderList = _context.Orders.Where(order => order.UserId == id).ToList();

            List<int> ProductIdList = new List<int>();
            foreach (Order o in OrderList)
            {
                ProductIdList.Add(o.ProductId);
            }

            var recentOrderDate = _context.Orders
                .Where(o => o.UserId == id)
                .Max(o => o.OrderDate);

            Order recentOrder = _context.Orders
                //.Where(o => o.isActivated == true)
                .FirstOrDefault(o => o.OrderDate == recentOrderDate && o.UserId == id);

            Product recentOrderProduct = _context.Products.FirstOrDefault(p => p.ProductId == recentOrder.ProductId);

            return Ok(recentOrderProduct);

        }

        /// <summary>
        /// Returns total amount yet paid by user of product which is recently/last ordered
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("getAmountPaidForProduct/")]
        public IActionResult GetAmountPaid(int userId, int productId)
        {
            decimal amountPaid = _context.Transactions
                .Where(t => t.UserId == userId && t.ProductId == productId)
                .Sum(t => t.TransactionAmount);

            return Ok(amountPaid);
        }

        /// <summary>
        /// Returns recent(last 5) transactions made by user by userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getRecentTransactions/")]
        public IActionResult GetRecentTransactions(int id)
        {
            var recentOrderDate = _context.Orders
                .Where(o => o.UserId == id)
                .Max(o => o.OrderDate);

            Order recentOrder = _context.Orders
                .FirstOrDefault(o => o.OrderDate == recentOrderDate && o.UserId == id);

            List<Transaction> recentTransactions = _context.Transactions
                .OrderByDescending(t => t.TransactionDate)
                .Take(5)
                .Where(t => t.UserId == id && t.OrderId == recentOrder.OrderId)
                .ToList();

            return Ok(recentTransactions);
        }
    }
}
