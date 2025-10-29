using API_BarberDanilo.Interfaces;
using API_BarberDanilo.Models.DTOs;
using API_BarberDanilo.Models;

namespace API_BarberDanilo.Services
{
    /// <summary>
    /// Servicio de lógica de negocio para citas
    /// Cumple con el Principio de Responsabilidad Única (S) - Solo maneja lógica de negocio
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly ILogger<AppointmentService> _logger;

        // Inyección de dependencias (Principio D)
        public AppointmentService(
            IRepository<Appointment> appointmentRepository,
            ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            try
            {
                return await _appointmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las citas");
                throw;
            }
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            try
            {
                return await _appointmentRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la cita con ID {Id}", id);
                throw;
            }
        }

        public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto createDto)
        {
            try
            {
                // --- VALIDACIONES DE NEGOCIO ---

                // 1. Validar que la fecha no sea en el pasado
                if (createDto.AppointmentDate < DateTime.Now)
                {
                    throw new ArgumentException("La fecha de la cita no puede ser en el pasado.");
                }

                // 2. Validar que la fecha no sea más de 3 meses en el futuro
                if (createDto.AppointmentDate > DateTime.Now.AddMonths(3))
                {
                    throw new ArgumentException("No se pueden agendar citas con más de 3 meses de anticipación.");
                }

                // 3. Validar horario laboral (8:00 AM - 6:00 PM)
                var appointmentTime = createDto.AppointmentDate.TimeOfDay;
                if (appointmentTime < new TimeSpan(8, 0, 0) || appointmentTime >= new TimeSpan(18, 0, 0))
                {
                    throw new ArgumentException("Las citas solo están disponibles entre 8:00 AM y 6:00 PM.");
                }

                // 4. Validar que no haya otra cita a la misma hora
                var existingAppointments = await _appointmentRepository.GetAllAsync();
                var conflictingAppointment = existingAppointments.FirstOrDefault(a =>
                    Math.Abs((a.AppointmentDate - createDto.AppointmentDate).TotalMinutes) < 30);

                if (conflictingAppointment != null)
                {
                    throw new ArgumentException(
                        $"Ya existe una cita cercana a esa hora. Intente con otro horario.");
                }

                // Crear la nueva cita
                var appointment = new Appointment
                {
                    CustomerName = createDto.CustomerName.Trim(),
                    AppointmentDate = createDto.AppointmentDate,
                    Service = createDto.Service.Trim(),
                    IsConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _appointmentRepository.AddAsync(appointment);
                await _appointmentRepository.SaveChangesAsync();

                _logger.LogInformation("Cita creada exitosamente para {CustomerName} el {Date}",
                    appointment.CustomerName, appointment.AppointmentDate);

                return appointment;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la cita");
                throw new InvalidOperationException("Error al crear la cita. Por favor, intente nuevamente.", ex);
            }
        }

        public async Task<bool> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateDto)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(id);
                if (appointment == null)
                {
                    _logger.LogWarning("No se encontró la cita con ID {Id}", id);
                    return false;
                }

                // Validar la nueva fecha si se está modificando
                if (updateDto.AppointmentDate != appointment.AppointmentDate)
                {
                    if (updateDto.AppointmentDate < DateTime.Now)
                    {
                        throw new ArgumentException("La fecha de la cita no puede ser en el pasado.");
                    }
                }

                // Actualizar los campos
                appointment.AppointmentDate = updateDto.AppointmentDate;
                appointment.Service = updateDto.Service.Trim();
                appointment.IsConfirmed = updateDto.IsConfirmed;
                appointment.UpdatedAt = DateTime.UtcNow;

                _appointmentRepository.Update(appointment);
                await _appointmentRepository.SaveChangesAsync();

                _logger.LogInformation("Cita {Id} actualizada exitosamente", id);
                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la cita con ID {Id}", id);
                throw new InvalidOperationException("Error al actualizar la cita. Por favor, intente nuevamente.", ex);
            }
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(id);
                if (appointment == null)
                {
                    _logger.LogWarning("No se encontró la cita con ID {Id} para eliminar", id);
                    return false;
                }

                _appointmentRepository.Delete(appointment);
                await _appointmentRepository.SaveChangesAsync();

                _logger.LogInformation("Cita {Id} eliminada exitosamente", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la cita con ID {Id}", id);
                throw new InvalidOperationException("Error al eliminar la cita. Por favor, intente nuevamente.", ex);
            }
        }
    }
}