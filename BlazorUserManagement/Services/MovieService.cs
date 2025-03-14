﻿using BlazorMovieApp.Models;
using System.Net.Http.Json;

namespace BlazorMovieApp.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        public List<Movie> Movies { get; private set; } = new List<Movie>();

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Загрузка фильмов в Movies
        public async Task RefreshMoviesList()
        {
            var movies = await _httpClient.GetFromJsonAsync<List<Movie>>("movies"); // убрали "api/"

            if (movies != null)
            {
                Movies = movies;
            }
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"movies/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Movie>();
            }

            return null;  // Если фильм не найден
        }


        public async Task AddMovieAsync(Movie movie)
        {
            var response = await _httpClient.PostAsJsonAsync("movies", movie);
            response.EnsureSuccessStatusCode();
            await RefreshMoviesList(); // Обновляем список после добавления
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var response = await _httpClient.PutAsJsonAsync($"movies/{movie.Id}", movie);
            response.EnsureSuccessStatusCode();
            await RefreshMoviesList(); // Обновляем список после редактирования
        }

        public async Task DeleteMovieAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"movies/{id}");
                
                    await RefreshMoviesList(); // Обновляем список после удаления
                
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при удалении фильма: {ex.Message}");
            }
        }


    }
}
