using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("VoorkeurAntwoorden")]
    public class VoorkeurAntwoord
    {
        //PRIMARY KEY
        [Key]
        public int VoorkeurAntwoordID { get; set; }

        //ATTRIBUTEN
        [Required]
        public string Antwoord { get; set; }

        //NAVIGATIE

        //FOREIGN KEY van Voorkeur
        [ForeignKey("VoorkeurID")]
        public Voorkeur Voorkeur { get; set; }
    }
}
