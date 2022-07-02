using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net6ShCart.DAL.Layer;
using Net6ShCart.Entity.Layer.DAL.Entities;

namespace Net6ShCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartContext _context;

        public ShoppingCartController(ShoppingCartContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartEntity>>> GetGetShoppingCartItems()
        {
          if (_context.GetShoppingCartItems == null)
          {
              return NotFound();
          }
            return await _context.GetShoppingCartItems.ToListAsync();
        }

        // GET: api/ShoppingCart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartEntity>> GetShoppingCartEntity(long id)
        {
          if (_context.GetShoppingCartItems == null)
          {
              return NotFound();
          }
            var shoppingCartEntity = await _context.GetShoppingCartItems.FindAsync(id);

            if (shoppingCartEntity == null)
            {
                return NotFound();
            }

            return shoppingCartEntity;
        }

        // PUT: api/ShoppingCart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/ShoppingCart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingCartEntity>> PostShoppingCartEntity(ShoppingCartEntity shoppingCartEntity)
        {
          if (_context.GetShoppingCartItems == null)
          {
              return Problem("Entity set 'ShoppingCartContext.GetShoppingCartItems'  is null.");
          }
            _context.GetShoppingCartItems.Add(shoppingCartEntity);
            await _context.SaveChangesAsync();

          return CreatedAtAction(nameof(GetShoppingCartEntity), new { id = shoppingCartEntity.UserID }, shoppingCartEntity);
        }

        // DELETE: api/ShoppingCart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartEntity(long id)
        {
            if (_context.GetShoppingCartItems == null)
            {
                return NotFound();
            }
            var shoppingCartEntity = await _context.GetShoppingCartItems.FindAsync(id);
            if (shoppingCartEntity == null)
            {
                return NotFound();
            }

            _context.GetShoppingCartItems.Remove(shoppingCartEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartEntityExists(long id)
        {
            return (_context.GetShoppingCartItems?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
