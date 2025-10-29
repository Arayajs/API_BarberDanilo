using API_BarberDanilo.Models.DTOs;
using API_BarberDanilo.Models;

namespace API_BarberDanilo.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de citas - Define el contrato de lógica de negocio
    /// Cumple con el Principio de Segregación de Interfaces (I)
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Obtiene todas las citas registradas
        /// </summary>
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();

        /// <summary>
        /// Obtiene una cita específica por su ID
        /// </summary>
        Task<Appointment?> GetAppointmentByIdAsync(int id);

        /// <summary>
        /// Crea una nueva cita con validaciones de negocio
        /// </summary>
        Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto createDto);

        /// <summary>
        /// Actualiza una cita existente
        /// </summary>
        Task<bool> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateDto);

        /// <summary>
        /// Elimina una cita
        /// </summary>
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
