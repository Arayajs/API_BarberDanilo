using API_BarberDanilo.Interfaces;
using API_BarberDanilo.Models.DTOs;
using API_BarberDanilo.Models;

namespace API_BarberDanilo.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;

        // Inyectamos la dependencia del repositorio (Principio D)
        public AppointmentService(IRepository<Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto createDto)
        {
            // --- LÓGICA DE NEGOCIO ---
            // Ejemplo: Validar que la fecha no sea en el pasado
            if (createDto.AppointmentDate < DateTime.Now)
            {
                throw new ArgumentException("La fecha de la cita no puede ser en el pasado.");
            }

            // Ejemplo: Validar que no haya otra cita a la misma hora (simplificado)
            var existingAppointments = await _appointmentRepository.GetAllAsync();
            if (existingAppointments.Any(a => a.AppointmentDate == createDto.AppointmentDate))
            {
                throw new ArgumentException("Ya existe una cita para esa fecha y hora.");
            }

            var appointment = new Appointment
            {
                CustomerName = createDto.CustomerName,
                AppointmentDate = createDto.AppointmentDate,
                Service = createDto.Service
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateDto)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }

            appointment.AppointmentDate = updateDto.AppointmentDate;
            appointment.Service = updateDto.Service;
            appointment.IsConfirmed = updateDto.IsConfirmed;

            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }

            _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();
            return true;
        }
    }
}