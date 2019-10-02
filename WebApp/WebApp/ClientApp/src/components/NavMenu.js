import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
    displayName = NavMenu.name

    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/'}>WebApp</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav>
                        <LinkContainer to={'/'} exact>
                            <NavItem>
                                Home
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/login'}>
                            <NavItem>
                                Login
                             </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/logout'}>
                            <NavItem>
                                Logout
                             </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/createticket'}>
                            <NavItem>
                                Create New Ticket
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/viewtickets'}>
                            <NavItem>
                                View Tickets
                            </NavItem>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
