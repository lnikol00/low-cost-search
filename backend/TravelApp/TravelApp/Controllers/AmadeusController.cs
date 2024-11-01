﻿using Microsoft.AspNetCore.Mvc;
using TravelApp.Controllers.DTO;
using TravelApp.Extensions;
using TravelApp.Filters;
using TravelApp.Models;
using TravelApp.Services;
using TravelApp.Services.Models;

namespace TravelApp.Controllers
{
    [AuthFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class AmadeusController : ControllerBase
    {
        private readonly IAmadeusService _amadaeusService;

        public AmadeusController(IAmadeusService amadeusService)
        {
            _amadaeusService = amadeusService;
        }

        [HttpPost]
        public async Task<ActionResult<SearchResultModel>> SearchFlights([FromBody] SearchRequestDTO planes)
        {

            string token = Request.HttpContext.Session.GetObjectFromJson<ApiTokenModel>("token").AccessToken;

            var flights = await _amadaeusService.GetFlightAsync(planes, token);
            return Ok(flights);
        }


        [HttpGet("/airports")]
        public async Task<ActionResult<List<AirportDTO>>> AllAirport()
        {
            var airports = await _amadaeusService.AllAirports();

            var airportInfoDTOs = airports.Select(x => AirportDTO.FromModel(x));

            return Ok(airports);
        }
    }

}
