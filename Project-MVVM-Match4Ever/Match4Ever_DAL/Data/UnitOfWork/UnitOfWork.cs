using Match4Ever_DAL.Data.Repositories;
using Match4Ever_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork //Geïmplementeerd uit het voorbeeld van Maaike!
    {
        private IRepository<Account> _accountRepo;
        private IRepository<AccountVoorkeur> _accountVoorkeurRepo;
        private IRepository<Locatie> _locatieRepo;
        private IRepository<Match> _matchRepo;
        private IRepository<Melding> _meldingRepo;
        private IRepository<Voorkeur> _voorkeurRepo;
        private IRepository<VoorkeurAntwoord> _voorkeurAntwoordRepo;
        
        private Match4EverEntities Match4EverEntities { get; }

        public UnitOfWork(Match4EverEntities match4EverEntities)
        {
            this.Match4EverEntities = match4EverEntities;
        }

        public IRepository<Account> AccountRepo
        {
            get
            {
                if (_accountRepo == null)
                {
                    _accountRepo = new Repository<Account>(this.Match4EverEntities);
                }
                return _accountRepo;
            }
        }

        public IRepository<AccountVoorkeur> AccountVoorkeurRepo
        {
            get
            {
                if (_accountVoorkeurRepo == null)
                {
                    _accountVoorkeurRepo = new Repository<AccountVoorkeur>(this.Match4EverEntities);
                }
                return _accountVoorkeurRepo;
            }
        }

        public IRepository<Locatie> LocatieRepo
        {
            get
            {
                if (_locatieRepo == null)
                {
                    _locatieRepo = new Repository<Locatie>(this.Match4EverEntities);
                }
                return _locatieRepo;
            }
        }

        public IRepository<Match> MatchRepo
        {
            get
            {
                if (_matchRepo == null)
                {
                    _matchRepo = new Repository<Match>(this.Match4EverEntities);
                }
                return _matchRepo;
            }
        }

        public IRepository<Melding> MeldingRepo
        {
            get
            {
                if (_meldingRepo == null)
                {
                    _meldingRepo = new Repository<Melding>(this.Match4EverEntities);
                }
                return _meldingRepo;
            }
        }

        public IRepository<Voorkeur> VoorkeurRepo
        {
            get
            {
                if (_voorkeurRepo == null)
                {
                    _voorkeurRepo = new Repository<Voorkeur>(this.Match4EverEntities);
                }
                return _voorkeurRepo;
            }
        }
        public IRepository<VoorkeurAntwoord> VoorkeurAntwoordRepo
        {
            get
            {
                if (_voorkeurAntwoordRepo == null)
                {
                    _voorkeurAntwoordRepo = new Repository<VoorkeurAntwoord>(this.Match4EverEntities);
                }
                return _voorkeurAntwoordRepo;
            }
        }

        public void Dispose()
        {
            Match4EverEntities.Dispose();
        }

        public int Save()
        {
            return Match4EverEntities.SaveChanges();
        }
    }
}
