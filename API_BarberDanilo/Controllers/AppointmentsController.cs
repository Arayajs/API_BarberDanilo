using API_BarberDanilo.Interfaces;
using API_BarberDanilo.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_BarberDanilo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        // Inyectamos la dependencia del servicio (Principio D)
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: api/appointments  (Para que el admin vea todas las citas)
        [HttpGet]
        // En un proyecto real, agregarías: [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        // POST: api/appointments (Para que un usuario cree una cita)
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createDto)
        {
            try
            {
                var newAppointment = await _appointmentService.CreateAppointmentAsync(createDto);
                // Devuelve un 201 Created con la ubicación del nuevo recurso
                return CreatedAtAction(nameof(GetAllAppointments), new { id = newAppointment.Id }, newAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/appointments/{id} (Para que el admin modifique una cita)
        [HttpPut("{id}")]
        // En un proyecto real, agregarías: [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto updateDto)
        {
            var success = await _appointmentService.UpdateAppointmentAsync(id, updateDto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent(); // Devuelve un 204 No Content si fue exitoso
        }

        // DELETE: api/appointments/{id} (Para que el admin elimine una cita)
        [HttpDelete("{id}")]
        // En un proyecto real, agregarías: [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var success = await _appointmentService.DeleteAppointmentAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent(); // Devuelve un 204 No Content si fue exitoso
        }
    }
}