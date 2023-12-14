using System;

namespace OusMarieWedding.Models
{
    public class Reservations
    {
        public int Id { get; set; }

        public string FirstName {get;set;}
        public string LastName { get; set; }
      
        public string PhoneNumber { get; set; }

        //public int PlusOneId { get; set; }
        //public virtual PlusOne PlusOne { get; set; }

        public DateTime dateTime { get; set; }
    }
}
