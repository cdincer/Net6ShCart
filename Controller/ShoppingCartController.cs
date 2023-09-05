using Microsoft.AspNetCore.Mvc;
using Net6ShCart.Controller.Rules.ItemCheckRulesEngine;
using Net6ShCart.Database;
using Net6ShCart.Database.Repository;
using Net6ShCart.Entities;

namespace Net6ShCart.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartContext _context;
        private readonly IShoppingCartRepository _repo;
        private readonly IProductStockRepository _StockRepo;
        private readonly IProductRepository _ProductRepo;

        public ShoppingCartController(ShoppingCartContext context, IShoppingCartRepository repo, IProductStockRepository stockrepo, IProductRepository productRepo)
        {
            _context = context;
            _repo = repo;
            _StockRepo = stockrepo;
            _ProductRepo = productRepo;
        }


        // GET: UserID,ProductID /Read Single Item From a Cart
        [HttpGet]
        [Route("GetSingle")]
        public async Task<ActionResult<ShoppingCartEntity>> GetSingle(long UserID, long ProductID)
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

        // GET: api/ShoppingCart /Read Single Users Cart
        [HttpGet]
        [Route("GetCart")]
        public async Task<ActionResult<IEnumerable<ShoppingCartEntity>>> GetCart(long UserID)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }

            var ShoppingCartUser = await _repo.GetUserShoppingCart(UserID);

            if (ShoppingCartUser == null)
            {
                return NotFound();
            }


            return ShoppingCartUser;
        }


        // GET: api/ShoppingCart /Read All Users Carts
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<ShoppingCartEntity>>> GetAll()
        {
            if (_context.ShoppingCartEntities == null)
            {
                return NotFound();
            }
            return await _repo.GetAllItemShoppingCart();
        }


        // PUT: api/ShoppingCart/5 
        [HttpPut]
        public async Task<IActionResult> PutEntity(ShoppingCartEntity shoppingCartEntity)
        {

            if (_context.ShoppingCartEntities == null)
            {
                return Problem("Entity set 'ShoppingCartContext.GetShoppingCartItems'  is null.");
            }
            bool decision = false;
            StockCalculator stockCalculator = new StockCalculator(_StockRepo, _ProductRepo);
            decision = stockCalculator.CalculateStock(shoppingCartEntity);

            if (decision)
            {
                await _repo.UpdateItemShoppingCart(shoppingCartEntity);
            }
            else
            {
                return Problem("Sorry you can't add that item to your shopping cart.");
            }

            return CreatedAtAction(nameof(GetSingle), new { id = shoppingCartEntity.UserID }, shoppingCartEntity);
        }

        // POST: api/ShoppingCart
        [HttpPost]
        public async Task<ActionResult<ShoppingCartEntity>> PostEntity(ShoppingCartEntity shoppingCartEntity)
        {
            if (_context.ShoppingCartEntities == null)
            {
                return Problem("Entity set 'ShoppingCartContext.GetShoppingCartItems'  is null.");
            }
            bool decision = false;
            StockCalculator stockCalculator = new StockCalculator(_StockRepo, _ProductRepo);
            decision = stockCalculator.CalculateStock(shoppingCartEntity);

            if (decision)
            {
                await _repo.AddItemShoppingCart(shoppingCartEntity);
            }
            else
            {
                return Problem("Sorry you can't add that item to your shopping cart.");
            }

            return CreatedAtAction(nameof(GetSingle), new { id = shoppingCartEntity.UserID }, shoppingCartEntity);
        }

        // DELETE: api/ShoppingCart/5
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteEntity(ShoppingCartEntity ItemToRemove)
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
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteAll")]
        public async Task<IActionResult> DeleteAll(long UserID)
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
            return Ok();
        }
        private bool ShoppingCartEntityExists(long id)
        {
            return (_context.ShoppingCartEntities?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
