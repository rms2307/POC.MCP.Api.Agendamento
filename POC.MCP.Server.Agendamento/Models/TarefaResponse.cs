namespace POC.MCP.Server.Agendamento.Models;

public record TarefaResponse(int Id, string Titulo, string? Descricao, bool Concluida, DateTime CriadaEm);