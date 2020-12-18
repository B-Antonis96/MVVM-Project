using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.AuthenticatieService
{
    public enum RegistratieResultaat
    {
        Gelukt,
        WachtwoordenNietHetZelfde,
        EmailBestaatAl,
        GebruikersnaamBestaatAl
    }
    public interface IAuthenticatieService
    {
        Task<RegistratieResultaat> Registreer(string email, string gebruikersnaam, string wachtwoord, string bevestigwachtwoord);

        Task<Account> Login(string gebruikersnaam, string wachtwoord);
    }
}
