using DesafioCaique.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioCaique.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; } = default!;
        public DbSet<Pedido> Pedidos { get; set; } = default!;
        public DbSet<ItemPedido> ItensPedido { get; set; } = default!;
    }
}
