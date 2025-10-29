using System;
using API.Data.Context;
using API.Data.Repositories;
using API.Domain.Entidades;
using API.Interfaces;

namespace API.Repositories;

public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
{
	public PedidoRepository(DoTrigoDbContext context) : base(context)
	{
	}
}
