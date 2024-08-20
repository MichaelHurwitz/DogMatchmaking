namespace dogMatchmaking.Services
{
    public interface IDogService
    {
        Task<string> GetRandomDogImageAsync();
    }
}
