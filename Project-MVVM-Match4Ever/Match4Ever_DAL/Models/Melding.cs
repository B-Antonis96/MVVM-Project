using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("Meldingen", Schema = "Match4Ever")]
    public class Melding
    {
        //PRIMARY KEY
        [Key]
        public int MeldingID { get; set; }

        //ATTRIBUTEN
        [MaxLength(255)]
        public string Zin { get; set; }

        public DateTime DatumMelding { get; set; }

        //NAVIGATIE

        //FOREIGN KEY naar match
        [ForeignKey("MatchID")]
        public Match Match { get; set; }
    }
}
