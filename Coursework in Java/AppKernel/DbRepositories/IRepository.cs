using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_in_Java.AppKernel.DbRepositories
{
    public interface IRepository : IDisposable
    {
        void Add<TEntity>(TEntity item) where TEntity : class;
        void Update<TEntity>(TEntity item) where TEntity : class;
        void Update<TEntity>(int id) where TEntity : class;
        void Update<TEntity>(string id) where TEntity : class;
        void Delete<TEntity>(TEntity item) where TEntity : class;
        void Delete<TEntity>(int id) where TEntity : class;
        void Delete<TEntity>(string id) where TEntity : class;
        void Save();
        Task SaveAsync();
        TEntity GetItem<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        TEntity GetItem<TEntity>(int id) where TEntity : class;
        TEntity GetItem<TEntity>(string id) where TEntity : class;
        Task<TEntity> GetItemAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        Task<TEntity> GetItemAsync<TEntity>(int id) where TEntity : class;
        Task<TEntity> GetItemAsync<TEntity>(string id) where TEntity : class;
        Task<TEntity> GetItemOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        Task<TEntity> GetItemOrDefaultAsync<TEntity>(int id) where TEntity : class;
        Task<TEntity> GetItemOrDefaultAsync<TEntity>(string id) where TEntity : class;
        DbSet<TEntity> Include<TEntity, TEntityOutput>(Expression<Func<TEntity, TEntityOutput>> include) where TEntity : class;
        DbSet<TEntity> GetItems<TEntity>() where TEntity : class;
        DbSet<TEntity> GetItems<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        Task<DbSet<TEntity>> GetItemsAsync<TEntity>() where TEntity : class;
        Task<DbSet<TEntity>> GetItemsAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
    }
}
