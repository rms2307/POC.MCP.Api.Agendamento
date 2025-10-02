# POC.MCP.Api.Agendamento

## Visão Geral

Este repositório contém uma solução .NET 8 composta por dois módulos principais:

- **POC.MCP.Api.Agendamento**: API REST para gerenciamento de tarefas (CRUD).
- **POC.MCP.Server.Agendamento**: MCP para consumir a API de agendamento de tarefas.

---

## Módulos

### 1. POC.MCP.Api.Agendamento

- **Descrição**: API minimalista para cadastro, consulta, atualização e remoção de tarefas.
- **Principais arquivos**:
  - `Program.cs`: Configuração dos endpoints REST (GET, POST, PUT, DELETE) e Swagger.
  - `TarefaRepository.cs`: Persistência das tarefas em arquivo JSON local.
  - `Tarefa.cs`: Modelo de entidade de tarefa.
  - `Dtos/Inputs` e `Dtos/Outputs`: DTOs para entrada e saída de dados.
- **Tecnologias**:
  - .NET 8
  - Minimal APIs
  - Swagger (OpenAPI)
  - Serialização JSON

### 2. POC.MCP.Server.Agendamento

- **Descrição**: Cliente HTTP para consumir a API de tarefas.
- **Principais arquivos**:
  - `Clients/AgendamentoClient.cs`: Métodos para listar, obter, criar, atualizar e excluir tarefas via HTTP.
  - `Models`: Modelos de request/response.
- **Tecnologias**:
  - .NET 8
  - HttpClient
  - Serialização JSON

---

## Tecnologias Utilizadas

- .NET 8
- Minimal APIs
- Swagger/OpenAPI
- Serialização JSON (System.Text.Json)
- Persistência em arquivo local (tarefas.json)

---

## Observações

- As tarefas são persistidas em arquivo local (`tarefas.json`).
- O Swagger está habilitado apenas em ambiente de desenvolvimento.
- O projeto não utiliza banco de dados externo.

---
