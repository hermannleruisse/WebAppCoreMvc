using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Areas.ADMIN.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObtenirListeElement();
        Task<T?> ObtenirElement(int id);
        Task EnregistrerElement(T entity);
        void MiseAjourElement(T entity);
        void SupprimerElement(T entity);
        Task SaveAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindElementByAsync(Expression<Func<T, bool>> predicate);
    }
}
