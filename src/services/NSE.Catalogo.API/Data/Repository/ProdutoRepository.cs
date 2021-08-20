using NSE.Catalogo.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;

namespace NSE.Catalogo.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        public ProdutoRepository(CatalogoContext context)
        {
            this._context = context;
        }

        public IUnitOfWork UnitOfWork => this._context;

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await this._context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await this._context.Produtos.FindAsync(id);
        }

        public void Adicionar(Produto produto)
        {
            this._context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            this._context.Produtos.Update(produto);
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }
    }
}