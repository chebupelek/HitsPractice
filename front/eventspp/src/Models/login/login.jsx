import React, { useState } from 'react';
import { Card, Form, Button, Alert, Container, Row, Col } from 'react-bootstrap';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { loginThunkCreator } from '../../Reducers/UserReducer';

const Login = () => {
    const dispatch = useDispatch();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = (e) => {
        e.preventDefault();
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!email || !password) {
            setError('Все поля должны быть заполнены.');
        } else 
        if (!emailPattern.test(email)) 
        {
            setError('Введите корректный email.');
        } 
        else 
        {
            setError('');
            const loginData = {
                email: email,
                password: password
            };
            dispatch(loginThunkCreator(loginData, navigate));
        }
    };

    return (
        <Container fluid>
            <Row className="justify-content-center">
                <Col md={6}>
                    <Card className="mt-5">
                        <Card.Body>
                            <Card.Title className="text-center">Войти</Card.Title>
                            <Form onSubmit={handleLogin}>
                                <Form.Group controlId="formEmail" className="mb-3">
                                    <Form.Label>Почта</Form.Label>
                                    <Form.Control type="email" placeholder="Введите вашу почту" value={email} onChange={(e) => setEmail(e.target.value)}/>
                                </Form.Group>
                                <Form.Group controlId="formPassword" className="mb-3">
                                    <Form.Label>Пароль</Form.Label>
                                    <Form.Control type="password" placeholder="Введите ваш пароль" value={password} onChange={(e) => setPassword(e.target.value)}/>
                                </Form.Group>
                                {error && (
                                    <Alert variant="danger" className="mb-3">
                                        {error}
                                    </Alert>
                                )}
                                <Button variant="primary" type="submit" className="w-100">Вход</Button>
                            </Form>
                            <div className="d-flex justify-content-between">
                                <Button variant="link" onClick={() => navigate('/register/student')}> Зарегистрироваться как студент</Button>
                                <Button variant="link" onClick={() => navigate('/register/company')}> Зарегистрироваться как представитель компании</Button>
                            </div>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
};

export default Login;
