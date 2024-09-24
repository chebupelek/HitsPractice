import React, { useState } from 'react';
import { Card, Form, Button, Alert, Container, Row, Col } from 'react-bootstrap';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { studenRegistrationThunkCreator } from '../../Reducers/UserReducer';

const RegisterStudent = () => {
    const dispatch = useDispatch();

    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [group, setGroup] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = (e) => {
        e.preventDefault();
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!email || !password || !name || !group) {
            setError('Все поля должны быть заполнены.');
        } else 
        if (!emailPattern.test(email)) 
        {
            setError('Введите корректный email.');
        } 
        else 
        {
            setError('');
            const registerData = {
                fullName: name,
                email: email,
                password: password,
                group: group
            };
            dispatch(studenRegistrationThunkCreator(registerData, navigate));
        }
    };

    return (
        <Container fluid>
            <Row className="justify-content-center">
                <Col md={6}>
                    <Card className="mt-5">
                        <Card.Body>
                            <Card.Title className="text-center">Регистрация как студент</Card.Title>
                            <Form onSubmit={handleLogin}>
                                <Form.Group className="mb-3">
                                    <Form.Label>ФИО</Form.Label>
                                    <Form.Control placeholder="Введите ваше имя" value={name} onChange={(e) => setName(e.target.value)}/>
                                </Form.Group>
                                <Form.Group controlId="formEmail" className="mb-3">
                                    <Form.Label>Почта</Form.Label>
                                    <Form.Control type="email" placeholder="Введите вашу почту" value={email} onChange={(e) => setEmail(e.target.value)}/>
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Группа</Form.Label>
                                    <Form.Control placeholder="Введите вашу группу" value={group} onChange={(e) => setGroup(e.target.value)}/>
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
                                <Button variant="primary" type="submit" className="w-100">Зарегистрироваться</Button>
                            </Form>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
};

export default RegisterStudent;
