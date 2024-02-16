using HtmlAgilityPack;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WebScrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("////////////////////////////////////////////////////");
            Console.WriteLine("////////////////// IATA DOWNLOADER /////////////////");
            Console.WriteLine("////////////////////////////////////////////////////");

            Console.WriteLine("----------------------------------------------------");

            Console.WriteLine("Želite preuzeti popis IATA kodova?");
            Console.Write("[D] za nastavka, bilo koja druga tipka za izlaz:");

            var userInput = Console.ReadKey();

            Console.WriteLine();

            if (userInput.Key == ConsoleKey.D)
            {
                //Podatke ću odmah staviti u datatable da kasnije mogu napraviti bulk insert, umjesto da spremam jedan po jedan
                DataTable dt = new();
                dt.Columns.Add(new DataColumn("IATA", typeof(string)));
                dt.Columns.Add(new DataColumn("AirportName", typeof(string)));

                using HttpClient httpClient = new();

                //Dohvat podatka za svako slovo abecede
                #region Dohvat i obrada podataka
                for (char letter = 'A'; letter <= 'Z'; letter++)
                {
                    var url = $"https://en.wikipedia.org/wiki/List_of_airports_by_IATA_code:_{letter}";

                    HtmlDocument htmlDocument = new();
                    htmlDocument.LoadHtml(await httpClient.GetStringAsync(url));

                    var table = htmlDocument
                        .DocumentNode
                        .SelectSingleNode("//table")
                        .Descendants("tr")
                        .Where(tr => tr.Elements("td").Count() > 2)
                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim().Replace("&#91;1&#93", "").Replace("&#91;2&#93", "")).ToList())
                        .ToList();

                    //Prikazujem podatke da se vidi da se nešto događa, punim data table
                    foreach (var row in table)
                    {
                        Console.WriteLine($"{row[0]} - {row[3]}");

                        DataRow dr = dt.NewRow();
                        dr["IATA"] = row[0];
                        dr["AirportName"] = row[3];
                        dt.Rows.Add(dr);
                    }
                }
                #endregion

                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Dohvat podataka završen\nSpremam podatke...");

                string connection = "Data Source=DESKTOP-0VO0VSO\\SQLEXPRESS;Initial Catalog=LowCostFlights;TrustServerCertificate=True;Integrated Security=True";
                using SqlConnection con = new SqlConnection(connection);
                using SqlBulkCopy objbulk = new(con);

                //Tablica u koju idu podaci 
                objbulk.DestinationTableName = "Airports";

                //Mapiranje polja
                objbulk.ColumnMappings.Add("IATA", "IATA");
                objbulk.ColumnMappings.Add("AirportName", "Name");

                con.Open();

                //Ispraznim tablicu
                using (SqlCommand command = new SqlCommand("[dbo].[DeleteAirports]", con))
                {
                    command.CommandTimeout = 3600;
                    command.ExecuteScalar();
                }

                //Spremim nove podatke
                objbulk.WriteToServer(dt);
                con.Close();

                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Baza podataka ažurirana\nNapuštam program...");
            }
            else
            {
                Console.WriteLine("Napuštam program...");
            }

            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}