using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity ObterPorId(int id);
        IQueryable<TEntity> ObterTodos();
        IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate, string? include = null);

        bool Adicionar(TEntity obj);
        bool AdicionarRange(List<TEntity> obj);
        bool Atualizar(TEntity obj);
        bool AtualizarRange(List<TEntity> obj);
        bool Desabilitar(int id);
        bool Excluir(int id);

        int SaveChanges();

    }
}