import React, { useState } from 'react';
import { Card, Button } from 'react-bootstrap';
import EditEventModal from './editEventModal';
import { useDispatch, useSelector } from 'react-redux';
import { signUpThunkCreator, unsignThunkCreator, deleteEventThunkCreator, changeEventThunkCreator, getRegisteredThunkCreator } from '../../Reducers/EventsReducer';
import RegisteredListModal from './registered';

const EventCard = ({ event, role, mondayDate }) => {
    const [showEditModal, setShowEditModal] = useState(false);
    const dispatch = useDispatch();
    const [showRegisteredListModal, setShowRegisteredListModal] = useState(false);
    const registeredList = useSelector((state) => state.events.registered);

    const handleSignUp = () => {
        dispatch(signUpThunkCreator({ id: event.id }, null));
    };

    const handleUnsign = () => {
        dispatch(unsignThunkCreator({ id: event.id }, null));
    };

    const handleDelete = () => {
        dispatch(deleteEventThunkCreator({ id: event.id }, null));
    };

    const handleEdit = () => {
        setShowEditModal(true);
    };

    const handleViewRegistered = () => {
        dispatch(getRegisteredThunkCreator(event.id, null));
        setShowRegisteredListModal(true);
    };

    const handleCloseRegisteredListModal = () => {
        setShowRegisteredListModal(false);
    };

    const handleCloseEditModal = () => {
        setShowEditModal(false);
    };

    const handleSubmitEdit = (updatedEvent) => {
        dispatch(changeEventThunkCreator({ id: event.id, ...updatedEvent }, null));
        setShowEditModal(false);
    };

    const eventDate = new Date(event.eventDate).toLocaleString();
    const eventDeadline = event.deadline ? new Date(event.deadline).toLocaleString() : 'Нет дедлайна';

    return (
        <>
            <Card className={`mb-2 ${event.isSign ? 'bg-success text-white' : ''}`}>
                <Card.Body>
                    <Card.Title>{event.name}</Card.Title>
                    <Card.Text>
                        Описание: {event.description || 'Нет описания'}<br />
                        Дата: {eventDate}<br />
                        Дедлайн: {eventDeadline}<br />
                        Место: {event.location}<br />
                        Сотрудник: {event.employee}
                    </Card.Text>
                    {role === 1 && !event.isSign && (
                        <Button variant="success" onClick={handleSignUp}>Подписаться</Button>
                    )}
                    {role === 1 && event.isSign && (
                        <Button variant="warning" onClick={handleUnsign}>Отписаться</Button>
                    )}
                    {event.isCreate && (
                        <>
                            <Button variant="danger" className="me-2" onClick={handleDelete}>Удалить</Button>
                            <Button variant="secondary" onClick={handleEdit}>Редактировать</Button>
                        </>
                    )}
                    {(role === 0 || event.isCreate) && (
                        <Button variant="info" className="me-2" onClick={handleViewRegistered}>Подписавшиеся</Button>
                    )}
                </Card.Body>
            </Card>
            <EditEventModal isOpen={showEditModal} onClose={handleCloseEditModal} onSubmit={handleSubmitEdit} event={event}/>
            <RegisteredListModal isOpen={showRegisteredListModal} onClose={handleCloseRegisteredListModal} registeredList={registeredList}/>
        </>
    );
};

export default EventCard;
