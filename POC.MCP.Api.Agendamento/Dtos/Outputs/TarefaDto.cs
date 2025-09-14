namespace POC.MCP.Api.Agendamento.Dtos.Outputs;

public record TarefaDto(int Id, string Titulo, string? Descricao, bool Concluida, DateTime CriadaEm);