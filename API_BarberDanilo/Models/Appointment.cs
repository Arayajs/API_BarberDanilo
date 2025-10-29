namespace API_BarberDanilo.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Service { get; set; } // Ej: "Corte de Cabello", "Afeitado de Barba"
        public bool IsConfirmed { get; set; } = true; // El administrador podría usar esto
    }
}
