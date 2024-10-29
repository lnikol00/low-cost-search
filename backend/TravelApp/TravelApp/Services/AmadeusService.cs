using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TravelApp.Controllers.DTO;
using TravelApp.Exceptions;
using TravelApp.Models.Amadeus;
using TravelApp.Models.Configuration;
using TravelApp.Services.Models;

namespace TravelApp.Services
{
    public class AmadeusService : IAmadeusService
    {
        private readonly IOptions<ConnectionApi> _connectionApi;

        private DatabaseContext _db;

        public AmadeusService(IOptions<ConnectionApi> connectionApi, DatabaseContext db)
        {
            _connectionApi = connectionApi;
            _db = db;
        }

        public async Task<List<SearchResultModel>> GetFlightAsync(SearchRequestDTO planes, string token)
        {
            string url = _connectionApi.Value.ConnectionString;

            url = url.Replace("departureAirport", planes.departureAirport);
            url = url.Replace("arrivalAirport", planes.arrivalAirport);
            url = url.Replace("dateFrom", planes.dateFrom);
            url = url.Replace("curency", planes.curency);
            url = url.Replace("passengers", planes.passengers.ToString());

            if (!string.IsNullOrEmpty(planes.dateTo))
            {
                url += $"&returnDate={planes.dateTo}";
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new List<SearchResultModel>();
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseContext = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseContext))
                    {
                        var flights = JsonConvert.DeserializeObject<SearchResult>(responseContext);

                        var searchParams = planes.ToModel();

                        //Provjeri ima li spremljena pretraga sa danim parametrima
                        var existingSearch = await _db.Searches.FirstOrDefaultAsync(x =>
                                x.DepartureAirport.Equals(searchParams.DepartureAirport)
                                && x.ArrivalAirport.Equals(searchParams.ArrivalAirport)
                                && x.DepartureDate == searchParams.DepartureDate
                                && x.Currency == searchParams.Currency
                                && x.Passengers == searchParams.Passengers
                                && x.ReturnDate == searchParams.ReturnDate
                        );

                        //Ako nema u bazi spremi u bazu
                        if (existingSearch == null)
                        {
                            _db.Add(searchParams);

                            try
                            {
                                await _db.SaveChangesAsync();
                            }
                            catch
                            {
                                throw new ErrorMessage("An error occurred while connecting to the database.");
                            }
                        }
                        else
                        {
                            searchParams = existingSearch;
                        }

                        if (flights != null && flights.Meta.Count > 0)
                        {

                            var departure = _db.Airports.First(x => x.IATA == searchParams.DepartureAirport).Name;
                            var destination = _db.Airports.First(x => x.IATA == searchParams.ArrivalAirport).Name;

                            flights.Data.ForEach(dataItem =>
                            {
                                var model = new SearchResultModel();
                                model.DepartureAirportName = departure;
                                model.ArrivalAirportName = destination;

                                var departureFlightData = dataItem.Itineraries[0];
                                var returnFlightData = dataItem.Itineraries.Count > 1 ? dataItem.Itineraries[1] : default;

                                model.DepartureDate = departureFlightData.Segments.First().Departure.At;
                                model.ReturnDate = returnFlightData?.Segments.First().Departure.At;
                                model.DepartureTransfers = departureFlightData.Segments.Count - 1;
                                model.ReturnTransfers = returnFlightData?.Segments.Count - 1;
                                model.Passengers = searchParams.Passengers;
                                model.Currency = dataItem.Price.Currency;
                                model.Price = dataItem.Price.GrandTotal;

                                request.Add(model);
                            });
                        }
                        else
                        {
                            throw new ErrorMessage("No flights available for the selected parameters.");
                        }
                    }
                }
                else
                {
                    throw new ErrorMessage("Wrong input on parameters!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error:{ex}");
            }

            return request;
        }


        public async Task<List<Airport>> AllAirports()
        {
            List<Airport> lstAirports = new List<Airport>();

            var airports = (from airport in _db.Airports
                            select airport).ToList();

            foreach (var a in airports)
            {
                var model = new Airport()
                {
                    IATA = a.IATA,
                    Name = a.Name,
                };

                lstAirports.Add(model);
            }

            return lstAirports;
        }
    }
}
