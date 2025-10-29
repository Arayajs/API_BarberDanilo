using System.ComponentModel.DataAnnotations;

namespace API_BarberDanilo.Models.DTOs
{
    public class CreateAppointmentDto
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Service { get; set; }
    }
}
