import "./assets/global.css";
import { Routes, Route, useNavigate } from 'react-router-dom';
import Input from "./components/Input";
import Tables from "./components/Tables";
import 'bootstrap/dist/css/bootstrap.min.css';
import { useRef, useState } from "react";
import axios from "./api/axios";
import Layout from "./context/Layout";

function App() {

  const [departureAirport, setDepartureAirport] = useState("");
  const [arrivalAirport, setArrivalAirport] = useState("");
  const [departureDate, setDepartureDate] = useState("");
  const [returnDate, setReturnDate] = useState("");
  const [curency, setCurency] = useState("USD");
  const [passengers, setPassengers] = useState(1);


  const [flights, setFlights] = useState([]);
  const [loading, setLoading] = useState(false)
  const [errMsg, setErrMsg] = useState(null);

  const [airports, setAirports] = useState([]);

  const navigate = useNavigate();

  const inputRef = useRef();

  const flightSearch = async (e) => {
    e.preventDefault();
    console.log(departureAirport, arrivalAirport, departureDate, returnDate, passengers, curency, flights)
    setLoading(true)

    try {
      const search = await axios.post(
        `/api/Amadeus`,
        {
          departureAirport,
          arrivalAirport,
          dateFrom: departureDate,
          dateTo: returnDate,
          curency,
          passengers
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
      setFlights(search.data);
      console.log(search.data);
      navigate("/searches");

    } catch (error) {
      if (error.response?.status === 500) {
        setErrMsg(`Wrong input!`)
      }
      else if (error.response?.status === 404) {
        setErrMsg(`No flights found!`)
      }
      else if (error.response?.status === 400) {
        setErrMsg(`${error.response.data}`)
      }
    } finally {
      setLoading(false);
    }
  }

  const AllAirports = async () => {
    try {
      const { data } = await axios.get("/airports");
      setAirports(data);
    } catch (error) {
      if (!error.response) {
        setErrMsg("No server response");
      }
    }
  }

  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element=
          {
            <Input
              departureAirport={departureAirport}
              setDepartureAirport={setDepartureAirport}
              arrivalAirport={arrivalAirport}
              setArrivalAirport={setArrivalAirport}
              departureDate={departureDate}
              setDepartureDate={setDepartureDate}
              returnDate={returnDate}
              setReturnDate={setReturnDate}
              curency={curency}
              setCurency={setCurency}
              passengers={passengers}
              setPassengers={setPassengers}
              flightSearch={flightSearch}
              loading={loading}
              errMsg={errMsg}
              inputRef={inputRef}

              airports={airports}
              AllAirports={AllAirports}
            />
          }
        />
        <Route path="searches" element=
          {
            <Tables
              flights={flights}
            />
          }
        />
      </Route>
    </Routes>
  );
}

export default App;
