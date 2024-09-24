import React from 'react';
import { Card } from 'react-bootstrap';
import EventCard from './eventCard';

const DayCard = ({ dayEvents, role, date }) => {
    const dayName = new Date(date).toLocaleString('ru', { weekday: 'long' });

    return (
        <Card className="my-3">
            <Card.Header className="d-flex justify-content-between">
                <span>{date} - {dayName}</span>
            </Card.Header>
            <Card.Body>
                {dayEvents.map((event) => (
                    <EventCard key={event.id} event={event} role={role} />
                ))}
            </Card.Body>
        </Card>
    );
};

export default DayCard;