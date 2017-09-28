using System;
using DiveBuddy.Models;

namespace DiveBuddy
{
    public class ReviewsModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //FK : User
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser {get; set;}

        //FK : Buisness
        public int BuisnessId { get; set; }
        public BuisnessModel Buisness { get; set; }
    }
}