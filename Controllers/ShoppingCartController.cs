using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net6ShCart.DAL.Layer;
using Net6ShCart.DAL.Layer.ShoppingCart;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartContext _context;
        private readonly IShoppingCartRepository _repo;
        private readonly IProductStockRepository _StockRepo;

        public ShoppingCartController(ShoppingCartContext context, IShoppingCartRepository repo, IProductStockRepository stockrepo)
        {
            _context = context;
            _repo = repo;
            _StockRepo = stockrepo;
        }

        // GET: api/ShoppingCart /Read All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartEntity>>> GetGetShoppingCartItems()
        {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }
            return await _repo.GetAllItemShoppingCart();
        }

        // GET: UserID,ProductID /Read Single
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartEntity>> GetShoppingCartEntity(long UserID, long ProductID)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }
            var shoppingCartEntity = await _repo.GetItemShoppingCart(UserID, ProductID);

            if (shoppingCartEntity == null)
            {
                return NotFound();
            }

            return shoppingCartEntity;
        }

        // PUT: api/ShoppingCart/5 /Update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartEntity(long id, ShoppingCartEntity shoppingCartEntity)
        {
            if (id != shoppingCartEntity.UserID)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShoppingCart /Create
        [HttpPost]
        public async Task<ActionResult<ShoppingCartEntity>> PostShoppingCartEntity(ShoppingCartEntity shoppingCartEntity)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return Problem("Entity set 'ShoppingCartContext.GetShoppingCartItems'  is null.");
            }
            /*
            Rule Engine Spot Check Before Adding.
          */
            await _repo.AddItemShoppingCart(shoppingCartEntity);

            return CreatedAtAction(nameof(GetShoppingCartEntity), new { id = shoppingCartEntity.UserID }, shoppingCartEntity);
        }

        // DELETE: api/ShoppingCart/5 /Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartEntity(long id)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }
            var shoppingCartEntity = await _context.ShoppingCartEntities.FindAsync(id);
            if (shoppingCartEntity == null)
            {
                return NotFound();
            }

            _context.ShoppingCartEntities.Remove(shoppingCartEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartEntityExists(long id)
        {
            return (_context.ShoppingCartEntities?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
