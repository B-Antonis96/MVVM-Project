using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("Accounts", Schema = "Match4Ever")]
    public class Account
    {
        //PRIMARY KEY
        public int AccountID { get; set; }

        //FOREIGN KEY
        public int LocatieID { get; set; }

        //ATTRIBUTEN
        [Required]
        [MaxLength(26)]
        [Index(IsUnique = true)]
        public string Gebruikersnaam { get; set; }

        [Required]
        [MaxLength(255)] //50
        [Index(IsUnique = true)]
        public string Emailadres { get; set; } //Beter ook encryptie?

        [Required]
        [MaxLength(255)]
        public string Wachtwoord { get; set; } //Zeker geëncrypteerd!

        [MaxLength(255)] //50
        public string ProfielfotoLink { get; set; } //Beter ook encryptie?

        [MaxLength(255)] //26
        public string Voornaam { get; set; } //Beter ook encryptie?

        [MaxLength(255)] //26
        public string Achternaam { get; set; } //Beter ook encryptie?

        [MaxLength(25)]
        public string Geslacht { get; set; }

        [MaxLength(25)]
        public string Geaardheid { get; set; }

        public DateTime Geboortedatum { get; set; }

        public DateTime LaatsteLogin { get; set; }

        public bool IsAdmin { get; set; } //Kijken of gebruiker (Account) Admin is!

        //NAVIGATIE

        //Locatie meegeven
        public Locatie Locatie { get; set; }

        //Collectie van AccountVoorkeur
        public ICollection<AccountVoorkeur> AccountVoorkeuren { get; set; }

        //Collectie van Matchen
        public ICollection<Match> Matchen { get; set; }
    }
}
