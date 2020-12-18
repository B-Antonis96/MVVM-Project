using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.AuthenticatieService
{
    public interface IAccountService
    {
        Task<Account> OpGebruikersnaam(string gebruikersnaam);
        Task<Account> OpEmail(string email);
    }
}
