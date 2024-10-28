import React, { useRef, useEffect, useState } from 'react'
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import "../assets/global.css"
import { Typeahead } from 'react-bootstrap-typeahead';
import { BiSolidLeftArrowSquare } from 'react-icons/bi';

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
        currency,
        setCurrency,
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
        setCurrency("USD");
        setPassengers(1);
        window.location.reload();
    }

    useEffect(() => {
        AllAirports();
    }, []);

    const test = () => {

    }

    const [departureAirportName, setDepartureAirportName] = useState("");
    const [arrivalAirportName, setArrivalAirportName] = useState("");

    return (
        <>
            {loading ? <div className='loading'>Loading...</div> : <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} >
                {errMsg}
            </p>}
            <Form ref={inputRef} onSubmit={flightSearch} className='p-4 rounded bg-success f-flex align-items-center justify-content-center position-absolute top-50 start-50 translate-middle w-50 h-50'>
                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridDepartureAirport">
                        <Form.Label>Departure</Form.Label>
                        <Typeahead
                            id="basic-typeahead-single"
                            labelKey="name"
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
                                departureAirportName ? airports.filter(airport => airport.name === departureAirportName) : []
                            }
                        />
                        {/* <Form.Control
                            type='text'
                            value={departureAirport}
                            onChange={(e) => setDepartureAirport(e.target.value)}
                            required
                        /> */}
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridArivalAirport">
                        <Form.Label>Destination</Form.Label>
                        <Typeahead
                            id="basic-typeahead-single"
                            labelKey="name"
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
                                arrivalAirportName ? airports.filter(airport => airport.name === arrivalAirportName) : []
                            }

                        />
                        {/* <Form.Control
                            type='text'
                            value={arrivalAirport}
                            onChange={(e) => setArrivalAirport(e.target.value)}
                            required
                        /> */}
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
                    <Form.Group as={Col} controlId="formGridPassengers">
                        <Form.Label>Number of adults</Form.Label>
                        <Form.Control
                            type='number'
                            value={passengers}
                            onChange={(e) => setPassengers(e.target.value)}
                            required

                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridCurrency">
                        <Form.Label>Currency</Form.Label>
                        <Form.Select
                            value={currency}
                            onChange={(e) => setCurrency(e.target.value)}
                            required

                        >
                            <option value="USD">USD</option>
                            <option value="EUR">EUR</option>
                            <option value="HRK">HRK</option>
                        </Form.Select>
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

            {/* <div>
                {departureAirport} {arrivalAirport} {departureDate} {returnDate} {currency} {passengers}
            </div> */}
        </>
    )
}

export default Input
