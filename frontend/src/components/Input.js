import React, { useRef, useEffect, useState } from 'react'
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import "../assets/global.css"
import { Typeahead } from 'react-bootstrap-typeahead';

function Input(
    {
        departureAirport,
        arrivalAirport,
        setDepartureAirport,
        setArrivalAirport,
        departureDate,
        setDepartureDate,
        returnDate,
        setReturnDate,
        curency,
        setCurency,
        passengers,
        setPassengers,
        flightSearch,
        loading,
        errMsg,
        inputRef,
        airports,
        AllAirports
    }) {

    const errRef = useRef();

    const resetValues = () => {
        setDepartureAirport("");
        setArrivalAirport("");
        setReturnDate("");
        setDepartureDate("");
        setCurency("USD");
        setPassengers(1);
        window.location.reload();
    }

    useEffect(() => {
        AllAirports();
    }, []);


    const [departureAirportName, setDepartureAirportName] = useState("");
    const [arrivalAirportName, setArrivalAirportName] = useState("");

    return (
        <div className="my-5 mx-auto">
            {loading ? <div className='loading'>Loading...</div> : <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} >
                {errMsg}
            </p>}
            <Form ref={inputRef} onSubmit={flightSearch} className='p-4 m-auto rounded bg-success w-auto mw-100 h-auto mh-100 position-relative'>
                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridDepartureAirport">
                        <Form.Label>Departure</Form.Label>
                        <Typeahead
                            id="basic-typeahead-single"
                            labelKey={(option) => `${option.name} (${option.iata})`}
                            onChange={(selected) => {
                                if (selected.length > 0) {
                                    setDepartureAirport(selected[0].iata);
                                    setDepartureAirportName(selected[0].name);
                                } else {
                                    setDepartureAirport(null);
                                    setDepartureAirportName('');
                                }
                            }}
                            options={airports.map(airport => ({
                                name: airport.name,
                                iata: airport.iata
                            }))}
                            filterBy={(option, state) => {
                                const searchTerm = state.text.toLowerCase();
                                return (
                                    option.name.toLowerCase().includes(searchTerm)
                                );
                            }}
                            placeholder="Choose departure airport..."
                            selected={
                                departureAirport ? airports.filter(airport => airport.iata === departureAirport) : []
                            }
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridArivalAirport">
                        <Form.Label>Destination</Form.Label>
                        <Typeahead
                            id="basic-typeahead-single"
                            labelKey={(option) => `${option.name} (${option.iata})`}
                            onChange={(selected) => {
                                if (selected.length > 0) {
                                    setArrivalAirport(selected[0].iata);
                                    setArrivalAirportName(selected[0].name);
                                } else {
                                    setArrivalAirport(null);
                                    setArrivalAirportName('');
                                }
                            }}
                            options={airports.map(airport => ({
                                name: airport.name,
                                iata: airport.iata
                            }))}
                            filterBy={(option, state) => {
                                const searchTerm = state.text.toLowerCase();
                                return (
                                    option.name.toLowerCase().includes(searchTerm)
                                );
                            }}
                            placeholder="Choose arrival airport..."
                            selected={
                                arrivalAirport ? airports.filter(airport => airport.iata === arrivalAirport) : []
                            }

                        />
                    </Form.Group>
                </Row>

                <Row className='mb-3'>
                    <Form.Group as={Col} controlId="formGridDepartureDate">
                        <Form.Label>Departure date</Form.Label>
                        <Form.Control
                            type='date'
                            value={departureDate}
                            onChange={(e) => setDepartureDate(e.target.value)}
                            required
                            className='mb-2'
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridArrivalDate">
                        <Form.Label>Return date</Form.Label>
                        <Form.Control
                            type="date"
                            value={returnDate}
                            onChange={(e) => setReturnDate(e.target.value)}
                            required
                        />
                    </Form.Group>
                </Row>


                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridCurrency">
                        <Form.Label>Currency</Form.Label>
                        <Form.Select
                            type="text"
                            value={curency}
                            onChange={(e) => setCurency(e.target.value)}
                            required

                        >
                            <option value="USD">USD</option>
                            <option value="EUR">EUR</option>
                            <option value="HRK">HRK</option>
                        </Form.Select>
                    </Form.Group>
                    <Form.Group as={Col} controlId="formGridPassengers">
                        <Form.Label>Number of adults</Form.Label>
                        <Form.Control
                            min="1"
                            type='number'
                            value={passengers}
                            onChange={(e) => setPassengers(e.target.value)}
                            required

                        />
                    </Form.Group>
                </Row>
                <div className='d-flex justify-content-between'>
                    <Button onClick={resetValues} className=' bg-light text-black border-0'>
                        Reset
                    </Button>
                    <Button type='submit' className='bg-light text-black border-0'>
                        Submit
                    </Button>
                </div>
            </Form>

            <div>
                {departureAirport} {arrivalAirport}
            </div>
        </div>
    )
}

export default Input
