using Microsoft.AspNetCore.Mvc;
using Net6ShCart.BusinessLayer.Rules.ItemCheckRulesEngine;
using Net6ShCart.DataLayer;
using Net6ShCart.DataLayer.ShoppingCart;
using Net6ShCart.Entities;

namespace Net6ShCart.BusinessLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartContext _context;
        private readonly IShoppingCartRepository _repo;
        private readonly IProductStockRepository _StockRepo;
        private readonly IProductRepository _ProductRepo;

        public ShoppingCartController(ShoppingCartContext context, IShoppingCartRepository repo, IProductStockRepository stockrepo,IProductRepository productRepo)
        {
            _context = context;
            _repo = repo;
            _StockRepo = stockrepo;
            _ProductRepo = productRepo;
        }

        // GET: api/ShoppingCart /Read All Users Carts
        [HttpGet]
        [Route("GetAllCartItems")]
        public async Task<ActionResult<IEnumerable<ShoppingCartEntity>>> GetGetShoppingCartItems()
        {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }
            return await _repo.GetAllItemShoppingCart();
        }

         // GET: api/ShoppingCart /Read Single Users Cart
        [HttpGet]
        [Route("GetUserShoppingCart")]
        public async Task<ActionResult<IEnumerable<ShoppingCartEntity>>>   GetUserShoppingCart(long UserID)
        { 
            if (_context.ShoppingCartEntities == null)
            {
                 return NotFound();
            }
            
            var ShoppingCartUser= await _repo.GetUserShoppingCart(UserID);

             if (ShoppingCartUser == null)
            {
                return NotFound();
            }


        return ShoppingCartUser;
        }
        
        // GET: UserID,ProductID /Read Single Item From a Cart
        [HttpGet]
        [Route("GetASingleItem")]
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
        [HttpPut]
        public async Task<IActionResult> PutShoppingCartEntity(ShoppingCartEntity shoppingCartEntity)
        {
      
            if (_context.ShoppingCartEntities == null)
            {
                return Problem("Entity set 'ShoppingCartContext.GetShoppingCartItems'  is null.");
            }
            bool decision = false;
            StockCalculator stockCalculator = new StockCalculator(_StockRepo,_ProductRepo);
            decision = stockCalculator.CalculateStock(shoppingCartEntity);

            if(decision)
            {
             await _repo.UpdateItemShoppingCart(shoppingCartEntity);
            }
            else
            {
                return Problem("Sorry you can't add that item to your shopping cart.");
            }

            return CreatedAtAction(nameof(GetShoppingCartEntity), new { id = shoppingCartEntity.UserID }, shoppingCartEntity);
        }

        // POST: api/ShoppingCart /Create
        [HttpPost]
        public async Task<ActionResult<ShoppingCartEntity>> PostShoppingCartEntity(ShoppingCartEntity shoppingCartEntity)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return Problem("Entity set 'ShoppingCartContext.GetShoppingCartItems'  is null.");
            }
            bool decision = false;
            StockCalculator stockCalculator = new StockCalculator(_StockRepo,_ProductRepo);
            decision = stockCalculator.CalculateStock(shoppingCartEntity);

            if(decision)
            {
             await _repo.AddItemShoppingCart(shoppingCartEntity);
            }
            else
            {
                return Problem("Sorry you can't add that item to your shopping cart.");
            }

            return CreatedAtAction(nameof(GetShoppingCartEntity), new { id = shoppingCartEntity.UserID }, shoppingCartEntity);
        }

        // DELETE: api/ShoppingCart/5 /Delete
        [HttpDelete]
        [Route("DeleteASingleItem")]
        public async Task<IActionResult> DeleteShoppingCartEntity(ShoppingCartEntity ItemToRemove)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }
            var shoppingCartEntity = _repo.DeleteItemShoppingCart(ItemToRemove);
            if (shoppingCartEntity == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteAllItems")]
        public async Task<IActionResult> DeleteAllShoppingCartEntity(long  UserID)
            {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }
            var shoppingCartEntity = _repo.DeleteAllShoppingCart(UserID);
            if (shoppingCartEntity == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        private bool ShoppingCartEntityExists(long id)
        {
            return (_context.ShoppingCartEntities?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
