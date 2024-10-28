using TravelApp.Services.Models;

namespace TravelApp.Controllers.DTO
{
    public class AirportDTO
    {
        public string? IATA { get; set; }
        public string? Name { get; set; }

        public static AirportDTO FromModel(Airport model)
        {
            return new AirportDTO
            {
                IATA = model.IATA,
                Name = model.Name,
            };
        }
    }
}
