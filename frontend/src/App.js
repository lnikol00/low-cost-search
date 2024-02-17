import "./assets/global.css";
import { Routes, Route, useNavigate } from 'react-router-dom';
import Input from "./components/Input";
import Tables from "./components/Tables";
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from "react";
import axios from "./api/axios";
import Layout from "./context/Layout";

function App() {

  // Input
  const [departureAirport, setDepartureAirport] = useState("");
  const [arrivalAirport, setArrivalAirport] = useState("");
  const [departureDate, setDepartureDate] = useState("");
  const [returnDate, setReturnDate] = useState("");
  const [currency, setCurrency] = useState(0);
  const [passengers, setPassengers] = useState(1);

  // Table
  const [flights, setFlights] = useState([]);
  const [errMsg, setErrMsg] = useState(null);

  const navigate = useNavigate();

  const flightSearch = async (e) => {
    e.preventDefault();
    try {
      const search = await axios.post(
        `/api/Amadeus/async`,
        { departureAirport, arrivalAirport, departureDate, currency, passengers },
        {
          headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
          },
        }
      )
      setFlights(search.data);
      console.log(search.data);
      navigate("/searches");
    } catch (error) {
      if (error.response.status === 400) {
        setErrMsg("Wrong input data!")
      }
      else if (error.response?.status === 404) {
        setErrMsg(`${error.response.data}`)
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
              // returnDate={returnDate}
              // setReturnDate={setReturnDate}
              currency={currency}
              setCurrency={setCurrency}
              passengers={passengers}
              setPassengers={setPassengers}
              flightSearch={flightSearch}
              errMsg={errMsg}
            />
          }
        />
        <Route path="searches" element=
          {
            <Tables
              flights={flights}
              errMsg={errMsg}
            />
          }
        />
      </Route>
    </Routes>
  );
}

export default App;
