using OusMarieWedding.Models;
using System.Collections.Generic;

namespace OusMarieWedding.ViewModels
{
    public class RsvpViewModel
    {
        public Reservations Reservations { get; set; }
        public PlusOne PlusOne { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int PlusOneId { get; set; }
        public IEnumerable<Reservations> Reservationss { get; set; }
        public IEnumerable<Speakers> Speakers { get; set; }
        public IEnumerable<Testimonial> Testimonials { get; set; }
        public bool? DataSaved { get; set; }
    }
}
