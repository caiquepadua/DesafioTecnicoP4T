# Desafio Técnico -- People4Tech

Este projeto implementa um sistema simples de gestão de **Produtos** e
**Pedidos**, utilizando:

-   Backend em ASP.NET Core 9
-   Entity Framework Core InMemory
-   Frontend em HTML + Bootstrap + JavaScript puro
-   Swagger UI para testes da API

O sistema possui duas interfaces frontend:

-   `produtos.html` --- CRUD completo de Produtos
-   `pedidos.html` --- Consulta de Pedidos

A criação de pedidos ocorre exclusivamente via Swagger ou via requisição
HTTP.

------------------------------------------------------------------------

## Estrutura do Projeto

    DesafioCaique/
     ??? Controllers/
     ?    ??? ProdutosController.cs
     ?    ??? PedidosController.cs
     ??? Data/
     ?    ??? ApplicationDbContext.cs
     ??? DTOs/
     ?    ??? CriarPedidoDTO.cs
     ?    ??? ItemPedidoDTO.cs
     ??? Models/
     ?    ??? Produto.cs
     ?    ??? Pedido.cs
     ?    ??? ItemPedido.cs
     ??? wwwroot/
     ?    ??? produtos.html
     ?    ??? pedidos.html
     ??? Program.cs

------------------------------------------------------------------------

## Tecnologias Utilizadas

  Tecnologia                Finalidade
  ------------------------- -----------------------
  .NET 9 / ASP.NET Core 9   API REST
  Entity Framework Core     Banco em memória
  Swagger / OpenAPI         Testes da API
  HTML + Bootstrap 5        Interface Web
  JavaScript (Fetch API)    Comunicação com a API

------------------------------------------------------------------------

# Modelos da Aplicação

### Produto

``` csharp
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int QuantidadeEstoque { get; set; }
}
```

### Pedido

``` csharp
public class Pedido
{
    public int Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public List<ItemPedido> Itens { get; set; } = new();
    public decimal ValorTotal { get; set; }
}
```

### ItemPedido

``` csharp
public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal => PrecoUnitario * Quantidade;
}
```

------------------------------------------------------------------------

# Endpoints da API

## Produtos

### `GET /api/Produtos`

Lista todos os produtos.

### `POST /api/Produtos`

Cria um novo produto.

**Exemplo de corpo**

``` json
{
  "nome": "Camiseta",
  "descricao": "Algodão",
  "preco": 59.90,
  "quantidadeEstoque": 20
}
```

### `GET /api/Produtos/{id}`

Retorna um produto específico.

### `PUT /api/Produtos/{id}`

Atualiza um produto existente.

### `DELETE /api/Produtos/{id}`

Remove um produto.

------------------------------------------------------------------------

## Pedidos

### `POST /api/Pedidos`

Cria um novo pedido.

**Exemplo de corpo**

``` json
{
  "nomeCliente": "Caique",
  "itens": [
    {
      "produtoId": 1,
      "quantidade": 2
    }
  ]
}
```

A API valida automaticamente:

-   Se todos os produtos existem
-   Se há estoque suficiente
-   Atualiza o estoque após a compra

### `GET /api/Pedidos`

Retorna todos os pedidos, incluindo itens e produtos.

------------------------------------------------------------------------

# Interface Web

## `produtos.html`

Funcionalidades:

-   Criar produto
-   Listar produtos
-   Editar produto
-   Excluir produto

Todas as operações usam Fetch API para comunicar com a API.

## `pedidos.html`

Funcionalidades:

-   Consultar pedidos criados via Swagger ou pela API
-   Exibição em cartões contendo:
    -   Id
    -   Nome do cliente
    -   Data
    -   Valor total
    -   Tabela com itens (produto, quantidade, preço unitário, subtotal)

------------------------------------------------------------------------

# Como Executar o Projeto

1.  Restaurar dependências:

``` bash
dotnet restore
```

2.  Executar a aplicação:

``` bash
dotnet run
```

3.  URLs principais (exemplo):

  Funcionalidade       URL
  -------------------- --------------------------------------
  Swagger              https://localhost:7113/swagger
  Página de Produtos   https://localhost:7113/produtos.html
  Página de Pedidos    https://localhost:7113/pedidos.html

------------------------------------------------------------------------

# Fluxo do Sistema

1.  **Cadastrar produtos** Feito pela página `produtos.html`.

2.  **Criar pedidos** Feito exclusivamente via:

    -   Swagger, ou
    -   Requisições `POST /api/Pedidos`

    O sistema:

    -   Calcula o total do pedido
    -   Cria os itens do pedido
    -   Atualiza o estoque dos produtos

3.  **Consultar pedidos** Feito em `pedidos.html`.

------------------------------------------------------------------------

