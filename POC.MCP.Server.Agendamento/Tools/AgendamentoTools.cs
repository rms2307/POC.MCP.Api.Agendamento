using ModelContextProtocol.Server;
using POC.MCP.Server.Agendamento.Clients;
using POC.MCP.Server.Agendamento.Models;
using System.ComponentModel;
using System.Text.Json;

namespace POC.MCP.Server.Agendamento.Tools
{
    [McpServerToolType]
    public static class AgendamentoTools
    {
        [McpServerTool, Description("Lista todas as tarefas. Retorna uma lista com todas as tarefas cadastradas. Saída: Array de objetos com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")]
        public static async Task<string> ListarTarefasAsync(AgendamentoClient client)
        {
            try
            {
                var tarefas = await client.ListarTarefasAsync();

                return !tarefas.Any()
                    ? "Nenhuma tarefa encontrada"
                    : JsonSerializer.Serialize(tarefas);
            }
            catch (Exception)
            {
                return "Erro ao listar tarefas";
            }
        }

        [McpServerTool, Description("Obtém uma tarefa específica pelo código/Id. Retorna os dados de uma tarefa específica pelo seu ID. Entrada: id (int) na URL. Saída: Objeto com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")]
        public static async Task<string> ObterTarefaAsync(AgendamentoClient client, [Description("Filtro obrigatório. Código/Id da tarefa.")] int id)
        {
            try
            {
                var tarefa = await client.ObterTarefaAsync(id);

                return tarefa is null
                    ? "Nenhuma tarefa encontrada"
                    : JsonSerializer.Serialize(tarefa);
            }
            catch (Exception)
            {
                return "Erro ao buscar tarefa";
            }
        }

        [McpServerTool, Description("Cria uma nova tarefa. Entrada: titulo (string obrigatório), descricao (string opcional). Saída: Objeto com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")]
        public static async Task<string> CriarTarefaAsync(AgendamentoClient client, [Description("Título da tarefa")] string titulo, [Description("Descrição da tarefa")] string? descricao = null)
        {
            try
            {
                var request = new CreateTarefaRequest(titulo, descricao);
                var tarefa = await client.CriarTarefaAsync(request);
                return JsonSerializer.Serialize(tarefa);
            }
            catch (Exception)
            {
                return "Erro ao criar tarefa";
            }
        }

        [McpServerTool, Description("Atualiza uma tarefa existente. Entrada: id (int obrigatório), titulo (string opcional), descricao (string opcional), concluida (bool opcional). Saída: Objeto com Id (int), Titulo (string), Descricao (string), Concluida (bool), CriadaEm (DateTime)")]
        public static async Task<string> AtualizarTarefaAsync(AgendamentoClient client, [Description("Código/Id da tarefa")] int id, [Description("Novo título da tarefa")] string? titulo = null, [Description("Nova descrição da tarefa")] string? descricao = null, [Description("Status de conclusão da tarefa")] bool? concluida = null)
        {
            try
            {
                var request = new UpdateTarefaRequest(titulo, descricao, concluida);
                var tarefa = await client.AtualizarTarefaAsync(id, request);

                return tarefa is null
                    ? "Nenhuma tarefa atualizada"
                    : "Tarefa atualizada com sucesso";
            }
            catch (Exception)
            {
                return "Erro ao atualizar tarefa";
            }
        }

        [McpServerTool, Description("Exclui uma tarefa pelo código/Id. Entrada: id (int obrigatório). Saída: Mensagem de confirmação ou erro")]
        public static async Task<string> ExcluirTarefaAsync(AgendamentoClient client, [Description("Código/Id da tarefa")] int id)
        {
            try
            {
                var sucesso = await client.ExcluirTarefaAsync(id);
                return sucesso
                    ? "Tarefa excluída com sucesso"
                    : "Tarefa não encontrada";
            }
            catch (Exception)
            {
                return "Erro ao excluir tarefa";
            }
        }
    }
}
