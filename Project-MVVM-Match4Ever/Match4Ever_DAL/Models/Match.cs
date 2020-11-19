using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    [Table("Matchen")]
    public class Match
    {
        //PRIMARY KEY
        [Key]
        public int MatchID { get; set; }

        //ATTRIBUTEN
        public int Score { get; set; }

        public DateTime MatchDatum { get; set; }

        public bool Type { get; set; } //Type is het verschil tussen "Love & Friend" respectievelijk "true & false", gebaseerd op score maar wordt ook opgeslagen in DB!

        //NAVIGATIE

        //FOREIGN KEY naar Account1 (eigen Account)
        [ForeignKey("AccountID")]
        public Account Account1 { get; set; }

        //FOREIGN KEY naar Account2 (andere Account => match)
        [ForeignKey("AccountID")]
        public Account Account2 { get; set; }

        //Collectie van Melding(en)
        public ICollection<Melding> Meldingen { get; set; }
    }
}
