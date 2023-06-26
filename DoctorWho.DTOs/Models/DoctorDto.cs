namespace DoctorWho.DTOs.Models
{
    public class DoctorDto
    {
        public string DoctorName { get; set; } = String.Empty;
        public int DoctorNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? FirstEpisodeDate { get; set; }
        public DateTime? LastEpisodeDate { get; set; }
    }
}
