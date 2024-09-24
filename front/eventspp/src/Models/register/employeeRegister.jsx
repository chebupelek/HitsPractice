import React, { useState, useEffect } from 'react';
import { Card, Form, Button, Alert, Container, Row, Col } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { employeeRegistrationThunkCreator } from '../../Reducers/UserReducer';
import { getNamesThunkCreator } from '../../Reducers/CompaniesReducer';

const RegisterEmployee = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const companies = useSelector(state => state.company.names);

    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [selectedCompany, setSelectedCompany] = useState('');
    const [error, setError] = useState('');

    useEffect(() => {
        dispatch(getNamesThunkCreator());
    }, [dispatch]);

    const handleRegister = (e) => {
        e.preventDefault();
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!email || !password || !name || !selectedCompany) {
            setError('Все поля должны быть заполнены.');
        } else if (!emailPattern.test(email)) {
            setError('Введите корректный email.');
        } else {
            setError('');
            const registerData = {
                fullName: name,
                email: email,
                password: password,
                companyId: selectedCompany
            };
            dispatch(employeeRegistrationThunkCreator(registerData, navigate));
        }
    };

    return (
        <Container fluid>
            <Row className="justify-content-center">
                <Col md={6}>
                    <Card className="mt-5">
                        <Card.Body>
                            <Card.Title className="text-center">Оставить заявку на регистрацию</Card.Title>
                            <Form onSubmit={handleRegister}>
                                <Form.Group className="mb-3">
                                    <Form.Label>ФИО</Form.Label>
                                    <Form.Control placeholder="Введите ваше имя" value={name} onChange={(e) => setName(e.target.value)} />
                                </Form.Group>
                                <Form.Group controlId="formEmail" className="mb-3">
                                    <Form.Label>Почта</Form.Label>
                                    <Form.Control type="email" placeholder="Введите вашу почту" value={email} onChange={(e) => setEmail(e.target.value)} />
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Компания</Form.Label>
                                    <Form.Control as="select" value={selectedCompany} onChange={(e) => setSelectedCompany(e.target.value)}>
                                        <option value="">Выберите компанию</option>
                                        {companies.map(company => (
                                            <option key={company.id} value={company.id}>{company.name}</option>
                                        ))}
                                    </Form.Control>
                                </Form.Group>
                                <Form.Group controlId="formPassword" className="mb-3">
                                    <Form.Label>Пароль</Form.Label>
                                    <Form.Control type="password" placeholder="Введите ваш пароль" value={password} onChange={(e) => setPassword(e.target.value)} />
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

export default RegisterEmployee;
