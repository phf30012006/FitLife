# FitLife - Plataforma de Treinos e Hábitos Saudáveis

## Visão Geral
FitLife é uma plataforma para gerenciar treinos, alimentação e hábitos saudáveis. Os usuários podem registrar seu progresso e receber sugestões personalizadas.

## Arquitetura

Este projeto utiliza **Clean Architecture** com as seguintes camadas:

### 1. Domain Layer (`FitLife.Domain`)
- **Responsabilidade**: Entidades de negócio e interfaces de repositório
- **Características**:
  - Sem dependências externas
  - Classes de polimorfismo para tipos de treino (Cardio, Musculação)
  - Entidades: `Usuario`, `Treino`, `RegistroAlimentacao`, `Habito`

### 2. Application Layer (`FitLife.Application`)
- **Responsabilidade**: Casos de uso e lógica de aplicação
- **Características**:
  - DTOs para transferência de dados
  - Serviços de aplicação
  - Mapeamento manual (sem AutoMapper)
  - Sistema de Recomendações (IA simples)
  - Depende apenas do Domain

### 3. Infrastructure Layer (`FitLife.Infrastructure`)
- **Responsabilidade**: Acesso a dados e implementações técnicas
- **Características**:
  - Implementação de repositórios
  - DbContext do Entity Framework Core
  - SQL Server como banco de dados
  - Configuração de relacionamentos e índices

### 4. API Layer (`FitLife.API`)
- **Responsabilidade**: Endpoints REST e configuração da aplicação
- **Características**:
  - Controllers REST
  - Injeção de dependências
  - Swagger para documentação
  - CORS habilitado

## Conceitos Implementados

### Classes e Polimorfismo
- Classe abstrata `Treino` com implementações concretas:
  - `TreinoCardio`: treinos cardiovasculares
  - `TreinoMusculacao`: treinos de força
- Método abstrato `CalcularCalorias()` implementado por cada tipo

### LINQ para Histórico e Ranking
- Consultas LINQ para filtrar treinos por usuário
- Ranking de treinos por calorias
- Filtros por período para alimentação

### API REST com CRUD
- **Usuários**: GET, POST, PUT, DELETE
- **Treinos**: GET, POST (Cardio/Musculação), DELETE, Ranking
- **Alimentação**: GET, POST, DELETE, filtro por período
- **Hábitos**: GET, POST, DELETE
- **Recomendações**: GET relatório com sugestões personalizadas (IA simples)


## Como Executar

### Pré-requisitos
- .NET 8.0 SDK
- SQL Server ou SQL Server LocalDB

### Passos

1. **Restaurar dependências**:
```powershell
cd "c:\Users\paulo\Plataforma de desnvolvimento\FitLife"
dotnet restore
```

2. **Criar o banco de dados**:
```powershell
cd src\FitLife.API
dotnet ef migrations add InitialCreate --project ..\FitLife.Infrastructure\FitLife.Infrastructure.csproj
dotnet ef database update
```

3. **Executar a aplicação**:
```powershell
dotnet run --project src\FitLife.API\FitLife.API.csproj
```

4. **Acessar o Swagger**:
- URL: `https://localhost:*porta*/swagger`

## Endpoints da API

### Usuários
- `GET /api/usuarios` - Lista todos os usuários
- `GET /api/usuarios/{id}` - Busca usuário por ID
- `GET /api/usuarios/email/{email}` - Busca usuário por email
- `POST /api/usuarios` - Cria novo usuário
- `PUT /api/usuarios/{id}` - Atualiza usuário
- `DELETE /api/usuarios/{id}` - Remove usuário

### Treinos
- `GET /api/treinos` - Lista todos os treinos
- `GET /api/treinos/{id}` - Busca treino por ID
- `GET /api/treinos/usuario/{usuarioId}` - Lista treinos do usuário
- `GET /api/treinos/ranking?top=10` - Ranking por calorias
- `POST /api/treinos/cardio` - Cria treino cardio
- `POST /api/treinos/musculacao` - Cria treino musculação
- `DELETE /api/treinos/{id}` - Remove treino

### Alimentação
- `GET /api/alimentacao/{id}` - Busca registro por ID
- `GET /api/alimentacao/usuario/{usuarioId}` - Lista registros do usuário
- `GET /api/alimentacao/usuario/{usuarioId}/periodo?dataInicio=...&dataFim=...` - Filtro por período
- `POST /api/alimentacao` - Cria registro
- `DELETE /api/alimentacao/{id}` - Remove registro

### Hábitos
- `GET /api/habitos/{id}` - Busca hábito por ID
- `GET /api/habitos/usuario/{usuarioId}` - Lista hábitos do usuário
- `POST /api/habitos` - Cria hábito
- `DELETE /api/habitos/{id}` - Remove hábito

### Recomendações
- `GET /api/recomendacoes/usuario/{usuarioId}/relatorio` - Relatório completo com estatísticas e sugestões
- `GET /api/recomendacoes/usuario/{usuarioId}/sugestoes` - Apenas sugestões personalizadas


## Tecnologias Utilizadas

- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM
- **SQL Server** - Banco de dados
- **Swagger** - Documentação da API

## Documentação Adicional

- **Pesquisa_Mercado.md** - Análise de concorrentes e oportunidades
- **Diagrama_UML.md** - Fluxos principais do sistema

