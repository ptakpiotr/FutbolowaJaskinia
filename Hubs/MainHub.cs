using AutoMapper;
using FutbolowaJaskinia.Data;
using FutbolowaJaskinia.Models;
using FutbolowaJaskinia.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Hubs
{
    public class MainHub : Hub
    {
        private readonly IUtiRepo _repo;
        private readonly IMapper _mapper;
        private readonly HttpContext _accessor;

        public MainHub(IUtiRepo repo, IMapper mapper, IHttpContextAccessor accesor)
        {
            _repo = repo;
            _mapper = mapper;
            _accessor = accesor.HttpContext;
        }

        public async Task LikePost(string postId)
        {
            var post = _repo.GetOneNews(postId);
            var mapped = _mapper.Map<NewsSignalDTO>(post);
            if (post != null)
            {
                if (!post.Likes.Contains(_accessor.User.Identity.Name))
                {
                    mapped.Likes.Add(_accessor.User.Identity.Name);
                    _mapper.Map(mapped, post);
                    _repo.SaveChanges();
                }
                else await Clients.Caller.SendAsync("LikePresent");
            }
        }


        public async Task AddComment(string msg)
        {
            var comm = JsonConvert.DeserializeObject<CommentModel>(msg);
            //comm.DateOfCreation = DateTime.Now;

            _repo.AddComment(comm);
            _repo.SaveChanges();

            await Clients.All.SendAsync("NewComment", JsonConvert.SerializeObject(comm));
        }
    }
}
