namespace DoctorWho.DTOs.Models
{
    public class EpisodeDto
    {
        public int EpisodeId { get; set; }
        public string SeriesNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeType { get; set; }
        public string Title { get; set; }
        public DateTime EpisodeDate { get; set; }
    }
}
