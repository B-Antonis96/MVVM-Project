using Match4Ever_DAL.Data.Repositories;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable //Geïmplementeerd uit het voorbeeld van de lessen!
    {
        IRepository<Account> AccountRepo { get; }
        IRepository<AccountVoorkeur> AccountVoorkeurRepo { get; }
        IRepository<Locatie> LocatieRepo { get; }
        IRepository<Match> MatchRepo { get; }
        IRepository<Melding> MeldingRepo { get; }
        IRepository<Voorkeur> VoorkeurRepo { get; }
        IRepository<VoorkeurAntwoord> VoorkeurAntwoordRepo { get; }
        int Save();
    }
}
