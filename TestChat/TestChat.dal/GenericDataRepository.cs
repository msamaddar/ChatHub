using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.SqlClient;
using log4net;

namespace TestChat.dal
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        ILog logger = LogManager.GetLogger(typeof(GenericDataRepository<T>));
        public IList<T> GetAll(params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list = null;

            try
            {
                using (var context = new TestChatEntities())
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    list = dbQuery
                        .AsNoTracking()
                        .ToList<T>();
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return list;
        }

        public IList<T> GetList(Func<T, bool> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            IList<T> list = null;
            try
            {
                using (var context = new TestChatEntities())
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    //apply eager loading
                    if (navigationProperties != null)
                        foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                            dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    list = dbQuery.AsNoTracking().Where<T>(where).ToList<T>();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return list;
        }

        public T GetSingle(Func<T, bool> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            try
            {
                using (var context = new TestChatEntities())
                {
                    IQueryable<T> dbQuery = context.Set<T>();

                    //apply eager loading
                    if (navigationProperties != null)
                        foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                            dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    item = dbQuery.AsNoTracking().FirstOrDefault<T>(where);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return item;
        }

        public void Add(params T[] items)
        {
            try
            {
                using (var context = new TestChatEntities())
                {
                    foreach (T item in items)
                        context.Entry(item).State = EntityState.Added;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        public void Update(params T[] items)
        {
            try
            {
                using (var context = new TestChatEntities())
                {
                    foreach (T item in items)
                        context.Entry(item).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        public void Remove(params T[] items)
        {
            try
            {
                using (var context = new TestChatEntities())
                {
                    foreach (T item in items)
                        context.Entry(item).State = EntityState.Deleted;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
