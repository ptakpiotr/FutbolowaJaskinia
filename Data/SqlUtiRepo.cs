using FutbolowaJaskinia.Models;
using System.Collections.Generic;
using System.Linq;

namespace FutbolowaJaskinia.Data
{
    public class SqlUtiRepo : IUtiRepo
    {
        private readonly UtiDbContext _context;

        public SqlUtiRepo(UtiDbContext context)
        {
            _context = context;
        }

        public void AddChat(ChatModel chat)
        {
            _context.Chats.Add(chat);
        }

        public void AddComment(CommentModel comment)
        {
            _context.Comments.Add(comment);
        }

        public void AddHighlights(IEnumerable<HighlightsModel> highlights)
        {
            _context.Highlights.AddRange(highlights);
        }

        public void AddNews(NewsModel news)
        {
            _context.News.Add(news);
        }

        public void DeleteChat()
        {
            var allChat = _context.Chats.ToList();

            _context.Chats.RemoveRange(allChat);
        }

        public void DeleteComment(string commId)
        {
            var comm = _context.Comments.FirstOrDefault(c => c.Id.ToString() == commId);

            _context.Comments.Remove(comm);
        }

        public List<ChatModel> GetAllChat()
        {
            var allChat = _context.Chats.ToList();

            return (allChat);
        }

        public List<CommentModel> GetComments(string postId)
        {
            var comms = _context.Comments.Where(c => c.PostId == postId).OrderByDescending(c => c.DateOfCreation).ToList();

            return (comms);
        }

        public List<HighlightsModel> GetHighlights()
        {
            var high = _context.Highlights.OrderByDescending(h => h.Date).Take(30).ToList();

            return (high);
        }

        public List<NewsModel> GetNews()
        {
            var news = _context.News.OrderByDescending(n => n.DateOfCreation).Take(9).ToList();

            return (news);
        }

        public NewsModel GetOneNews(string id)
        {
            var news = _context.News.FirstOrDefault(n => n.Id.ToString() == id);

            return (news);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateNews(NewsModel news)
        {
            //automapper does
        }
    }
}
