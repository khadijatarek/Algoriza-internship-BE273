using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task <IEnumerable<T>> GetAll(Expression<Func<T, bool>>? criteria,  int? pageNo, int? pageSize,string[]? includes= null);

        Task<T> GetById(int id);
        Task<T> GetById(string id);

        Task<T> Find(Expression<Func<T, bool>> criteria);

        Task<int> Count(Expression<Func<T, bool>>? criteria);
        
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);

        //T Update(T entity);

        //void DeleteById(int id);
        //void DeleteById(string id);
    }
}
