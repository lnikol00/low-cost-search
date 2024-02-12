# Job Application Task

<h2>Uvod:</h2>
<p>
  Potrebno je napraviti .NET web aplikaciju za pretraživanje cijena low-cost avionskih letova. Ukoliko se
  ne možeš odlučiti za frontend tehnologiju, u KING-u trenutno najviše koristimo ReactJS, a ponekad i
  Angular. 
</p>

<p>Cijene letova se moraju moći pretraživati po:</p>

* Polaznom aerodromu (prema IATA šifri aerodroma)
* Odredišnom aerodromu (prema IATA šifri aerodroma)
* Datumu polaska/povratka
* Broju putnika
* Valuti (USD, EUR, HRK)

<h2>Zadatak</h2>
<p>
  Rezultate je potrebno tablično prikazati na ekranu, vrijednosti koje je potrebno prikazati:
</p>

* Polazni aerodrom
* Odredišni aerodrom
* Datum polaska/povratka
* Broj presjedanja u odlaznom/povratnom putovanju
* Broj putnika
* Valuta
* Ukupna cijena

<b>Dodatno, dohvaćene podatke je potrebno lokalno pohraniti i u slučaju ponovne pretrage po istim
parametrima dohvatiti lokalno pohranjene podatke.</b>

* Podatke o letovima se dohvaćaju s Amadeusa, točnije preko servisa opisanog ovdje:
https://developers.amadeus.com/self-service/category/air/api-doc/flight-low-fare-search

* • IATA šifre aerodroma mogu se vidjeti ovdje 
https://en.wikipedia.org/wiki/List_of_airports_by_IATA_code:_A
