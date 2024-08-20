using dogMatchmaking.Dto;
using System.Net.Http;
using System.Text.Json;

namespace dogMatchmaking.Services
{
    public class RandomUserService(HttpClient httpClient) : IRandomUserService
    {
        public async Task<List<UserDto>> GetRandomUsersAsync(int count)
        {
            var response = await httpClient.GetAsync($"https://randomuser.me/api/?results={count}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var randomUsers = JsonSerializer.Deserialize<RandomUsers>(
                    content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return randomUsers?.Results.Select(u => new UserDto
                {
                    Name = u.Name,
                    Picture = u.Picture
                }).ToList() ?? new List<UserDto>();
            }
            return new List<UserDto>();
        }
    }
}
