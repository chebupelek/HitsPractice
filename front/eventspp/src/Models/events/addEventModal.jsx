import React, { useState } from 'react';
import { Modal, Button, Form, Alert } from 'react-bootstrap';

const AddEventModal = ({ isOpen, onClose, onSubmit }) => {
    const [formData, setFormData] = useState({
        name: '',
        eventDate: '',
        description: '',
        location: '',
        deadline: ''
    });

    const [error, setError] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = () => {
        const newEvent = { ...formData };

        const eventDate = new Date(formData.eventDate);
        const today = new Date();
        const sevenDaysFromNow = new Date();
        sevenDaysFromNow.setDate(today.getDate() + 7);

        if (eventDate < sevenDaysFromNow) {
            setError('Дата события должна быть как минимум через 7 дней от сегодняшней даты.');
            return;
        }

        if (formData.deadline) {
            const deadlineDate = new Date(formData.deadline);
            if (deadlineDate >= eventDate) {
                setError('Дедлайн должен быть до даты проведения мероприятия.');
                return;
            }
        }

        var eventDay = new Date(formData.eventDate);
        const eventDateISO = eventDay.toISOString();
        var deadlineDay = '';
        if(formData.deadline !== '')
        {
            var deadlineDate = new Date(formData.deadline);
            deadlineDay = deadlineDate.toISOString();
        }

        var addData = {
            name: formData.name,
            eventDate: eventDateISO,
            description: formData.description,
            location: formData.location,
            deadline: deadlineDay
        };

        if (!formData.description) delete addData.description;
        if (!formData.deadline) delete addData.deadline;

        setError('');
        
        onSubmit(addData);
    };

    return (
        <Modal show={isOpen} onHide={onClose}>
            <Modal.Header closeButton>
                <Modal.Title>Добавить событие</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {error && <Alert variant="danger">{error}</Alert>}
                <Form>
                    <Form.Group controlId="formEventName">
                        <Form.Label>Название</Form.Label>
                        <Form.Control type="text" name="name" value={formData.name} onChange={handleChange} placeholder="Введите название" required/>
                    </Form.Group>

                    <Form.Group controlId="formEventDate">
                        <Form.Label>Дата и время</Form.Label>
                        <Form.Control type="datetime-local" name="eventDate" value={formData.eventDate} onChange={handleChange} required/>
                    </Form.Group>

                    <Form.Group controlId="formEventLocation">
                        <Form.Label>Место проведения</Form.Label>
                        <Form.Control type="text" name="location" value={formData.location} onChange={handleChange} required/>
                    </Form.Group>

                    <Form.Group controlId="formEventDeadline">
                        <Form.Label>Дедлайн</Form.Label>
                        <Form.Control type="datetime-local" name="deadline" value={formData.deadline} onChange={handleChange} />
                    </Form.Group>

                    <Form.Group controlId="formEventDescription">
                        <Form.Label>Описание</Form.Label>
                        <Form.Control as="textarea" name="description" value={formData.description} onChange={handleChange} />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onClose}>Отмена</Button>
                <Button variant="primary" onClick={handleSubmit}>Добавить</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default AddEventModal;
