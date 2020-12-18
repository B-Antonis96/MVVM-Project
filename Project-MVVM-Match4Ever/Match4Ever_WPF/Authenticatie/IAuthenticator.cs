using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_WPF.Authenticatie
{
    public interface IAuthenticator
    {
        Account HuidigAccount { get; }
        bool IsIngelogd { get; }
        int WVShaCode { get; }

        Task<bool> Login(string gebruikersnaam, string email, string wachtwoord);

        void Logout();
    }
}
