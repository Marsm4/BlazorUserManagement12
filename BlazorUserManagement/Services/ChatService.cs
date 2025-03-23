using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorUserManagement.Models;


namespace BlazorUserManagement.Services
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Получение сообщений для фильма
        public async Task<List<ChatMessage>> GetMessagesByMovieIdAsync(int movieId)
        {
            return await _httpClient.GetFromJsonAsync<List<ChatMessage>>($"api/chat/messages/{movieId}");
        }

        // Отправка сообщения для фильма
        public async Task SendMessageAsync(ChatMessage message)
        {
            var response = await _httpClient.PostAsJsonAsync("api/chat/messages", message);
            response.EnsureSuccessStatusCode();
        }

        // Получение сообщений для личного чата
        public async Task<List<ChatMessage>> GetPrivateMessagesAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<ChatMessage>>($"api/chat/private-messages/{userId}");
        }

        // Отправка сообщения в личный чат
        public async Task SendPrivateMessageAsync(int userId, ChatMessage message)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/chat/private-messages/{userId}", message);
            response.EnsureSuccessStatusCode();
        }
    }
}