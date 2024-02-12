import React from 'react'
import Table from 'react-bootstrap/Table'

function Tables() {
    return (
        <div className='p-3'>
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
                    {/* {leauge?.leauge.filter((season) => {
                    if (season.season.toLocaleLowerCase().includes(filter.toLocaleLowerCase())) {
                        return season;
                    }
                }).map((season, index) => {
                    return (
                        <tr key={index}>
                            <td>{index + 1}</td>
                            <td>{season.team}</td>
                            <td>{season.played}</td>
                            <td>{season.winns}</td>
                            <td>{season.ties}</td>
                            <td>{season.loses}</td>
                            <td>{season.goals}</td>
                            <td>{season.points}</td>
                        </tr>
                    )
                })} */}
                    <tr>
                        <td>1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                        <td>Test1</td>
                    </tr>
                </tbody>
            </Table >
        </div>

    )
}

export default Tables
