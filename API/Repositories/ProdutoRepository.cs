using System;
using API.Data.Context;
using API.Data.Repositories;
using API.Domain.Entidades;
using API.Interfaces;

namespace API.Repositories;

public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
{
	public ProdutoRepository(DoTrigoDbContext context) : base(context)
	{
	}
}
