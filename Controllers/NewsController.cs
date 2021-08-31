using AutoMapper;
using FutbolowaJaskinia.Data;
using FutbolowaJaskinia.Models;
using FutbolowaJaskinia.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutbolowaJaskinia.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class NewsController : Controller
    {
        private readonly IUtiRepo _repo;

        private readonly IMapper _mapper;

        public NewsController(IUtiRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(NewsCreateDTO create)
        {
            if (ModelState.IsValid)
            {
                var mapped = _mapper.Map<NewsModel>(create);

                _repo.AddNews(mapped);
                _repo.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(create);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var one = _repo.GetOneNews(id);

            if (one == null)
            {
                return View("Error");
            }

            var output = _mapper.Map<NewsReadDTO>(one);

            var comms = _repo.GetComments(output.Id.ToString());
            output.Comments.AddRange(comms);

            return View(output);
        }
    }
}
