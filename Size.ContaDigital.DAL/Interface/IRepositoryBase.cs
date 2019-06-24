using System.Collections.Generic;
using System.Threading.Tasks;

namespace Size.ContaDigital.DAL.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void AddAsync(TEntity obj);

        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        void Update(TEntity obj);
        void UpdateAsync(TEntity obj);

        void Remove(TEntity obj);
        void RemoveAsync(TEntity obj);

        void Dispose();
    }
}
