using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<SearchResultModel>> SearchFlights([FromQuery] string departureAirport, [FromQuery] string arrivalAirport, [FromQuery] string dateFrom, [FromQuery] string dateTo, [FromQuery] string curency, [FromQuery] int passengers)
        {

            string token = Request.HttpContext.Session.GetObjectFromJson<ApiTokenModel>("token").AccessToken;

            var flights = await _amadaeusService.GetFlightAsync(departureAirport, arrivalAirport, dateFrom, dateTo, curency, passengers, token);
            return Ok(flights);
        }
    }

}
