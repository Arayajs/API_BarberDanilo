using API_BarberDanilo.Data;
using API_BarberDanilo.Interfaces;
using API_BarberDanilo.Models;
using Microsoft.EntityFrameworkCore;

namespace API_BarberDanilo.Repositories
{
    /// <summary>
    /// Implementación del patrón Repository usando Entity Framework Core
    /// Cumple con el Principio de Responsabilidad Única (S) y Inversión de Dependencias (D)
    /// </summary>
    public class EfAppointmentRepository : IRepository<Appointment>
    {
        private readonly BarberShopDbContext _context;

        public EfAppointmentRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Appointment entity)
        {
            await _context.Appointments.AddAsync(entity);
        }

        public void Update(Appointment entity)
        {
            _context.Appointments.Update(entity);
        }

        public void Delete(Appointment entity)
        {
            _context.Appointments.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}