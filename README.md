# ecommerce-api (.NET 9 + Dapper)

API REST para gerenciamento de produtos do módulo administrativo de um e-commerce.  
Este projeto foi desenvolvido como parte de um desafio técnico com foco em organização, clean code e integração com frontend Angular.

---

## 📌 Tecnologias utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [Dapper](https://github.com/DapperLib/Dapper)
- [SQLite In-Memory](https://www.sqlite.org/inmemorydb.html)
- Swagger (Swashbuckle)

---

## ⚙️ Como executar

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

### Passos para rodar localmente

```bash
cd ecommerce-api-net8
dotnet run
```

A API será iniciada em:

```
http://localhost:8080
```

A documentação Swagger estará disponível em:

```
http://localhost:8080/swagger
```

---

## 🔧 Funcionalidades implementadas

### Produtos

- `GET /produtos` — Listagem paginada de produtos
- `POST /produtos` — Criação de novo produto
- `PUT /produtos/{id}` — Atualização de produto
- `DELETE /produtos/{id}` — Exclusão lógica de produto

### Departamentos

- `GET /departamentos` — Lista fixa de departamentos

---

## 🗃️ Estrutura da Tabela `Produtos`

| Campo         | Tipo     | Descrição                           |
|---------------|----------|-------------------------------------|
| `Id`          | string   | UUID gerado automaticamente         |
| `Code`        | string   | Código identificador do produto     |
| `Description` | string   | Descrição do produto                |
| `Department`  | string   | Código do departamento              |
| `Price`       | double   | Preço do produto                    |
| `Status`      | boolean  | Ativo ou Inativo                    |
| `Deletado`    | int      | Exclusão lógica (1 = deletado)      |

---

## 🏁 Observações

- A base de dados é criada automaticamente na memória ao iniciar a aplicação.
- Não utiliza Entity Framework; todas as queries são SQL explícito com Dapper.
- Código organizado por camadas: Controllers, Models, Repositories, Data.
- Middleware global para tratamento de erros com padrão `ErrorDetails`.
- **Paginação customizada:** Inicialmente, o desafio foi liberado para implementação em Java, mas posteriormente foi solicitado também em .NET. Por isso, a lógica de paginação foi implementada manualmente no endpoint de produtos, garantindo que o frontend Angular consuma ambas as APIs de forma idêntica, sem necessidade de ajustes.
- [Repositório Java disponível aqui](https://github.com/lelicerre/ecommerce-api)---

## 📁 Organização do projeto

```
ecommerce-api-net8/
├── Controllers/
│   ├── ProdutosController.cs
│   └── DepartamentosController.cs
├── Models/
│   ├── Produto.cs
│   ├── Departamento.cs
│   ├── ErrorDetails.cs
│   └── ValidationErrorDetails.cs
├── Repositories/
│   └── ProdutoRepository.cs
├── Data/
│   ├── DbConnectionProvider.cs
│   └── DbInitializer.cs
├── Middleware/
│   └── ExceptionMiddleware.cs
├── Program.cs
```

---

## ✅ Testado com

- Postman (requisições manuais)
- Angular frontend acoplado (ecommerce-front)
