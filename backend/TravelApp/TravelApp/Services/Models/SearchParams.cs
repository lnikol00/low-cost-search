﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApp.Services.Models
{
    [Table("SearchHistory")]
    public class SearchParams
    {
        public string? DepartureAirport { get; set; }

        public string? ArrivalAirport { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        public Currency? Currency { get; set; }

        [Range(1, int.MaxValue)]
        public int Passengers { get; set; }
    }
}
