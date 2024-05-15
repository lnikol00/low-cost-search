using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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

        public async Task<List<SearchResultModel>> GetFlightAsync(string departureAirport, string arrivalAirport, string dateFrom, string dateTo, string curency, int passengers, string token)
        {
            string url = _connectionApi.Value.ConnectionString;

            url = url.Replace("departureAirport", departureAirport);
            url = url.Replace("arrivalAirport", arrivalAirport);
            url = url.Replace("dateFrom", dateFrom);
            url = url.Replace("curency", curency);
            url = url.Replace("passengers", passengers.ToString());

            if (!string.IsNullOrEmpty(dateTo))
            {
                url += $"&returnDate={dateTo}";
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

                        var searchParams = new SearchParams
                        {
                            DepartureAirport = departureAirport,
                            ArrivalAirport = arrivalAirport,
                            DepartureDate = DateTime.Parse(dateFrom),
                            ReturnDate = DateTime.Parse(dateTo),
                            Currency = (Currency)Enum.Parse(typeof(Currency), curency),
                            Passengers = passengers

                        };

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
    }
}
