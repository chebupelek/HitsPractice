import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Navbar, Nav, Col, Row, Container, Button } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { logoutThunkCreator, setRoleThunkCreator } from '../../Reducers/UserReducer';

const Header = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const token = localStorage.getItem("token") !== null;
    const role = useSelector(state => state.user.role);

    useEffect(() => {
        if (token) {
            dispatch(setRoleThunkCreator(navigate));
        }
    }, [token, dispatch]);

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
                    {token && role == 0 ? (
                        <>
                            <Col md={2}>
                                {token && (<Nav.Link as={Link} to="/page1">Мероприятия</Nav.Link>)}
                            </Col>
                            <Col md={2}>
                                {token && (<Nav.Link as={Link} to="/page1">Компании</Nav.Link>)}
                            </Col>
                            <Col md={2}>
                                {token && (<Nav.Link as={Link} to="/page1">Заявки</Nav.Link>)}
                            </Col>
                        </>
                    ) : (
                        <Col md={6}></Col>
                    )}
                    <Col md={2}></Col>
                    <Col md={2} className="d-flex justify-content-end">
                        {token ? (
                            <Button type="light" onClick={handleLogout}>Выйти</Button>
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
