using FutbolowaJaskinia.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutbolowaJaskinia.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IUtiRepo _repo;

        public ChatController(IUtiRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var chats = _repo.GetAllChat();

            return View(chats);
        }
    }
}
