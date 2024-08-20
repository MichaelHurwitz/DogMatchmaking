using dogMatchmaking.Data;
using dogMatchmaking.Models;
using dogMatchmaking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IRandomUserService _randomUserService;

    public UserController(ApplicationDbContext context, IRandomUserService randomUserService)
    {
        _context = context;
        _randomUserService = randomUserService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.Include(u => u.Dogs).ToListAsync();
        if (!users.Any())
        {
            var randomUsers = await _randomUserService.GetRandomUsersAsync(30);
            users = randomUsers.Select(u => new UserModel
            {
                FullName = $"{u.Name.First} {u.Name.Last}",
                Image = u.Picture.Large
            }).ToList();

            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
        }
        return View(users);
    }

    public async Task<IActionResult> Details(int id)
    {
        var user = await _context.Users.Include(u => u.Dogs).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveDog(int userId, int dogId)
    {
        var dog = await _context.Dogs.FindAsync(dogId);
        if (dog != null && dog.UserId == userId)
        {
            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Details), new { id = userId });
    }
}
