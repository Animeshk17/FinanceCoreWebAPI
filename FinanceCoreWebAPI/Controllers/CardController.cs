using FinanceCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinanceCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public CardController(AppDbContext context)
        {
            _context = context;
        }

        //Method to get list of all the Cards
        [HttpGet]
        public ActionResult<IEnumerable<Card>> Get()
        {
            var cardList = _context.Cards.ToList();
            return Ok(cardList);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Card>> Get(int id)
        {
            var cardData = _context.Cards.FirstOrDefault(c => c.UserId == id);
            return Ok(cardData);
        }

        [HttpPost]
        public ActionResult Post(Card newCard)
        {
            _context.Cards.Add(newCard);
            _context.SaveChanges();

            //return Ok();
            return CreatedAtAction("Get", new { id = newCard.UserId }, newCard);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Card modifiedCard)
        {
            var data = _context.Cards.FirstOrDefault(c => c.UserId == id);
            if (data == null)
                return BadRequest();
            else
            {
                var purchaseAmount = modifiedCard.AccountBalance;
                data.AccountBalance = data.AccountBalance - purchaseAmount;
                _context.SaveChanges();
                return Ok();
            }
        }


    }
}
