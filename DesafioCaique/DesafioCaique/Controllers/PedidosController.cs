using DesafioCaique.Data;
using DesafioCaique.DTOs;
using DesafioCaique.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioCaique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> CriarPedido(CriarPedidoDTO dto)
        {
            // Load order products
            var produtosIds = dto.Itens.Select(i => i.ProdutoId).ToList();

            var produtos = await _context.Produtos
                .Where(p => produtosIds.Contains(p.Id))
                .ToListAsync();

            // Validate that all products exist
            if (produtos.Count != produtosIds.Count)
                return BadRequest("One or more products not exists.");

            // Validate stock
            foreach (var item in dto.Itens)
            {
                var produto = produtos.First(p => p.Id == item.ProdutoId);

                if (produto.QuantidadeEstoque < item.Quantidade)
                {
                    return BadRequest($"Insufficient stock for the product '{produto.Nome}'.");
                }
            }

            // Create the order
            var pedido = new Pedido
            {
                NomeCliente = dto.NomeCliente,
                Itens = new List<ItemPedido>()
            };

            decimal total = 0;

            foreach (var item in dto.Itens)
            {
                var produto = produtos.First(p => p.Id == item.ProdutoId);

                var itemPedido = new ItemPedido
                {
                    ProdutoId = produto.Id,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produto.Preco
                };

                total += produto.Preco * item.Quantidade;

                pedido.Itens.Add(itemPedido);

                // Download stock
                produto.QuantidadeEstoque -= item.Quantidade;
            }

            // Save
            pedido.ValorTotal = total;

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return Ok(pedido);
        }

        // GET: api/pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .ToListAsync();
        }
    }
}