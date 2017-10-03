using System.Collections.Generic;

namespace DiveBuddy{
    public class BuisnessModel{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string PhoneNbr { get; set; }
        public string Website { get; set; }

        //FK Reviews                         //because there's going to be many reviews
        public ICollection<ReviewsModel> Reviews { get; set; } = new HashSet<ReviewsModel>();

    }
}