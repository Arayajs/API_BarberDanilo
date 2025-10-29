namespace API_BarberDanilo.Models.DTOs
{
    public class UpdateAppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public string Service { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
