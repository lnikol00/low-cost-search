import React from 'react'
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Nav from 'react-bootstrap/Nav';

function Input() {
    return (
        <Form className='p-4 rounded bg-secondary f-flex align-items-center justify-content-center position-absolute top-50 start-50 translate-middle w-75 h-50'>
            <Row className="mb-3">
                <Form.Group as={Col} controlId="formGridEmail">
                    <Form.Label>Departure</Form.Label>
                    <Form.Select defaultValue="Choose...">
                        <option>Choose...</option>
                        <option>...</option>
                    </Form.Select>
                </Form.Group>

                <Form.Group as={Col} controlId="formGridPassword">
                    <Form.Label>Destination</Form.Label>
                    <Form.Select defaultValue="Choose...">
                        <option>Choose...</option>
                        <option>...</option>
                    </Form.Select>
                </Form.Group>
            </Row>

            <Row className='mb-3'>
                <Form.Group as={Col} controlId="formGridAddress1">
                    <Form.Label>Departure date</Form.Label>
                    <Form.Control type='date' placeholder="1234 Main St" />
                </Form.Group>

                <Form.Group as={Col} controlId="formGridAddress2">
                    <Form.Label>Return date</Form.Label>
                    <Form.Control type="date" placeholder="Apartment, studio, or floor" />
                </Form.Group>
            </Row>


            <Row className="mb-3">
                <Form.Group as={Col} controlId="formGridCity">
                    <Form.Label>Number of passengers</Form.Label>
                    <Form.Control type='number' />
                </Form.Group>

                <Form.Group as={Col} controlId="formGridState">
                    <Form.Label>Currency</Form.Label>
                    <Form.Select defaultValue="Choose...">
                        <option>Choose...</option>
                        <option>...</option>
                    </Form.Select>
                </Form.Group>
            </Row>
            <Button variant="primary" type="submit">
                Submit
            </Button>
            <Nav.Item>
                <Nav.Link eventKey="link-1" href={`/test`}>Premier Leauge</Nav.Link>
            </Nav.Item>
        </Form>
    )
}

export default Input
