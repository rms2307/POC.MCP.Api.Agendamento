using POC.MCP.Api.Agendamento;
using POC.MCP.Api.Agendamento.Dtos.Inputs;
using POC.MCP.Api.Agendamento.Dtos.Outputs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TarefaRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET /tarefas - Listar todas as tarefas
app.MapGet("/tarefas", (TarefaRepository repository) =>
{
    return repository.ObterTodas().Select(t => new TarefaDto(t.Id, t.Titulo, t.Descricao, t.Concluida, t.CriadaEm));
})
.WithName("ListarTarefas")
.WithSummary("Lista todas as tarefas")
.WithDescription("Retorna uma lista com todas as tarefas cadastradas. Saída: Array de objetos com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")
.Produces<IEnumerable<TarefaDto>>(200);

// GET /tarefas/{id} - Obter tarefa por ID
app.MapGet("/tarefas/{id:int}", (int id, TarefaRepository repository) =>
{
    var tarefa = repository.ObterPorId(id);
    return tarefa is not null 
        ? Results.Ok(new TarefaDto(tarefa.Id, tarefa.Titulo, tarefa.Descricao, tarefa.Concluida, tarefa.CriadaEm))
        : Results.NotFound();
})
.WithName("ObterTarefa")
.WithSummary("Obtém uma tarefa específica")
.WithDescription("Retorna os dados de uma tarefa específica pelo seu ID. Entrada: id (int) na URL. Saída: Objeto com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")
.Produces<TarefaDto>(200)
.Produces(404);

// POST /tarefas - Criar nova tarefa
app.MapPost("/tarefas", (CreateTarefaDto dto, TarefaRepository repository) =>
{
    var tarefa = repository.Adicionar(dto.Titulo, dto.Descricao);
    var tarefaDto = new TarefaDto(tarefa.Id, tarefa.Titulo, tarefa.Descricao, tarefa.Concluida, tarefa.CriadaEm);
    return Results.Created($"/tarefas/{tarefa.Id}", tarefaDto);
})
.WithName("CriarTarefa")
.WithSummary("Cria uma nova tarefa")
.WithDescription("Cria uma nova tarefa com título e descrição opcional. Entrada: Titulo (string obrigatório), Descricao (string opcional). Saída: Objeto criado com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")
.Accepts<CreateTarefaDto>("application/json")
.Produces<TarefaDto>(201);

// PUT /tarefas/{id} - Atualizar tarefa
app.MapPut("/tarefas/{id:int}", (int id, UpdateTarefaDto dto, TarefaRepository repository) =>
{
    if (!repository.Atualizar(id, dto.Titulo, dto.Descricao, dto.Concluida))
        return Results.NotFound();
    
    var tarefa = repository.ObterPorId(id)!;
    var tarefaDto = new TarefaDto(tarefa.Id, tarefa.Titulo, tarefa.Descricao, tarefa.Concluida, tarefa.CriadaEm);
    return Results.Ok(tarefaDto);
})
.WithName("AtualizarTarefa")
.WithSummary("Atualiza uma tarefa existente")
.WithDescription("Atualiza os dados de uma tarefa existente. Entrada: id (int) na URL, Titulo (string opcional), Descricao (string opcional), Concluida (bool opcional). Saída: Objeto atualizado com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")
.Accepts<UpdateTarefaDto>("application/json")
.Produces<TarefaDto>(200)
.Produces(404);

// DELETE /tarefas/{id} - Excluir tarefa
app.MapDelete("/tarefas/{id:int}", (int id, TarefaRepository repository) =>
{
    return repository.Remover(id) ? Results.NoContent() : Results.NotFound();
})
.WithName("ExcluirTarefa")
.WithSummary("Exclui uma tarefa")
.WithDescription("Remove uma tarefa específica pelo seu ID. Entrada: id (int) na URL. Saída: Sem conteúdo (204) se removida com sucesso")
.Produces(204)
.Produces(404);

app.Run();
