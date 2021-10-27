using System;
using System.Data;
using System.Linq;

using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Core;
using Ninject;

namespace Es.Udc.DotNet.ModelUtil.Dao
{
    public class  GenericDaoEntityFramework<E, PK> : IGenericDao<E, PK>
        where E : class
    {

        #region Private Attributes

        // entityClass is set in the constructor of this class
        private readonly Type entityClass;

        // Context must be set by means of Context property
        private DbContext dbCommonContext;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public Constructor. Initializes a new instance of the <see cref="GenericDaoEntityFramework{E, PK}" /> class.
        /// Sets the value of the EntityClass attribute.
        /// </summary>
        public GenericDaoEntityFramework()
        {
            this.entityClass = typeof(E);
        }

        #endregion

        #region Common Context to all DAOs (injected)

        /// <summary>
        /// Property Context stores the Common Context shared between all DAOs.
        /// It will be injected as a Singleton by NInject framework
        /// </summary>
        /// <see cref="Es.Udc.DotNet.ModelUtil.IoC.IIoCManager"/>
        /// <code>
        ///     /* DbContext for MiniPortal*/
        ///     string connectionString =
        ///     ConfigurationManager.ConnectionStrings["MiniPortalEntities"].ConnectionString;
        /// 
        ///     kernel.Bind<DbContext>().ToSelf().InSingletonScope().WithConstructorArgument("nameOrConnectionString", connectionString);
        /// </code>
        [Inject]
        public DbContext Context
        {
            set
            {
                this.dbCommonContext = value;
            }

            get
            {
                return dbCommonContext;
            }

        }
        #endregion

        #region IGenericDao<E> Common CRUD Operations: Create, Find/Read, Update,  Delete

        public void Create(E entity)
        {
            dbCommonContext.Set<E>().Add(entity);
            dbCommonContext.SaveChanges(); 
        }

        /// <exception cref="InstanceNotFoundException"/>
        public E Find(PK id)
        {
            E result = dbCommonContext.Set<E>().Find(id);

            if (result == null)
                throw new InstanceNotFoundException(id, entityClass.FullName);
            else
                return result;

        }



        public void Update(E entity)
        {
            dbCommonContext.Entry<E>(entity).State = EntityState.Modified;
            dbCommonContext.SaveChanges();

        }

        /// <exception cref="InstanceNotFoundException"/>
        public void Remove(PK id)
        {           
            
            E objectToRemove = default(E);

            try
            {
                // we need to find the object
                objectToRemove = this.Find(id);

                // Another option: 
                // dbCommonContext.Entry(objectToRemove).State = EntityState.Deleted;

                dbCommonContext.Set<E>().Remove(objectToRemove); 
                dbCommonContext.SaveChanges();
            }
            catch (InstanceNotFoundException)
            {
                throw;
            }
            catch (InvalidOperationException e)
            {
                throw new InternalErrorException(e);
            }
            catch(OptimisticConcurrencyException)
            {
                var context = 
                    ((System.Data.Entity.Infrastructure.IObjectContextAdapter)dbCommonContext).ObjectContext;

                context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.ClientWins, objectToRemove);
                context.DeleteObject(objectToRemove);
                context.SaveChanges();
            }
        }

        #endregion

        #region IGenericDao<E> Extended Operations: Attach, Exists, GetAllElements


        public void Attach(E entityAlreadyInDb)
        {
            dbCommonContext.Entry(entityAlreadyInDb).State = EntityState.Added;
            dbCommonContext.SaveChanges();

        }

        public Boolean Exists(PK id)
        {

            return (dbCommonContext.Set<E>().Find(id) != null);

        }

        public List<E> GetAllElements()
        {
            return dbCommonContext.Set<E>().ToList<E>();
        } 

        #endregion


    }

}