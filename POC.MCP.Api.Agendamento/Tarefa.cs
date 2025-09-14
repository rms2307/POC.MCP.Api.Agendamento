
// Modelo da entidade Tarefa
public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Concluida { get; set; }
    public DateTime CriadaEm { get; set; }
}
