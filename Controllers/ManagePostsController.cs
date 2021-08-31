using FutbolowaJaskinia.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Controllers
{
    //new policy
    public class ManagePostsController : Controller
    {
        private readonly ILogger<ManagePostsController> _logger;
        private readonly IUtiRepo _repo;

        public ManagePostsController(ILogger<ManagePostsController> logger,IUtiRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }
    }
}
