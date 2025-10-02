# POC.MCP.Api.Agendamento

## Vis�o Geral

Este reposit�rio cont�m uma solu��o .NET 8 composta por dois m�dulos principais:

- **POC.MCP.Api.Agendamento**: API REST para gerenciamento de tarefas (CRUD).
- **POC.MCP.Server.Agendamento**: MCP para consumir a API de agendamento de tarefas.

---

## M�dulos

### 1. POC.MCP.Api.Agendamento

- **Descri��o**: API minimalista para cadastro, consulta, atualiza��o e remo��o de tarefas.
- **Principais arquivos**:
  - `Program.cs`: Configura��o dos endpoints REST (GET, POST, PUT, DELETE) e Swagger.
  - `TarefaRepository.cs`: Persist�ncia das tarefas em arquivo JSON local.
  - `Tarefa.cs`: Modelo de entidade de tarefa.
  - `Dtos/Inputs` e `Dtos/Outputs`: DTOs para entrada e sa�da de dados.
- **Tecnologias**:
  - .NET 8
  - Minimal APIs
  - Swagger (OpenAPI)
  - Serializa��o JSON

### 2. POC.MCP.Server.Agendamento

- **Descri��o**: Cliente HTTP para consumir a API de tarefas.
- **Principais arquivos**:
  - `Clients/AgendamentoClient.cs`: M�todos para listar, obter, criar, atualizar e excluir tarefas via HTTP.
  - `Models`: Modelos de request/response.
- **Tecnologias**:
  - .NET 8
  - HttpClient
  - Serializa��o JSON

---

## Tecnologias Utilizadas

- .NET 8
- Minimal APIs
- Swagger/OpenAPI
- Serializa��o JSON (System.Text.Json)
- Persist�ncia em arquivo local (tarefas.json)

---

## Observa��es

- As tarefas s�o persistidas em arquivo local (`tarefas.json`).
- O Swagger est� habilitado apenas em ambiente de desenvolvimento.
- O projeto n�o utiliza banco de dados externo.

---
