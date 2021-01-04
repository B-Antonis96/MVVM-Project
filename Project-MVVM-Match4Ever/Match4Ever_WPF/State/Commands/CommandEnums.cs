using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.State.Commands
{
    public class CommandEnums { }
    public enum Commands
    {
        Aanmelden,
        Afmelden,
        Registreer,
        Update,
        Vorige,
        Volgende,
        Opslaan,
        Verwijder,

        ToevoegenVoorkeur,
        AanpassenVoorkeur,
        VerwijderenVoorkeur,
        ToevoegenAntwoord,
        AanpassenAntwoord,
        VerwijderenAntwoord
    }
}
