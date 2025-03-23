using BlazorUserManagement.Models;

namespace BlazorUserManagement.Services
{
    public interface IChatService
    {
        Task<List<ChatMessage>> GetPrivateMessagesAsync(int userId);
        Task SendPrivateMessageAsync(int userId, ChatMessage message);
    }
}
