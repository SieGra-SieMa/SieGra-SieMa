using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }

        public virtual User User { get; set; }
        public Log(int UserId, string Action)
        {
            this.UserId = UserId;
            this.Action = Action;
            this.Time = DateTime.Now;
        }
        public Log(User User, string Action)
        {
            this.User = User;
            this.Action = Action;
            this.Time = DateTime.Now;
        }
    }
}
