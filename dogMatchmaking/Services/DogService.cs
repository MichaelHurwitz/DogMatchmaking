using dogMatchmaking.Dto;
using System.Net.Http;
using System.Text.Json;

namespace dogMatchmaking.Services
{
    public class DogService(HttpClient httpClient) : IDogService
    {
        public async Task<string> GetRandomDogImageAsync()
        {
            var response = await httpClient.GetAsync("https://dog.ceo/api/breeds/image/random");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var dogImage = JsonSerializer.Deserialize<DogDto>(
                    content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return dogImage?.Message ?? "";
            }
            return "";
        }
    }
}
