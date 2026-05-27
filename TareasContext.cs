using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tareas> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación entre Tareas y Categorias
        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categorias");
            categoria.HasKey(c => c.CategoriaId);
            categoria.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(c => c.Descripcion).HasMaxLength(500);
        });

        modelBuilder.Entity<Tareas>(tarea =>
        {
            tarea.ToTable("Tareas");
            tarea.HasKey(t => t.TareaId);
            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
            tarea.HasOne(t => t.Categoria).WithMany(c => c.Tareas).HasForeignKey(t => t.CategoriaId);
            tarea.Property(t => t.Descripcion).HasMaxLength(500);
            tarea.Property(t => t.PrioridadTarea);
            tarea.Property(t => t.FechaCreacion);

        });
           
    }
    
}

