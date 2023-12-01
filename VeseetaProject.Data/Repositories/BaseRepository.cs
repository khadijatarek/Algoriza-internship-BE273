using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VeseetaProject.Core.Repositories;

namespace VeseetaProject.Data.Repositories
{
    public class BaseRepository <T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(criteria).ToListAsync();

        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? criteria, int? pageNo, int? pageSize, string[]? includes = null)
        {
            int? startIndex = (pageNo - 1) * pageSize;
            IQueryable<T> query = _context.Set<T>();
            
            if (criteria != null)
            {
                query = query.Where(criteria);
            }
            
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (startIndex.HasValue)
            {
                query = query.Skip(startIndex.Value);
            }

            if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            return await query.ToListAsync();

        }

        public async Task<T> Find(Expression<Func<T, bool>> criteria)
        {
            var result = await _context.Set<T>().SingleOrDefaultAsync(criteria);
            if (result != null)
                return result;
                
            return null;
        }

        public async Task<T> GetById(int id)//, string[]? includes = null)
        {
            var allResults = GetAll();// null, null, null, includes);
            var result = await _context.Set<T>().FindAsync(id);
            if (result != null)
                return result;

            return null;
        }
        public async Task<T> GetById(string id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result != null)
                return result;

            return null;
        }

        public async Task<int> Count(Expression<Func<T, bool>>? criteria)
        {
            if (criteria == null)
            {
                return await _context.Set<T>().CountAsync();
            }
            return await _context.Set<T>().CountAsync(criteria);
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }


     
    }
}
