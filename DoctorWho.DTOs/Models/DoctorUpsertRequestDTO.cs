namespace DoctorWho.DTOs.Models
{
    public class DoctorUpsertRequestDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = String.Empty;
        public int DoctorNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
