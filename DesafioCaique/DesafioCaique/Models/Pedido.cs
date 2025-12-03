namespace DesafioCaique.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        public decimal ValorTotal { get; set; }
    }
}