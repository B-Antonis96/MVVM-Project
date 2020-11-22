using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("VoorkeurAntwoorden", Schema = "Match4Ever")]
    public class VoorkeurAntwoord
    {
        //PRIMARY KEY
        public int VoorkeurAntwoordID { get; set; }

        //FOREIGN KEY
        public int VoorkeurID { get; set; }

        //ATTRIBUTEN
        [Required]
        public string Antwoord { get; set; }

        //NAVIGATIE

        //Voorkeur meegeven
        public Voorkeur Voorkeur { get; set; }
    }
}
