using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using los_api.Models;

namespace los_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : Controller
    {
        private readonly AppDbContext _context;

        public StocksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStockModel>>> GetStocks()
        {
            var _stock = await _context.Stocks.ToListAsync();
            var _product = await _context.Products.ToListAsync();
            List<ProductStockModel> ps = (from s in _stock
                                          join p in _product
                                          on s.ProductId equals p.Id
                                          select new ProductStockModel
                                          {
                                              Name = p.Name,
                                              Amount = s.Amount,
                                              Id = p.Id,
                                              ImageUrl = p.ImageUrl,
                                              Price = p.Price,
                                              StockId = s.Id
                                          }
                                    ).ToList();
            return ps;
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockModel>> GetStockModel(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return stockModel;
        }

        // GET: api/Stocks/5
        [HttpGet("[controller]/ProductInStock/{productId}")]
        public async Task<ActionResult<ProductStockModel>> GetStockProductByID(int productId)
        {
            var _stock = await _context.Stocks.ToListAsync();
            var _product = await _context.Products.ToListAsync();
            ProductStockModel ps = (from s in _stock
                                    join p in _product
                                    on s.ProductId equals p.Id
                                    where s.ProductId == productId
                                    select new ProductStockModel
                                    {
                                        Name = p.Name,
                                        Amount = s.Amount,
                                        Id = p.Id,
                                        ImageUrl = p.ImageUrl,
                                        Price = p.Price,
                                        StockId = s.Id
                                    }
                                        ).FirstOrDefault();

            return ps;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockModel(int id, StockModel stockModel)
        {
            if (id != stockModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockModelExists(id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockModel>> PostStockModel(StockModel stockModel)
        {
            _context.Stocks.Add(stockModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockModel", new { id = stockModel.Id }, stockModel);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockModel(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockModelExists(int id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }
    }
}
