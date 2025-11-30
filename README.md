# API de Gerenciamento de Usuários

Projeto base para a avaliação - scaffold entregue automaticamente.

## Como executar

1. Instale .NET 8 SDK
2. Entre na pasta `APIUsuarios`
3. Execute:
   - `dotnet restore`
   - `dotnet ef migrations add InitialCreate` (se quiser gerar migrations)
   - `dotnet ef database update`
   - `dotnet run`

## Endpoints principais

- `GET /usuarios`
- `GET /usuarios/{id}`
- `POST /usuarios`
- `PUT /usuarios/{id}`
- `DELETE /usuarios/{id}` (soft delete)

## Observações

- Emails são normalizados para lowercase antes de salvar.
- Delete é soft: apenas marca `Ativo = false`.
- Validações com FluentValidation.
