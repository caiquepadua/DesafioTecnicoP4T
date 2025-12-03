namespace DesafioCaique.DTOs
{
    public class CriarPedidoDTO
    {
        public string NomeCliente { get; set; } = string.Empty;
        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }
}