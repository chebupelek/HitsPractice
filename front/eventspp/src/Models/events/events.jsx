import React, { useState, useEffect } from 'react';
import { Button, Form } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { getEventsThunkCreator, addEventThunkCreator } from '../../Reducers/EventsReducer';
import DayCard from './dayCard';
import AddEventModal from './addEventModal';
import { useNavigate } from 'react-router-dom';

const Events = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    
    const [selectedMonday, setSelectedMonday] = useState(new Date().toISOString().slice(0, 10));
    const [isAddEventModalOpen, setIsAddEventModalOpen] = useState(false);

    const events = useSelector((state) => state.events.events);
    const role = useSelector((state) => state.user.role);

    useEffect(() => {
        const mondayDate= new Date(selectedMonday).toISOString(); 
        dispatch(getEventsThunkCreator(mondayDate, navigate)); 
    }, [selectedMonday, dispatch, navigate]);

    const handleSearch = () => 
    { 
        const mondayDate = new Date(selectedMonday).toISOString(); 
        dispatch(getEventsThunkCreator(mondayDate, navigate)); 
    };
        
    const handleOpenAddEventModal = () => 
    { 
        setIsAddEventModalOpen(true); 
    };
        
    const handleCloseAddEventModal = () => { 
        setIsAddEventModalOpen(false); 
    };
        
    const handleAddEvent = (eventData) => { 
        dispatch(addEventThunkCreator(eventData, navigate, selectedMonday)); 
        setIsAddEventModalOpen(false); 
    };

    const addDays = (date, days) => {
        const result = new Date(date);
        result.setDate(result.getDate() + days);
        return result;
    };
        
    return ( 
        <div className="container mt-4">
            <Form.Group controlId="monday-date" className="mb-3"> 
                <Form.Label>Выберите понедельник</Form.Label> 
                <Form.Control type="date" value={selectedMonday} onChange={(e) => setSelectedMonday(e.target.value)}/> 
            </Form.Group> 

            <Button variant="primary" onClick={handleSearch}> Поиск </Button>
            
            {role === 2 && (
                <Button variant="primary" onClick={() => handleOpenAddEventModal()}>
                    Добавить
                </Button>
            )}

            <div className="mt-4">
                {events.map((dayEvents, index) => (
                    <DayCard 
                        date={addDays(selectedMonday, index).toISOString().slice(0, 10)}
                        key={index} 
                        dayEvents={dayEvents} 
                        role={role} 
                    />
                ))}
            </div>

            {isAddEventModalOpen && (
                <AddEventModal 
                    isOpen={isAddEventModalOpen} 
                    onClose={handleCloseAddEventModal} 
                    onSubmit={handleAddEvent}
                />
            )}
        </div>
    ); 
};

export default Events;