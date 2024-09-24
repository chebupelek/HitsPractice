import React, { useState, useEffect } from 'react';
import { Alert, Button, Form, ListGroup, Modal } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { getCompaniesThunkCreator, addCompanyThunkCreator, removeCompanyThunkCreator } from '../../Reducers/CompaniesReducer';
import { useNavigate } from 'react-router-dom';

const Companies = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    
    const [showAddModal, setShowAddModal] = useState(false);
    const [newCompany, setNewCompany] = useState({ name: '', email: '' });
    const [errorMessage, setErrorMessage] = useState('');

    const companies = useSelector((state) => state.company.companies);

    useEffect(() => {
        dispatch(getCompaniesThunkCreator(navigate)); 
    }, [companies, dispatch, navigate]);

    const handleAddCompany = () => {
        if (!newCompany.name || !newCompany.email) {
            setErrorMessage('Оба поля должны быть заполнены.');
            return;
        }

        dispatch(addCompanyThunkCreator(navigate, newCompany)).then(() => {
            setShowAddModal(false);
            setNewCompany({ name: '', email: '' });
            setErrorMessage('');
        });
    };

    const handleDeleteCompany = (id) => {
        dispatch(removeCompanyThunkCreator(navigate, { id }));
    };

    return ( 
        <div className="container mt-4">
            <Button variant="primary" onClick={() => setShowAddModal(true)}>Добавить компанию</Button>

            <div className="mt-4">
                <ListGroup>
                    {companies.map((company) => (
                        <ListGroup.Item key={company.id} className="d-flex justify-content-between align-items-center">
                            <div>
                                <strong>{company.name}</strong> - {company.email}
                            </div>
                            <Button variant="danger" onClick={() => handleDeleteCompany(company.id)}>Удалить</Button>
                        </ListGroup.Item>
                    ))}
                </ListGroup>
            </div>

            <Modal show={showAddModal} onHide={() => setShowAddModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Добавить компанию</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {errorMessage && <Alert variant="danger">{errorMessage}</Alert>}
                    <Form>
                        <Form.Group controlId="formCompanyName">
                            <Form.Label>Название компании</Form.Label>
                            <Form.Control type="text" placeholder="Введите название" value={newCompany.name} onChange={(e) => setNewCompany({ ...newCompany, name: e.target.value })} required/>
                        </Form.Group>

                        <Form.Group controlId="formCompanyEmail">
                            <Form.Label>Email компании</Form.Label>
                            <Form.Control type="email" placeholder="Введите email" value={newCompany.email} onChange={(e) => setNewCompany({ ...newCompany, email: e.target.value })} required/>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowAddModal(false)}>Отмена</Button>
                    <Button variant="primary" onClick={handleAddCompany}>Добавить</Button>
                </Modal.Footer>
            </Modal>
        </div>
    ); 
};

export default Companies;