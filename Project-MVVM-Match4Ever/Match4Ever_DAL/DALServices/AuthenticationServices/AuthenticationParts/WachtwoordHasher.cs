using Match4Ever_DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts
{
    public class WachtwoordHasher
    {
        private readonly IPasswordHasher Hasher = new PasswordHasher(); //ASP.NET onderdeel => zinloos om het wiel opnieuw uit te vinden !
        public string HashWachtwoord(string wachtwoord) => Hasher.HashPassword(wachtwoord);
        public bool HashCheck(string wachtwoord, Account account) => Hasher.VerifyHashedPassword(HashWachtwoord(wachtwoord), account.Wachtwoord) == PasswordVerificationResult.Success;
    }
}
