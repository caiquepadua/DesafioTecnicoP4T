using DesafioCaique.Data;
using DesafioCaique.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioCaique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound();

            return produto;
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
            }
            catch (DbUpdateException ex)
            {
                if (_context.Produtos.Any(p => p.Id == produto.Id))
                {
                    return Conflict($"Já existe um produto com o Id {produto.Id}.");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar o produto.");
            }
        }

        // PUT: api/produtos/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
                return BadRequest("The product ID in the URL is different from the request body.");

            var existe = await _context.Produtos.AnyAsync(p => p.Id == id);
            if (!existe)
                return NotFound();

            _context.Entry(produto).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }

        // DELETE: api/produtos/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}