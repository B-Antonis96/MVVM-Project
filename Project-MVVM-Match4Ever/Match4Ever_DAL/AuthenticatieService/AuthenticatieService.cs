using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.AuthenticatieService
{
    public class AuthenticatieService : IAuthenticatieService
    {
        private readonly IAccountService _accountService;
        //private readonly IPasswordHasher

        public Task<Account> Login(string gebruikersnaam, string wachtwoord)
        {
            throw new NotImplementedException();
        }

        public Task<RegistratieResultaat> Registreer(string email, string gebruikersnaam, string wachtwoord, string bevestigwachtwoord)
        {
            throw new NotImplementedException();
        }
    }
}
