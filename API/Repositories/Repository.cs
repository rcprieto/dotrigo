using System.Linq.Expressions;
using API.Data.Context;
using API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected DoTrigoDbContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(DoTrigoDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();

        }

        public virtual TEntity ObterPorId(int id)
        {
            var teste = Db.Database.GetConnectionString();
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> ObterTodos()
        {
            var teste = Db.Database.GetConnectionString();
            return DbSet.AsNoTracking();
        }

        public virtual IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate, string? include = null)
        {
            if (!String.IsNullOrEmpty(include))
                return DbSet.AsNoTracking().Where(predicate).Include(include);
            else
                return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual bool Adicionar(TEntity obj)
        {
            try
            {
                //Db.Database.Log
                DbSet.Add(obj);

                int retorno = Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public virtual bool AdicionarRange(List<TEntity> obj)
        {
            try
            {
                //Db.Database.Log
                DbSet.AddRange(obj);

                int retorno = Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public virtual bool Atualizar(TEntity obj)
        {
            try
            {
                DbSet.Update(obj);
                var teste = Db.SaveChanges();
                //foreach (var dbEntityEntry in Db.ChangeTracker.Entries().ToArray())
                //{

                //    if (dbEntityEntry.Entity != null)
                //    {
                //        dbEntityEntry.State = EntityState.Detached;
                //    }
                //}
                return true;
            }
            catch (Exception e)
            {
                foreach (var dbEntityEntry in Db.ChangeTracker.Entries().ToArray())
                {

                    if (dbEntityEntry.Entity != null)
                    {
                        dbEntityEntry.State = EntityState.Detached;
                    }
                }
                return false;
            }

        }

        public virtual bool AtualizarRange(List<TEntity> obj)
        {
            try
            {
                DbSet.UpdateRange(obj);
                Db.SaveChanges();
                foreach (var dbEntityEntry in Db.ChangeTracker.Entries().ToArray())
                {

                    if (dbEntityEntry.Entity != null)
                    {
                        dbEntityEntry.State = EntityState.Detached;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                foreach (var dbEntityEntry in Db.ChangeTracker.Entries().ToArray())
                {

                    if (dbEntityEntry.Entity != null)
                    {
                        dbEntityEntry.State = EntityState.Detached;
                    }
                }
                return false;
            }

        }

        public virtual bool Desabilitar(int id)
        {
            throw new NotImplementedException();
        }

        public virtual bool Excluir(int id)
        {
            try
            {
                DbSet.Remove(DbSet.Find(id));
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

    }
}