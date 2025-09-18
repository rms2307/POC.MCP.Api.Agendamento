namespace POC.MCP.Server.Agendamento.Models;

public record UpdateTarefaRequest(string? Titulo, string? Descricao, bool? Concluida);