using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.Data.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> Ophalen();
        void Toevoegen(T entity);
        void Aanpassen(T entity);
        void Verwijderen(T entity);

        //Uitbreidingen
        IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden);
        IEnumerable<T> Ophalen(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Ophalen(Expression<Func<T, bool>> voorwaarden, params Expression<Func<T, object>>[] includes);

        //Handige Functies
        T ZoekOpPrimaryKey<TPrimaryKey>(TPrimaryKey id);
        void ToevoegenOfAanpassen(T entity);
        void ToevoegenRange(IEnumerable<T> entities);
        void Verwijderen<TPrimaryKey>(TPrimaryKey id);
        void VerwijderenRange(IEnumerable<T> entities);
    }
}
