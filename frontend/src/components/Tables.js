import React from 'react'
import Table from 'react-bootstrap/Table'
import "../assets/global.css"

function Tables({ flights }) {

    return (
        <div className='p-3'>
            <Table striped bordered hover variant="light" responsive>
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
                                <td>{new Date(flight.departureDate).toLocaleString()}</td>
                                <td>{new Date(flight.returnDate).toLocaleString()}</td>
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
