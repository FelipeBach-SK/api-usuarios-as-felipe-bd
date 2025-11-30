using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIUsuarios.Application.Interfaces;
using APIUsuarios.Domain.Entities;
using APIUsuarios.Infrastructure.Persistence;

namespace APIUsuarios.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _ctx;
        public UsuarioRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Usuario usuario, CancellationToken ct)
        {
            await _ctx.Usuarios.AddAsync(usuario, ct);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken ct)
        {
            return await _ctx.Usuarios.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct)
        {
            return await _ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public async Task<Usuario?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _ctx.Usuarios.FindAsync(new object[] { id }, ct);
        }

        public Task RemoveAsync(Usuario usuario, CancellationToken ct)
        {
            _ctx.Usuarios.Update(usuario);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Usuario usuario, CancellationToken ct)
        {
            _ctx.Usuarios.Update(usuario);
            return Task.CompletedTask;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        {
            return await _ctx.Usuarios.AnyAsync(u => u.Email == email, ct);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return await _ctx.SaveChangesAsync(ct);
        }
    }
}
