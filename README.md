# Job Application Task

<h2>Uvod:</h2>
<p>
  Potrebno je napraviti .NET web aplikaciju za pretraživanje cijena low-cost avionskih letova. Ukoliko se
  ne možeš odlučiti za frontend tehnologiju, u KING-u trenutno najviše koristimo ReactJS, a ponekad i
  Angular. 
</p>

<p>Cijene letova se moraju moći pretraživati po:</p>
* polaznom aerodromu (prema IATA šifri aerodroma)
* odredišnom aerodromu (prema IATA šifri aerodroma)
* datumu polaska/povratka
* broju putnika
* valuti (USD, EUR, HRK)

<h2>Zadatak</h2>
<p>
  Rezultate je potrebno tablično prikazati na ekranu, vrijednosti koje je potrebno prikazati:
</p>
* polazni aerodrom
* odredišni aerodrom
* datum polaska/povratka
* broj presjedanja u odlaznom/povratnom putovanju
* broju putnika
* valuta
* ukupna cijena

<b>Dodatno, dohvaćene podatke je potrebno lokalno pohraniti i u slučaju ponovne pretrage po istim
parametrima dohvatiti lokalno pohranjene podatke.</b>

* Podatke o letovima se dohvaćaju s Amadeusa, točnije preko servisa opisanog ovdje:
https://developers.amadeus.com/self-service/category/air/api-doc/flight-low-fare-search

* • IATA šifre aerodroma mogu se vidjeti ovdje 
https://en.wikipedia.org/wiki/List_of_airports_by_IATA_code:_A
