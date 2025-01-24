using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace CruiseShip.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<ActionResult<IEnumerable<T>>> GetAll(string? includeProperties = null);
        Task<ActionResult<T>> Get(Expression<Func<T,bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
}
