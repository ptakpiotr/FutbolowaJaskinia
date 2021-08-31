using FutbolowaJaskinia.Data;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace FutbolowaJaskinia.Utilities
{
    public class HangfireJobs
    {
        private readonly ApiAccess _access;
        private readonly IUtiRepo _repo;
        private readonly ILogger<HangfireJobs> _logger;

        public HangfireJobs(ApiAccess access, IUtiRepo repo, ILogger<HangfireJobs> logger)
        {
            _access = access;
            _repo = repo;
            _logger = logger;
        }

        public void GetHighlights()
        {
            var res = _access.GetHighlightsAsync().GetAwaiter().GetResult();
            _repo.AddHighlights(res);
            _repo.SaveChanges();
        }

        public void DeleteChat()
        {
            _repo.DeleteChat();
            _repo.SaveChanges();
        }


        public void HighlightsJob()
        {
            RecurringJob.AddOrUpdate("HighlightsJob", () => GetHighlights(), Cron.Daily);
        }

        public void ChatJob()
        {
            RecurringJob.AddOrUpdate("ChatJob", () => DeleteChat(), Cron.Daily);
        }
    }
}
