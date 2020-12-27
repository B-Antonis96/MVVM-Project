using Match4Ever_DAL.DALServices.DataServices;
using Match4Ever_DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts
{
    public class WachtwoordService
    {
        //BENODIGDHEDEN\\
        private readonly IPasswordHasher Hasher = new PasswordHasher(); //ASP.NET onderdeel => zinloos om het wiel opnieuw uit te vinden !
        private readonly DataService DataService = new DataService();
        public string ResultaatString { get; set; }
        public string Code { get; set; }

        //Custom PasswordHasher
        public string HashWachtwoord(string wachtwoord) => Hasher.HashPassword(wachtwoord); 

        //Custom HashChecker
        public bool HashCheck(string wachtwoord, string accountWachtwoord) => Hasher.VerifyHashedPassword(accountWachtwoord, wachtwoord) == PasswordVerificationResult.Success;

        //Verander wachtwoord
        public void VeranderWachtwoord(Account account, string niewWachtwoord, string bevestigWachtwoord)
        {
            if (WachtwoordCheck(niewWachtwoord, bevestigWachtwoord))
            {
                ResultaatString = "Wachtwoord kon niet aangepast worden!";
                account.Wachtwoord = HashWachtwoord(niewWachtwoord);
                if (DataService.AanpassenAccount(account))
                {
                    ResultaatString = "Wachtwoord is aangepast!";
                }
            }
        }

        //Wachtwoord vergeten
        public void WachtwoordVergeten(int id, string niewWachtwoord, string bevestigWachtwoord, string code)
        {
            ResultaatString = "Email is niet gelinkt aan een account!";
            if (id > 0)
            {
                ResultaatString = "Code komt niet overeen!";
                if (code == Code)
                {
                    if (WachtwoordCheck(niewWachtwoord, bevestigWachtwoord))
                    {
                        ResultaatString = "Er is een onverwachte fout opgetreden!";
                        Account account = DataService.AccountOphalenOpID(id);
                        account.Wachtwoord = HashWachtwoord(niewWachtwoord);
                        if (DataService.AanpassenAccount(account))
                        {
                            ResultaatString = "Wachtwoord aangepast.\nLogin met nieuwe wachtwoord!";
                        }
                    }
                }
            }
        }

        //Wachtwoord vergeten helper, code generator
        public int CodeGenerator(string email)
        {
            ResultaatString = "Email is niet gelinkt aan een account!";

            int id = DataService.AccountIDOphalenOpEmail(email);
            if (id > 0)
            {
                string uitkomst = "";
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                string clean = rgx.Replace(email, "");
                for (int i = 0; i < clean.Length; i++)
                {
                    uitkomst += (char.ToUpper(clean[i]) - 64).ToString();
                }
                Code = uitkomst;
                return id;
            }
            return 0;
        }

        //Wachtwoord helper lengte en hetzelfde
        public bool WachtwoordCheck(string niewWw, string bevestigWw)
        {
            ResultaatString = "Wachtwoord moet minstens 8 tekens bevatten!";
            if (niewWw.Length > 8)
            {
                ResultaatString = "Wachtwoorden komen niet overeen!";
                if (niewWw == bevestigWw)
                {
                    return true;
                }
            }
            return false;
        }
    }


}
