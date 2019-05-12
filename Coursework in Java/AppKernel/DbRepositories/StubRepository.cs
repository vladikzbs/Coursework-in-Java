//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Diagnostics;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Threading.Tasks;

//using Coursework_in_Java.Models;

///// <summary>
///// Stub Репозиторий базы данных
///// </summary>
//namespace Coursework_in_Java.AppKernel.DbRepositories
//{
//    public class StubRepository<TEntity> : IRepository
//        where TEntity : class, new()
//    {
//        private ICollection<TEntity> context;
//        private bool disposed;


//        [DebuggerHidden]
//        public StubRepository()
//        {
//            this.context = new List<TEntity>();
//        }

//        /// <summary>
//        /// Добавление нового элемента в базу данных
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="item"></param>
//        public void Add<TEntity>(TEntity item)
//            where TEntity : class
//        {
//            if (item == null)
//            {
//                throw new ArgumentNullException("Empty argument");
//            }
//            else if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//           ;
//        }

//        /// <summary>
//        /// Удаление элемента
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="item"></param>
//        public void Delete<TEntity>(TEntity item)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//           ;
//        }

//        /// <summary>
//        /// Удаление элемента
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        public void Delete<TEntity>(int id)
//             where TEntity : class
//        {
//            ;
//        }

//        /// <summary>
//        /// Удаление элемента
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        public void Delete<TEntity>(string id)
//             where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            TEntity item = entity.Find(id);
//            entity.Remove(item);
//        }

//        /// <summary>
//        /// Освобождение управляемых ресурсов
//        /// </summary>
//        public void Dispose()
//        {
//            if (disposed == true)
//            {
//                return;
//            }

//            context = null;
//            disposed = true;
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="where"></param>
//        /// <returns></returns>
//        public TEntity GetItem<TEntity>(Expression<Func<TEntity, bool>> where)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            if (where == null)
//            {
//                throw new NullReferenceException("Item not found.");
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public TEntity GetItem<TEntity>(int id)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            if (id <= 0 || id > 1)
//            {
//                throw new NullReferenceException("Item not found.");
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public TEntity GetItem<TEntity>(string id)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            if (string.IsNullOrEmpty(id))
//            {
//                throw new NullReferenceException("Item not found.");
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд асинхронно
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="where"></param>
//        /// <returns></returns>
//        public async Task<TEntity> GetItemAsync<TEntity>(Expression<Func<TEntity, bool>> where)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            if (where == null)
//            {
//                throw new NullReferenceException("Item not found.");
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд асинхронно
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public async Task<TEntity> GetItemAsync<TEntity>(int id)
//                    where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            if (id <= 0 || id > 1)
//            {
//                throw new NullReferenceException("Item not found.");
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд асинхронно
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public async Task<TEntity> GetItemAsync<TEntity>(string id)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }
       
//            if (string.IsNullOrEmpty(id))
//            {
//                throw new NullReferenceException("Item not found.");
//            }
//            else
//            {
//                return default;
//            }
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд асинхронно (Не дает исключения, если не нашел)
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="where"></param>
//        /// <returns></returns>
//        public async Task<TEntity> GetItemOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> where)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            TEntity item = await entity.Where(where).SingleOrDefaultAsync();

//            return item == default(TEntity) ? (default) : item;
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд асинхронно (Не дает исключения, если не нашел)
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="where"></param>
//        /// <returns></returns>
//        public async Task<TEntity> GetItemOrDefaultAsync<TEntity>(int id)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            TEntity item = await entity.FindAsync(id);

//            return item == default(TEntity) ? (default) : item;
//        }

//        /// <summary>
//        /// Поиск элемента в сущности бд асинхронно (Не дает исключения, если не нашел)
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="where"></param>
//        /// <returns></returns>
//        public async Task<TEntity> GetItemOrDefaultAsync<TEntity>(string id)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            TEntity item = await entity.FindAsync(id);

//            return item == default(TEntity) ? (default) : item;
//        }

//        public DbSet<TEntity> GetItems<TEntity>() where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            return entity;
//        }

//        public DbSet<TEntity> GetItems<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            DbSet<TEntity> result = entity.Where(where) as DbSet<TEntity>;
//            return result;
//        }

//        public DbSet<TEntity> Include<TEntity, TEntityOutput>(Expression<Func<TEntity, TEntityOutput>> include) where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            DbSet<TEntity> result = entity.Include(include) as DbSet<TEntity>;
//            return result;
//        }

//        public async Task<DbSet<TEntity>> GetItemsAsync<TEntity>() where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            return entity;
//        }

//        public async Task<DbSet<TEntity>> GetItemsAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            DbSet<TEntity> result = entity.Where(where) as DbSet<TEntity>;
//            return result;
//        }

//        /// <summary>
//        /// Сохранение изменений в бд
//        /// </summary>
//        public void Save()
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            context.SaveChanges();
//        }

//        /// <summary>
//        /// Сохранение изменений в бд асинхронно
//        /// </summary>
//        /// <returns></returns>
//        public async Task SaveAsync()
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            await context.SaveChangesAsync();
//        }

//        /// <summary>
//        /// Обновление значений сущности в бд
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="item"></param>
//        public void Update<TEntity>(TEntity item)
//                        where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            context.Entry(item).State = EntityState.Modified;
//        }

//        /// <summary>
//        /// Обновление значений сущности в бд
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        public void Update<TEntity>(int id)
//             where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            TEntity item = entity.Find(id);
//            context.Entry(item).State = EntityState.Modified;
//        }

//        /// <summary>
//        /// Обновление значений сущности в бд
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <param name="id"></param>
//        public void Update<TEntity>(string id)
//             where TEntity : class
//        {
//            if (disposed == true)
//            {
//                throw new ObjectDisposedException("Object was disposed");
//            }

//            DbSet<TEntity> entity = GetEntity<TEntity>();
//            TEntity item = entity.Find(id);
//            context.Entry(item).State = EntityState.Modified;
//        }

//        /// <summary>
//        /// Метод определяет с помощью рефлексии к какой таблице идет обращение
//        /// </summary>
//        /// <typeparam name="TEntity"></typeparam>
//        /// <returns></returns>
//        private DbSet<TEntity> GetEntity<TEntity>()
//            where TEntity : class
//        {
//            Type entityType = typeof(DbSet<TEntity>);
//            PropertyInfo[] properties = context.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

//            DbSet<TEntity> table = null;

//            foreach (var property in properties)
//            {
//                Type propertyType = property.GetValue(context).GetType();

//                if (propertyType == entityType)
//                {
//                    table = property.GetValue(context) as DbSet<TEntity>;
//                    break;
//                }
//            }

//            if (table == null)
//            {
//                throw new Exception("Not initiaized repository!");
//            }

//            return table;
//        }
//    }
//}