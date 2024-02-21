import React from 'react'
import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import logoImage from "../utils/images/logo-image.png"
import { AiFillHome } from "react-icons/ai";

function Header() {
    return (
        <>
            <Navbar expand="lg" className="bg-body-tertiary" bg='dark' data-bs-theme="dark" sticky='top'>
                <Container>
                    <Navbar.Brand>
                        <img
                            alt="logo"
                            src={logoImage}
                            width="30"
                            height="30"
                            className="d-inline-block align-top"
                        />{' '}
                        Low Cost Search Engine
                    </Navbar.Brand>
                    <Nav.Link href="/" className='text-white'>
                        <AiFillHome />
                    </Nav.Link>
                </Container>
            </Navbar>
        </>
    )
}

export default Header
