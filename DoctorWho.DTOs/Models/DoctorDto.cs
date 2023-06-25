namespace DoctorWho.DTOs.Models
{

    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }=String.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime FirstEpisodeDate { get; set; }
        public DateTime LastEpisodeDate { get; set; }
    }


}
