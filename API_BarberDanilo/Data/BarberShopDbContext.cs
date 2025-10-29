using API_BarberDanilo.Models;
using Microsoft.EntityFrameworkCore;

namespace API_BarberDanilo.Data
{
    public class BarberShopDbContext : DbContext
    {
        public BarberShopDbContext(DbContextOptions<BarberShopDbContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Appointment
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Service)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AppointmentDate)
                    .IsRequired();

                entity.Property(e => e.IsConfirmed)
                    .HasDefaultValue(true);
            });

            
        }
    }
}