import React, { useRef } from 'react'
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import styles from "../assets/tables.module.css"

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
        errMsg
    }) {

    const errRef = useRef();

    return (
        <>
            <p ref={errRef} className={errMsg ? `${styles.errmsg}` : `${styles.offscreen}`} aria-live="assertive">
                {errMsg}
            </p>
            <Form onSubmit={flightSearch} className='p-4 rounded bg-secondary f-flex align-items-center justify-content-center position-absolute top-50 start-50 translate-middle w-75 h-50'>
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
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridArrivalDate">
                        <Form.Label>Return date</Form.Label>
                        <Form.Control
                            type="date"
                            value={returnDate}
                            onChange={(e) => setReturnDate(e.target.value)}
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
                <Button type='submit'>
                    Submit
                </Button>
            </Form>
        </>
    )
}

export default Input
