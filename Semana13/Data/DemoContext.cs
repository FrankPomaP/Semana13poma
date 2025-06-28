using Microsoft.EntityFrameworkCore;
using Semana13.Models;

namespace Semana13.Data
{
    public class DemoContext:DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options)
        :base(options)
        {
        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica el filtro global para la eliminación lógica en la entidad Course.
            // Esto asegura que cada vez que se consulte 'Courses', automáticamente
            // se añadirá la condición WHERE Activo = 1.
            modelBuilder.Entity<Course>().HasQueryFilter(c => c.Activo == 1);

            // Si también quieres aplicar un filtro similar a otras entidades,
            // como Grades, si tuvieran una propiedad 'Activo', lo harías de forma similar:
            // modelBuilder.Entity<Grade>().HasQueryFilter(g => g.Activo == 1);
        }
    }

}
