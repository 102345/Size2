using Microsoft.EntityFrameworkCore;
using Size.ContaDigital.DAL.Context;
using Size.ContaDigital.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Size.ContaDigital.DAL.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected ContaDigitalContext Db = new ContaDigitalContext();

        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }


        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            Db.SaveChanges();
        }

        public void Dispose()
        {

            Db.Dispose();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Db.Set<TEntity>().ToListAsync();
        }

        public void AddAsync(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return  await Db.Set<TEntity>().FindAsync(id);
        }

      

        public void UpdateAsync(TEntity obj)
        {

            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChangesAsync();
        }

        public void RemoveAsync(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            Db.SaveChangesAsync();
        }
    }
}
