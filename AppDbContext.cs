using Microsoft.EntityFrameworkCore;
using APIUsuarios.Domain.Entities;

namespace APIUsuarios;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Usuario> Usuarios => Set<Usuario>();
}