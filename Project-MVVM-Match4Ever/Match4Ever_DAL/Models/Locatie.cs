using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("Locaties", Schema = "Match4Ever")]
    public class Locatie
    {
        //PRIMARY KEY
        public int LocatieID { get; set; }

        //ATTRIBUTEN
        [Required]
        [MaxLength(15)]
        public string Stad { get; set; }

        [Required]
        [MaxLength(26)]
        [Index(IsUnique = true)]
        public string Land { get; set; }

        //NAVIGATIE

        //Collectie van Account('s)
        [ForeignKey("LocatieID")]
        public ICollection<Account> Accounts { get; set; }
    }
}
