namespace API.DTOs
{
    public class HomePageDTO
    {
          public List<MovieDTO>? InCinemas { get; set; }
        public List<MovieDTO>? UpcomingReleases { get; set; }
    }
}