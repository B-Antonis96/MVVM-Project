using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("Voorkeuren", Schema = "Match4Ever")]
    public class Voorkeur
    {
        //PRIMARY KEY
        [Key]
        public int VoorkeurID { get; set; }

        //ATTRIBUTEN
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Vraag { get; set; }

        //NAVIGATIE

        //Collectie van VoorkeurAntwoord
        public ICollection<VoorkeurAntwoord> VoorkeurAntwoorden { get; set; }
    }
}
