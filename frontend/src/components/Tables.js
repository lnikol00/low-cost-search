import React, { useRef } from 'react'
import Table from 'react-bootstrap/Table'
import styles from "../assets/tables.module.css"

function Tables({ flights, errMsg }) {

    const errRef = useRef();

    return (
        <div className='p-3'>
            <p ref={errRef} className={errMsg ? `${styles.errmsg}` : `${styles.offscreen}`} aria-live="assertive">
                {errMsg}
            </p>
            <Table striped bordered hover variant="dark" responsive>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Departure</th>
                        <th>Destination</th>
                        <th>Departure date</th>
                        <th>Return date</th>
                        <th>Transfers to destination</th>
                        <th>Return Transfers</th>
                        <th>Passengers</th>
                        <th>Currency</th>
                        <th>Total price</th>
                    </tr>
                </thead>
                <tbody>
                    {flights.map((flight, index) => {
                        return (
                            <tr key={index}>
                                <td>{index + 1}</td>
                                <td>{flight.departureAirportName}</td>
                                <td>{flight.arrivalAirportName}</td>
                                <td>{flight.departureDate}</td>
                                <td>{flight.returnDate}</td>
                                <td>{flight.departureTransfers}</td>
                                <td>{flight.returnTransfers}</td>
                                <td>{flight.passengers}</td>
                                <td>{flight.currency}</td>
                                <td>{flight.price}</td>
                            </tr>
                        )
                    })}
                </tbody>
            </Table >
        </div>

    )
}

export default Tables
