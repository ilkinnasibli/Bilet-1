
using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Bilet1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Post> posts = _context.Posts.ToList();
            return View(posts);
        }
        public IActionResult Details(int id)
        {
            Post post = _context.Posts.Find(id);
            return View(post);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post item)
        {
            if (!ModelState.IsValid) return View(item);
            await _context.Posts.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Post post = _context.Posts.Find(id);
            if (post == null) return NotFound();
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeletePost(int id)
        {
            Post post = _context.Posts.Find(id);
            if (post == null) return NotFound();
            _context.Remove(post);
            _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Post post = _context.Posts.Find(id);

            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Post item)
        {
            if (!ModelState.IsValid) return View(item);
             _context.Posts.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
    

