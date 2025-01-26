using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using CruiseShip.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
namespace CruiseShip.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
     {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet=db.Set<T>();
            _db.Facilities.Include(u => u.CreatedByUser).Include(u=>u.CreatedBy);
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.
                    Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.
                    Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                } 
            }
            return await query.ToListAsync();
        }

        public async Task Remove(T entity)
        {
            await Task.Run(() => dbSet.Remove(entity));
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            await Task.Run(() => dbSet.RemoveRange(entities));
        }


    }
}
