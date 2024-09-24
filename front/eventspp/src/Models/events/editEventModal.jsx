import React, { useState } from 'react';
import { Modal, Button, Form, Alert } from 'react-bootstrap';

const EditEventModal = ({ isOpen, onClose, onSubmit, event }) => {
    const [formData, setFormData] = useState({
        name: event.name || '',
        description: event.description || '',
        location: event.location || '',
    });

    const [error, setError] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = () => {
        const { name, description, location } = formData;

        if (!name.trim() && !description.trim() && !location.trim()) {
            setError('Хотя бы одно поле должно быть заполнено.');
            return;
        }

        const updatedEvent = { ...formData };
        if (!updatedEvent.name.trim()) delete updatedEvent.name;
        if (!updatedEvent.description.trim()) delete updatedEvent.description;
        if (!updatedEvent.location.trim()) delete updatedEvent.location;

        setError('');
        onSubmit(updatedEvent);
    };

    return (
        <Modal show={isOpen} onHide={onClose}>
        <Modal.Header closeButton>
            <Modal.Title>Редактировать событие</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            {error && <Alert variant="danger">{error}</Alert>}
            <Form>
            <Form.Group controlId="formEventName">
                <Form.Label>Название</Form.Label>
                <Form.Control type="text" name="name" value={formData.name} onChange={handleChange} placeholder="Введите название" required/>
            </Form.Group>

            <Form.Group controlId="formEventDescription">
                <Form.Label>Описание</Form.Label>
                <Form.Control as="textarea" name="description" value={formData.description} onChange={handleChange} placeholder="Введите описание"/>
            </Form.Group>

            <Form.Group controlId="formEventLocation">
                <Form.Label>Место проведения</Form.Label>
                <Form.Control type="text" name="location" value={formData.location} onChange={handleChange} placeholder="Введите место проведения" required/>
            </Form.Group>
            </Form>
        </Modal.Body>
        <Modal.Footer>
            <Button variant="secondary" onClick={onClose}>Отмена</Button>
            <Button variant="primary" onClick={handleSubmit}>Редактировать</Button>
        </Modal.Footer>
        </Modal>
    );
};

export default EditEventModal;