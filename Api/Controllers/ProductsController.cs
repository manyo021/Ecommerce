using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {

        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;


        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            //GET: api/Products gets all products
            return await _context.Products.ToListAsync();

        }

        // GET: api/Products/5 specifies ID of product we want to retrieve is id 5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

    }
}