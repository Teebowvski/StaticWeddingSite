using OusMarieWedding.Models;
using System.Collections.Generic;

namespace OusMarieWedding.ViewModels
{
    public class SpeakersViewModel
    {
        public Reservations Reservations { get; set; }
        public Speakers Speakers { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string Position { get; set; }
        public IEnumerable<Speakers> SpeakerList { get; set; }





    }
}
