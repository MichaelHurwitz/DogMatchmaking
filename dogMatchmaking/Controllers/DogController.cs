using dogMatchmaking.Data;
using dogMatchmaking.Models;
using dogMatchmaking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dogMatchmaking.Controllers
{
    public class DogController(ApplicationDbContext context, IDogService dogService) : Controller
    {

        public async Task<IActionResult> Match(int userId)
        {
            var dogImage = await dogService.GetRandomDogImageAsync();
            ViewBag.UserId = userId;
            return View(new DogModel { Image = dogImage });
        }

        [HttpPost]
        public async Task<IActionResult> AdoptDog(int userId, string dogImage, string dogName)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                var dog = new DogModel { Name = dogName, Image = dogImage, UserId = userId };
                context.Dogs.Add(dog);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "User", new { id = userId });
        }
    }
}
