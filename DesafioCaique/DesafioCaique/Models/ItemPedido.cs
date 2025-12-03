using DesafioCaique.Models;
using System.Text.Json.Serialization;

namespace DesafioCaique.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        [JsonIgnore] // Use to avoid circular reference during serialization
        public Pedido Pedido { get; set; } = default!;

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = default!;

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}