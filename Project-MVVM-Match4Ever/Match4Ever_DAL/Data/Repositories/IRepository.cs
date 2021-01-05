using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.Data.Repositories
{
    public interface IRepository<T> where T : class, new() //Geïmplementeerd uit het voorbeeld van de lessen!
    {
        IEnumerable<T> AllesOphalen();
        void EntityToevoegen(T entity);
        void EntityAanpassen(T entity);
        void EntityVerwijderen(T entity);

        //Uitbreidingen
        IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden);
        IEnumerable<T> Ophalen(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden, params Expression<Func<T, object>>[] includes);

        //Handige Functies
        T ZoekenOpPrimaryKey<TPrimaryKey>(TPrimaryKey id);
        void ToevoegenOfAanpassen(T entity);
        void ToevoegenRange(IEnumerable<T> entities);
        void Verwijderen<TPrimaryKey>(TPrimaryKey id);
        void VerwijderenRange(IEnumerable<T> entities);
    }
}
