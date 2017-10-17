using System.Collections.Generic;

namespace DiveBuddy{
    public class BuisnessModel{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNbr { get; set; }
        public string Website { get; set; }
        public BusinessEnum Type { get; set; } //adding enum 

        //FK Reviews                         //because there's going to be many reviews
        public ICollection<ReviewsModel> Reviews { get; set; } = new HashSet<ReviewsModel>();

        //FK Photos
        public ICollection<PhotosModel> Photos {get; set;} = new HashSet<PhotosModel>(); 

    }
}