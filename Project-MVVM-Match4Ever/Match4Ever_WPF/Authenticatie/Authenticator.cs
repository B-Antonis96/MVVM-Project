using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.Authenticatie
{
    public class Authenticator : IAuthenticator
    {
        public Account HuidigAccount { get; private set; }

        public bool IsIngelogd => HuidigAccount != null;

        public int WVShaCode => throw new NotImplementedException();

        public async Task<bool> Login(string gebruikersnaam, string email, string wachtwoord)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
