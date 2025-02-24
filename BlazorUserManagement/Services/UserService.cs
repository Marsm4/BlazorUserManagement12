using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorUserManagement.Models;

namespace BlazorUserManagement.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto userDto)
        {
            var response = await _http.PostAsJsonAsync("users/register", userDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var response = await _http.PostAsJsonAsync("users/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDto>();

                // Приветствие для обычных пользователей
                if (user.Role == "User")
                {
                    Console.WriteLine($"Привет, дорогой {user.Name}!");
                }

                return user;
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка входа: {error}");
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDto>>("users/all");
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _http.DeleteAsync($"users/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            var response = await _http.GetAsync($"users/emailExists?email={email}");
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> AddUserAsync(RegisterUserDto userDto)
        {
            var response = await _http.PostAsJsonAsync("users/register", userDto);
            return response.IsSuccessStatusCode;
        }

        // Метод для обновления пользователя через API
        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var response = await _http.PutAsJsonAsync($"users/{id}", userDto);
            return response.IsSuccessStatusCode;
        }
    }
}
