using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match4Ever_DAL.Models
{
    //Tussentabel voor Account en Voorkeur (meer op meer relatie)
    [Table("AccountVoorkeuren", Schema = "Match4Ever")]
    public class AccountVoorkeur
    {
        //PRIMARY KEY
        public int AccountVoorkeurID { get; set; }

        //FOREIGN KEY('S)
        public int AccountID { get; set; }

        public int VoorkeurID { get; set; }

        //NAVIGATIE

        //Account meegeven
        [ForeignKey("AccountID")]
        public Account Account { get; set; }

        //Voorkeur meegeven
        [ForeignKey("VoorkeurID")]
        public Voorkeur Voorkeur { get; set; }
    }
}
