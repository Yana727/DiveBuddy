using System;
using DiveBuddy.Models;

namespace DiveBuddy 
{
    public class PhotosModel
    {
     public int Id { get; set; }
     public string PicName { get; set; }

     public string Url { get; set; }    //Upload url of the image to put into the source tag

     //FK Buisness

        public int BuisnessId { get; set; }
        public BuisnessModel Buisness { get; set; }

     //FK User 

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser {get; set;}   
    }
}