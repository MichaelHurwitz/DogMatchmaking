using dogMatchmaking.Dto;

namespace dogMatchmaking.Services
{
    public interface IRandomUserService
    {
        Task<List<UserDto>> GetRandomUsersAsync(int count);
    }
}
