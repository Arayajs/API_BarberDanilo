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
            _appointments.Add(new Appointment { Id = _nextId++, CustomerName = "Juan Pérez", AppointmentDate = DateTime.Now.AddDays(1), Service = "Corte de Cabello" });
            _appointments.Add(new Appointment { Id = _nextId++, CustomerName = "Carlos Solís", AppointmentDate = DateTime.Now.AddDays(2), Service = "Afeitado Clásico" });
        }

        public Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Appointment>>(_appointments);
        }

        public Task<Appointment> GetByIdAsync(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(appointment);
        }

        public Task AddAsync(Appointment entity)
        {
            entity.Id = _nextId++;
            _appointments.Add(entity);
            return Task.CompletedTask;
        }

        public void Update(Appointment entity)
        {
            // EF Core maneja esto automáticamente en escenarios conectados.
            // Para la lista en memoria, la referencia ya está actualizada.
        }

        public void Delete(Appointment entity)
        {
            _appointments.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            // En un escenario en memoria, no hay nada que guardar, pero en EF Core esto confirmaría la transacción.
            return Task.FromResult(1); // Retorna 1 como si una fila hubiera sido afectada.
        }
    }
}
