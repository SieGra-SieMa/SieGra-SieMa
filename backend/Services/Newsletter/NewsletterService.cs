using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface INewsletterService
    {
        public Task<IEnumerable<Newsletter>> GetNewsletters();
        public Task<Newsletter> GetNewsletter(int UserId);
        public Task<Newsletter> AddNewsletter(int UserId);
        public Task<bool> RemoveNewsletter(int UserId);
        public Task<bool> CheckNewsletter(int UserId);

    }
    public class NewsletterService : INewsletterService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public NewsletterService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }
        public async Task<IEnumerable<Newsletter>> GetNewsletters()
        {
            return _SieGraSieMaContext.Newsletters.ToList();
        }
        public async Task<Newsletter> GetNewsletter(int UserId)
        {
            return _SieGraSieMaContext.Newsletters.Where(n => n.UserId == UserId).SingleOrDefault();
        }
        public async Task<Newsletter> AddNewsletter(int UserId)
        {
            if(await CheckNewsletter(UserId))
            {
                throw new Exception("User already added to newsletter");
            }
            var result = await _SieGraSieMaContext.Newsletters.AddAsync(new Newsletter { UserId = UserId });
            await _SieGraSieMaContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> CheckNewsletter(int UserId)
        {
            return _SieGraSieMaContext.Newsletters.Any(n => n.UserId == UserId);
        }
        public async Task<bool> RemoveNewsletter(int UserId)
        {
            if (!await CheckNewsletter(UserId))
            {
                throw new Exception("User is not subscribed to newsletter");
            }
            _SieGraSieMaContext.Newsletters.Remove(await GetNewsletter(UserId));
            return _SieGraSieMaContext.SaveChanges()>0;
        }
    }
}
