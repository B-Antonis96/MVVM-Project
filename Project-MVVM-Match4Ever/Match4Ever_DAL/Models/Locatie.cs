using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("Locaties")]
    public class Locatie
    {
        //PRIMARY KEY
        [Key]
        public int LocatieID { get; set; }

        //ATTRIBUTEN
        [Required]
        [MaxLength(15)]
        public string Stad { get; set; }

        [Required]
        [MaxLength(15)]
        [Index(IsUnique = true)]
        public string Postcode { get; set; } //String omdat in sommige landen een postcode letters kan bevatten

        [Required]
        [MaxLength(26)]
        [Index(IsUnique = true)]
        public string Land { get; set; }

        //NAVIGATIE
        //Collectie van Account('s)
        public ICollection<Account> Accounts { get; set; }
    }
}
