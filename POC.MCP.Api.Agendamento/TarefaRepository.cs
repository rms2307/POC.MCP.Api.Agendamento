using System.Text.Json;

namespace POC.MCP.Api.Agendamento;

public class TarefaRepository
{
    private readonly string _filePath = "tarefas.json";
    private List<Tarefa> _tarefas;
    private int _proximoId;

    public TarefaRepository()
    {
        CarregarTarefas();
    }

    public IEnumerable<Tarefa> ObterTodas() => _tarefas;

    public Tarefa? ObterPorId(int id) => _tarefas.FirstOrDefault(t => t.Id == id);

    public Tarefa Adicionar(string titulo, string? descricao)
    {
        var tarefa = new Tarefa
        {
            Id = _proximoId++,
            Titulo = titulo,
            Descricao = descricao,
            Concluida = false,
            CriadaEm = DateTime.Now
        };
        
        _tarefas.Add(tarefa);
        SalvarTarefas();
        return tarefa;
    }

    public bool Atualizar(int id, string? titulo, string? descricao, bool? concluida)
    {
        var tarefa = ObterPorId(id);
        if (tarefa is null) return false;

        if (titulo is not null) tarefa.Titulo = titulo;
        if (descricao is not null) tarefa.Descricao = descricao;
        if (concluida.HasValue) tarefa.Concluida = concluida.Value;

        SalvarTarefas();
        return true;
    }

    public bool Remover(int id)
    {
        var tarefa = ObterPorId(id);
        if (tarefa is null) return false;

        _tarefas.Remove(tarefa);
        SalvarTarefas();
        return true;
    }

    private void CarregarTarefas()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var dados = JsonSerializer.Deserialize<DadosArquivo>(json);
            _tarefas = dados?.Tarefas ?? new List<Tarefa>();
            _proximoId = dados?.ProximoId ?? 1;
        }
        else
        {
            _tarefas = new List<Tarefa>();
            _proximoId = 1;
        }
    }

    private void SalvarTarefas()
    {
        var dados = new DadosArquivo { Tarefas = _tarefas, ProximoId = _proximoId };
        var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    private record DadosArquivo
    {
        public List<Tarefa> Tarefas { get; set; } = new();
        public int ProximoId { get; set; }
    }
}