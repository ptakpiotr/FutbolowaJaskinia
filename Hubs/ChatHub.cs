using FutbolowaJaskinia.Data;
using FutbolowaJaskinia.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUtiRepo _repo;

        public ChatHub(IUtiRepo repo)
        {
            _repo = repo;
        }

        public async Task AddMessage(string obj)
        {
            var data = JsonConvert.DeserializeObject<ChatModel>(obj);

            _repo.AddChat(data);
            _repo.SaveChanges();

            await Clients.All.SendAsync("NewMessage", JsonConvert.SerializeObject(data));
        }
    }
}
