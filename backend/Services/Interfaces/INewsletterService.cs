using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Interfaces
{
    public interface INewsletterService
    {
        void AddNewsletter(Newsletter Newsletter);
        void RemoveNewsletter(int NewsletterId);
        Newsletter GetNewsletter(int NewsletterId);
        bool CheckNewsletter(int UserId);
        IEnumerable<Newsletter> GetNewsletters();

    }
}
