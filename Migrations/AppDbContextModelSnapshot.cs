using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using APIUsuarios;

namespace APIUsuarios.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("APIUsuarios.Domain.Entities.Usuario", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();
                b.Property<string>("Nome").IsRequired();
                b.Property<string>("Email").IsRequired();
                b.Property<string>("Senha").IsRequired();
                b.HasKey("Id");
                b.ToTable("Usuarios");
            });
        }
    }
}
