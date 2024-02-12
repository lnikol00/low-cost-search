using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TravelApp.Extensions;
using TravelApp.Filters;
using TravelApp.Models;
using TravelApp.Models.Amadeus;
using TravelApp.Repositories;

namespace TravelApp.Controllers
{
    [AuthFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class AmadeusController : ControllerBase
    {
        private DatabaseContext _db;

        private readonly IAmadeusRepository _amadaeusRepository;

        public AmadeusController(DatabaseContext db, IAmadeusRepository amadeusRepository)
        {
            _db = db;
            _amadaeusRepository = amadeusRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new SearchParams());
        }

        [HttpPost("async")]
        public async Task<ActionResult> SearchFlights(SearchParams searchParams)
        {
            var results = new List<SearchResultModel>();

            //Provjeri da li u bazi ima spremljena pretraga sa danim parametrima
            var existingSearch = await _db.Searches
                .FirstOrDefaultAsync(x =>
                    x.DepartureAirport.Equals(searchParams.DepartureAirport)
                    && x.ArrivalAirport.Equals(searchParams.ArrivalAirport)
                    && x.DepartureDate == searchParams.DepartureDate
                    && x.Currency == searchParams.Currency
                    && x.Passengers == searchParams.Passengers
                    && x.ReturnDate == (searchParams.ReturnDate ?? new DateTime(1900, 1, 1))
                );

            //Ako nema u bazi zovi API i spremi u bazu
            if (existingSearch == null)
            {
                searchParams.JsonResult = await _amadaeusRepository.GetFlightAsync(
                searchParams.DepartureAirport,
                searchParams.ArrivalAirport,
                searchParams.DepartureDate.ToString("yyyy-MM-dd"),
                searchParams.ReturnDate.HasValue ? searchParams.ReturnDate.Value.ToString("yyyy-MM-dd") : null,
                searchParams.Currency.ToString(),
                searchParams.Passengers,
                Request.HttpContext.Session.GetObjectFromJson<ApiTokenModel>("token").AccessToken);

                //U bazi sam složio kompozitni ključ koji obuhvaća sve parametre, a PK ne može sadržavati NULL, pa u bazu umjesto NULL spremam dummy vrijednost
                if (!searchParams.ReturnDate.HasValue)
                {
                    searchParams.ReturnDate = new DateTime(1900, 1, 1);
                }

                _db.Add(searchParams);

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            else
            {
                searchParams.JsonResult = existingSearch.JsonResult;
            }

            //Parsiram u C# -> objekte sam generirao preko https://json2csharp.com/
            var flights = JsonConvert.DeserializeObject<SearchResult>(searchParams.JsonResult);

            if (flights != null && flights.Meta.Count > 0)
            {
                //Polazni i odredišni aerodromi su uvijek isti -> oni koje smo odabrali, a kako ne prikazujem aerodrome preko kojih presjedamo,
                //uzimam samo ta 2, inaće bih dohvatio sve i filtrirao u memoriji kako se ne bih morao svaki puta spajati na bazu
                //var iataCodes = await _db.Airports.ToListAsync();

                var departure = _db.Airports.First(x => x.IATA == searchParams.DepartureAirport).Name;
                var destination = _db.Airports.First(x => x.IATA == searchParams.ArrivalAirport).Name;

                flights.Data.ForEach(dataItem =>
                {
                    var model = new SearchResultModel();
                    model.DepartureAirportName = departure;
                    model.ArrivalAirportName = destination;

                    //Itineraries će uvijek postojati barem jedan, prvi uvijek predstavlja odlazni let,
                    //a ako smo tražili povratne letove, drugi opisuje povratni let,
                    //dok segmenata može biti više, segmenti su u biti transferi
                    var departureFlightData = dataItem.Itineraries[0];
                    var returnFlightData = dataItem.Itineraries.Count > 1 ? dataItem.Itineraries[1] : default;

                    model.DepartureDate = departureFlightData.Segments.First().Departure.At;
                    model.ReturnDate = returnFlightData?.Segments.First().Departure.At;
                    //Ako ima samo jedan segment onda se radi o direktnom letu i nema presjednja
                    model.DepartureTransfers = departureFlightData.Segments.Count - 1;
                    model.ReturnTransfers = returnFlightData?.Segments.Count - 1;
                    model.Passengers = searchParams.Passengers;
                    model.Currency = dataItem.Price.Currency;
                    model.Price = dataItem.Price.GrandTotal; //Ovdje sam mogao dodatno raditi konverziju u decimal, ali nema potrebe jer se radi samo prikaz

                    results.Add(model);
                });
            }

            return Ok(results);
        }

        [HttpGet("async")]
        public async Task<IActionResult> AirportAutocomplete(string term)
        {
            var names = await _db.Airports.Where(x => x.Name.Contains(term)).ToListAsync();
            return new JsonResult(names);
        }

    }

}
