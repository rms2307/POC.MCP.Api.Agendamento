using System.Net;
using System.Text.Json;
using POC.MCP.Server.Agendamento.Models;

namespace POC.MCP.Server.Agendamento.Clients
{
    public class AgendamentoClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public AgendamentoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<IEnumerable<TarefaResponse>> ListarTarefasAsync()
        {
            var response = await _httpClient.GetAsync("/tarefas");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TarefaResponse>>(json, _jsonSerializerOptions) ?? Enumerable.Empty<TarefaResponse>();
        }

        public async Task<TarefaResponse?> ObterTarefaAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/tarefas/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TarefaResponse>(json, _jsonSerializerOptions);
        }

        public async Task<TarefaResponse> CriarTarefaAsync(CreateTarefaRequest request)
        {
            var json = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/tarefas", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TarefaResponse>(responseJson, _jsonSerializerOptions)!;
        }

        public async Task<TarefaResponse?> AtualizarTarefaAsync(int id, UpdateTarefaRequest request)
        {
            var json = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/tarefas/{id}", content);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TarefaResponse>(responseJson, _jsonSerializerOptions);
        }

        public async Task<bool> ExcluirTarefaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/tarefas/{id}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }
}
