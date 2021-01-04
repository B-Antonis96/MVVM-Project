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
    public sealed class WachtwoordService
    {
        //BENODIGDHEDEN\\
        private readonly IPasswordHasher Hasher = new PasswordHasher(); //ASP.NET onderdeel => zinloos om het wiel opnieuw uit te vinden !
        private DataService DataService = new DataService();
        private DataTools Tools = new DataTools();
        public string ResultaatString { get; set; }
        public string Code { get; set; }
        public int AccountID { get; set; }

        //Custom PasswordHasher
        public string HashWachtwoord(string wachtwoord) => Hasher.HashPassword(wachtwoord); 

        //Custom HashChecker
        public bool HashCheck(string wachtwoord, string accountWachtwoord) => Hasher.VerifyHashedPassword(accountWachtwoord, wachtwoord) == PasswordVerificationResult.Success;

        //Verander wachtwoord
        public void VeranderWachtwoord(Account account, string niewWachtwoord, string bevestigWachtwoord)
        {
            string[] zinnen = { "Wachtwoord zijn niet hetzelfde!", "Wachtwoord is aangepast!" };

            ResultaatString = zinnen[0];

            //Controleren of velden hetzelfde zijn
            if (WachtwoordCheck(niewWachtwoord, bevestigWachtwoord))
            {
                //Lengte van wachtwoord controleren
                if (!Tools.SizeChecker(8, niewWachtwoord.Length))
                {
                    //Wachtwoord hashen en opslaan als neiuw wachtwoord
                    account.Wachtwoord = HashWachtwoord(niewWachtwoord);
                    ResultaatString = zinnen[1];
                    DataService.AanpassenAccount(account);
                }
            }
        }

        //Wachtwoord vergeten
        public void WachtwoordVergeten(int id, string niewWachtwoord, string bevestigWachtwoord, string code)
        {
            string[] zinnen = { "Email is niet gelinkt aan een account!", "Code komt niet overeen!", "Wachtwoord aangepast.\nLogin met nieuwe wachtwoord!" };

            ResultaatString = zinnen[0];

            //Id op grootte controleren
            if (Tools.SizeChecker(id, 0))
            {
                ResultaatString = zinnen[1];

                //Codes controleren met elkaar
                if (Tools.ParameterCheck(code, Code))
                {
                    //Controleren of wachtwoorden hetzelfde zijn
                    if (WachtwoordCheck(niewWachtwoord, bevestigWachtwoord))
                    {
                        //Account ophalen en wachtwoord veranderen + opslaan
                        Account account = DataService.AccountOphalenOpID(id);
                        account.Wachtwoord = HashWachtwoord(niewWachtwoord);
                        ResultaatString = zinnen[2];
                        DataService.AanpassenAccount(account);
                    }
                }
            }
        }

        //Wachtwoord vergeten helper, code generator
        public bool CodeGenerator(string email) //Genereerd een code op basis van email door alles naar chars te gooien en hier elke letter op het plaats van het alfabet op te halen
        {
            ResultaatString = "Email is niet gelinkt aan een account!";

            int id = DataService.AccountIDOphalenOpEmail(email);
            if (Tools.SizeChecker(id, 0))
            {
                string uitkomst = "";
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                string clean = rgx.Replace(email, "");
                for (int i = 0; i < clean.Length; i++)
                {
                    uitkomst += (char.ToUpper(clean[i]) - 64).ToString();
                }
                Code = uitkomst;
                AccountID = id;
                return true;
            }
            return false;
        }

        //Wachtwoord helper lengte en hetzelfde
        public bool WachtwoordCheck(string niewWw, string bevestigWw)
        {
            string[] zinnen = { "Wachtwoord moet minstens 8 tekens bevatten!", "Wachtwoorden komen niet overeen!" };

            ResultaatString = zinnen[0];
            if (Tools.SizeChecker(niewWw.Length, 8))
            {
                ResultaatString = zinnen[1];
                if (Tools.ParameterCheck(niewWw, bevestigWw))
                {
                    ResultaatString = "";
                    return true;
                }
            }
            return false;
        }
    }


}
