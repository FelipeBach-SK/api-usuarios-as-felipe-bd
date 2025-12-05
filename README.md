API de Gerenciamento de Usuários

Descrição:

-Esta API foi desenvolvida para realizar o controle completo de usuários dentro de um sistema, permitindo criar, listar, atualizar e remover registros de forma simples e organizada. Ela serve como uma camada intermediária entre a aplicação cliente e o banco de dados, garantindo que todas as operações sejam feitas com segurança, validação e padronização.
-O projeto foi estruturado seguindo boas práticas de desenvolvimento, como Clean Architecture e separação clara de responsabilidades. Isso torna o código mais fácil de manter, testar e expandir no futuro, permitindo que novas regras, funcionalidades ou integrações possam ser adicionadas sem complicação. Mesmo sendo uma API simples, ela foi construída com base em conceitos profissionais que garantem qualidade e escalabilidade.

Tecnologias Utilizadas:

-.NET 8.0
-ASP.NET Core Minimal APIs
-Entity Framework Core 8
-SQLite
-FluentValidation
-AutoMapper
-Swagger / Swashbuckle
-Dependency Injection nativa do .NET
-Repository Pattern
-Service Layer Pattern
-DTO Pattern
-EF Core Migrations
-C# 12
-LINQ
-dotnet CLI
-Git
-GitHub
-OpenAPI

Padrões de Projeto Implementados:

-Repository Pattern
-Service Pattern
-DTO Pattern
-Dependency Injection

Como Executar o Projeto:

-Pré-requisitos: .NET SDK 8.0 ou superior

Passos:

-Clone o repositório: "git clone https://github.com/seu-repositorio.git"
-Aplique as migrations: "dotnet ef database update"
-Execute a aplicação: "dotnet run"

Exemplos de Requisições:
-Criar usuário (POST):
"POST /usuarios
{
  "nome": "Felipe",
  "email": "felipe@email.com",
  "senha": "123456"
}"

-Buscar todos os usuários (GET):
"GET /usuarios"

-Atualizar usuário (PUT):
"PUT /usuarios/1
{
  "nome": "Felipe Silva",
  "email": "felipe.silva@email.com"
}"

-Deletar usuário (DELETE):
"DELETE /usuarios/1"

Estrutura do Projeto:
-Domain → Entidades, interfaces e regras de negócio
-Infrastructure → EF Core, contexto do banco e repositórios
-Application → Serviços, DTOs, validações
-API → Endpoints, configuração da aplicação e injeção de dependência

Autor:
Nome: Felipe Bach
RA: 2025003602 (Não sei se é isso)
Curso: ADS