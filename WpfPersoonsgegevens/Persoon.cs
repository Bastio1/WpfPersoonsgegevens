using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPersoonsgegevens
{
    class Persoon
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public GeslachtEnum Geslacht { get; set; }
        public string Land { get; set; }
        public int Leeftijd
        {
            get
            {
                int leeftijd = DateTime.Now.Year - Geboortedatum.Year;
                if (Geboortedatum.AddYears(leeftijd) >= DateTime.Now)
                leeftijd--;

                return leeftijd;
            }
        }
        public string VolledigeNaam
        {
            get
            {
                return Voornaam + " " + Achternaam;
            }
        }

    }
    enum GeslachtEnum
    {
        Man, Vrouw, Onbekend
    }

}
