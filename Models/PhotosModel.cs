using System;
using DiveBuddy.Models;
using System.Linq; 
using System.Threading.Tasks; 

namespace DiveBuddy 
{ 
    public class PhotosModel 
    {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public byte[] Data { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }
      public string ContentType { get; set; }
      

       //FK : User
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser {get; set;}

        //FK : Buisness
        public int BuisnessId { get; set; }
        public BuisnessModel Buisness { get; set; }
    }
}