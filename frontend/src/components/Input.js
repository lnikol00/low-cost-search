import React, { useRef, useState } from 'react'
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import "../assets/global.css"

function Input(
    {
        departureAirport,
        setDepartureAirport,
        arrivalAirport,
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
        inputRef
    }) {

    const errRef = useRef();

    const resetValues = () => {
        setDepartureAirport("");
        setArrivalAirport("");
        setReturnDate("");
        setDepartureDate("");
        setCurrency(0);
        setPassengers(1);
        window.location.reload();
    }

    const Currency = {
        0: "USD",
        1: "EUR",
        2: "HRK"
    }

    return (
        <>
            {loading ? <div>Loading...</div> : <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} >
                {errMsg}
            </p>}
            <Form ref={inputRef} onSubmit={flightSearch} className='p-4 rounded bg-secondary f-flex align-items-center justify-content-center position-absolute top-50 start-50 translate-middle w-75 h-50'>
                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridDepartureAirport">
                        <Form.Label>Departure</Form.Label>
                        <Form.Control
                            type='text'
                            placeholder="SYD"
                            value={departureAirport}
                            onChange={(e) => setDepartureAirport(e.target.value)}
                            required
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridArivalAirport">
                        <Form.Label>Destination</Form.Label>
                        <Form.Control
                            type='text'
                            placeholder="BKK"
                            value={arrivalAirport}
                            onChange={(e) => setArrivalAirport(e.target.value)}
                            required
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
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridArrivalDate">
                        <Form.Label>Return date</Form.Label>
                        <Form.Control
                            type="date"
                            value={returnDate === null ? "" : returnDate}
                            onChange={(e) => setReturnDate(returnDate === null ? "" : e.target.value)}
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
                            <option value={0}>USD</option>
                            <option value={1}>EUR</option>
                            <option value={2}>HRK</option>
                        </Form.Select>
                    </Form.Group>
                </Row>
                <div className='d-flex justify-content-between'>
                    <Button onClick={resetValues}>
                        Reset
                    </Button>
                    <Button type='submit'>
                        Submit
                    </Button>
                </div>
            </Form>
        </>
    )
}

export default Input
