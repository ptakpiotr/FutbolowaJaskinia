using AutoMapper;
using FutbolowaJaskinia.Data;
using FutbolowaJaskinia.Models;
using FutbolowaJaskinia.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUtiRepo _repo;
        private readonly ApiAccess _api;
        private readonly IEmailSender _sender;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUtiRepo repo, ApiAccess api, IEmailSender sender, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _api = api;
            _sender = sender;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var news = _repo.GetNews();

            var output = _mapper.Map<List<NewsReadDTO>>(news);

            return View(output);
        }

        public IActionResult Highlights()
        {
            var md = _repo.GetHighlights();

            return View(md);
        }

        [Authorize]
        public async Task<IActionResult> Standings(string competition)
        {
            var stnd = await _api.GetStandingsAsync(competition);

            return View(stnd);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ErrorCode(string id)
        {
            var md = new ErrorCodeModel { Code = id };

            return View(md);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult About(ContactFormModel md)
        {
            if (ModelState.IsValid)
            {
                _sender.SendEmailAsync("marschall.lesch5@ethereal.email", md.Title, md.Content);
                ModelState.AddModelError("", "Kontakt wyslany!");
            }
            return View(md);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
