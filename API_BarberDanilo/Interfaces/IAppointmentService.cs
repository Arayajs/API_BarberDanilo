using API_BarberDanilo.Models.DTOs;
using API_BarberDanilo.Models;

namespace API_BarberDanilo.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto createDto);
        Task<bool> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateDto);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
