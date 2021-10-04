using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public NewsletterService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }
        public void AddNewsletter(Newsletter Newsletter)
        {
            _SieGraSieMaContext.Newsletters.Add(Newsletter);
            _SieGraSieMaContext.SaveChanges();
        }

        public bool CheckNewsletter(int UserId)
        {
            return _SieGraSieMaContext.Newsletters.Any(Newsletter => Newsletter.UserId == UserId);
        }

        public Newsletter GetNewsletter(int NewsletterId)
        {
            return _SieGraSieMaContext.Newsletters.Where(n => n.Id == NewsletterId).SingleOrDefault();
        }

        public IEnumerable<Newsletter> GetNewsletters()
        {
            return _SieGraSieMaContext.Newsletters.ToList();
        }

        public void RemoveNewsletter(int NewsletterId)
        {
            _SieGraSieMaContext.Newsletters.Remove(GetNewsletter(NewsletterId));
            _SieGraSieMaContext.SaveChanges();
        }
    }
}
