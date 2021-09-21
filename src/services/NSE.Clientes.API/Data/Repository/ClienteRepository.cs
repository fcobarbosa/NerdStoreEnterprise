using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Models;
using NSE.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;
        public ClienteRepository(ClientesContext context)
        {
            this._context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Cliente cliente)
        {
            this._context.Clientes.Add(cliente);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await this._context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> GetByCpf(string cpf)
        {
            return await this._context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }
    }
}
