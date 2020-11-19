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
    [Table("AccountVoorkeuren")]
    public class AccountVoorkeur
    {
        //PRIMARY KEY
        [Key]
        public int AccountVoorkeurID { get; set; }

        //NAVIGATIE

        //FOREIGN KEY naar Account
        [ForeignKey("AccountID")]
        public Account Account { get; set; }

        //FOREIGN KEY naar Voorkeur
        [ForeignKey("VoorkeurID")]
        public Voorkeur Voorkeur { get; set; }
    }
}
