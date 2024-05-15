using System.ComponentModel.DataAnnotations;

namespace TravelApp.Services.Models
{
    public class Airport
    {
        [Key]
        public string? IATA { get; set; }
        public string? Name { get; set; }
    }
}
