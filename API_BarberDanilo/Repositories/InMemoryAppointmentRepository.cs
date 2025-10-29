using API_BarberDanilo.Interfaces;
using API_BarberDanilo.Models;

namespace API_BarberDanilo.Repositories
{
    public class InMemoryAppointmentRepository : IRepository<Appointment>
    {
        private readonly List<Appointment> _appointments = new();
        private int _nextId = 1;

        public InMemoryAppointmentRepository()
        {
            // Datos de ejemplo para empezar
            _appointments.Add(new Appointment
            {
                Id = _nextId++,
                CustomerName = "Juan Pérez",
                PhoneNumber = "+506 8888-7777",
                AppointmentDate = DateTime.Now.AddDays(1),
                Service = "Corte de Cabello",
                IsConfirmed = false
            });
            _appointments.Add(new Appointment
            {
                Id = _nextId++,
                CustomerName = "Carlos Solís",
                PhoneNumber = "8765-4321",
                AppointmentDate = DateTime.Now.AddDays(2),
                Service = "Afeitado Clásico",
                IsConfirmed = true
            });
        }

        public Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Appointment>>(_appointments.OrderBy(a => a.AppointmentDate));
        }

        public Task<Appointment?> GetByIdAsync(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(appointment);
        }

        public Task AddAsync(Appointment entity)
        {
            entity.Id = _nextId++;
            entity.CreatedAt = DateTime.UtcNow;
            _appointments.Add(entity);
            return Task.CompletedTask;
        }

        public void Update(Appointment entity)
        {
            var existing = _appointments.FirstOrDefault(a => a.Id == entity.Id);
            if (existing != null)
            {
                var index = _appointments.IndexOf(existing);
                _appointments[index] = entity;
            }
        }

        public void Delete(Appointment entity)
        {
            _appointments.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }
    }
}
