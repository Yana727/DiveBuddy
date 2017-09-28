using System.Collections.Generic;

namespace DiveBuddy{
    public class BuisnessModel{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string PhoneNbr { get; set; }

        //FK Reviews
        public ICollection<ReviewsModel> Reviews { get; set; } = new HashSet<ReviewsModel>();

    }
}