import React from 'react';
import { useDispatch } from 'react-redux';
import { Navbar, Nav, Col, Row, Container, Button } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { logoutThunkCreator } from '../../Reducers/UserReducer';

const Header = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const token = localStorage.getItem("token") !== null;

    const handleLogout = () => {
        dispatch(logoutThunkCreator(navigate));
    }

    return (
        <Navbar bg="light" expand="lg">
            <Container fluid>
                <Row className="w-100 align-items-center">
                    <Col md={2} className="fs-4">
                        Мероприятия
                    </Col>
                    <Col md={2}>
                        {token && (<Nav.Link as={Link} to="/page1">Кнопка 1</Nav.Link>)}
                    </Col>
                    <Col md={2}>
                        {token && (<Nav.Link as={Link} to="/page1">Кнопка 1</Nav.Link>)}
                    </Col>
                    <Col md={2}>
                        {token && (<Nav.Link as={Link} to="/page1">Кнопка 1</Nav.Link>)}
                    </Col>
                    <Col md={2}></Col>
                    <Col md={2} className="d-flex justify-content-end">
                        {token ? (
                            <Button type="light" onClick={handleLogout()}>Выйти</Button>
                        ) : (
                            <Nav.Link as={Link} to="/login">Войти</Nav.Link>
                        )}
                    </Col>
                </Row>
            </Container>
        </Navbar>
    );
};

export default Header;
