using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_BarberDanilo.Models
{
    /// <summary>
    /// Entidad que representa una cita en la barbería
    /// </summary>
    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Service { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; } = true;

        // Campos de auditoría (opcional pero recomendado)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}