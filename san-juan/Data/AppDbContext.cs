using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Docente> Docentes { get; set; }
    public DbSet<Curso> Cursos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración explícita de la relación entre Curso y Docente
        modelBuilder.Entity<Curso>()
            .HasOne(c => c.Docente)   // Un Curso tiene un Docente
            .WithMany()                // Un Docente puede tener muchos Cursos (relación uno a muchos)
            .HasForeignKey(c => c.IdDocente);  // La clave foránea en la tabla Curso
    }
}
